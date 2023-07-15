using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;

using TaskExplorer.models;
using TaskExplorer.views;
using System;
using System.Collections.Generic;

namespace TaskExplorer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<Task>? Tasks { get; set; }
    private static readonly string path = "json\\tasks.json";

    public bool IsSelectAllFinished { get; set; } = false;

    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = this;

        Tasks = new ObservableCollection<Task>();

        this.TaskContentRichTextBox.IsEnabled = false;
        LoadTasks(path);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);

        this.SaveTasks(path, Tasks);
    }

    protected void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
    {
        field = value;

        if (this.PropertyChanged != null)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    private void OpenAddWindow_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button addButton)
            new AddWindow(this.Tasks).ShowDialog();
    }

    private void SelectTask_Click(object sender, RoutedEventArgs e)
    {
        if (sender is ListView listView)
        {
            if (listView.SelectedItem is Task task)
                task.IsSelected = !task.IsSelected;
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button deleteButton)
        {
            Task? currentTask = Tasks?.FirstOrDefault(task => task.IsSelected);

            while (currentTask != null)
            {
                Tasks?.Remove(currentTask);
                currentTask = Tasks?.FirstOrDefault(task => task.IsSelected);
            }
        }
    }

    private void Task_IsMouseCapturedChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is ListView listView)
        {
            if (listView.SelectedItem is Task task)
                ChangeContent_RichTextBox(TaskContentRichTextBox, task.Text);
        }
    }

    private void FinishButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button finishButton)
        {
            var action = new Action<Task>(task => {
                task.Status = STATUS.Done;
                task.IsSelected = IsSelectAllFinished;
            });
            var predicate = new Func<Task, bool>(task => task.IsSelected);
            TaskChanger(Tasks, action, predicate);
        }
    }

    private void SelectFinished_IsEnabledChanged(object sender, RoutedEventArgs e)
    {
        if (sender is CheckBox checkbox)
        {
            IsSelectAllFinished = !IsSelectAllFinished;

            var action = new Action<Task>(task => task.IsSelected = IsSelectAllFinished);
            var predicate = new Func<Task, bool>(task => task.Status == STATUS.Done);
            TaskChanger(Tasks, action, predicate);
        }
    }

    private void TaskChanger(IEnumerable<Task>? tasks, Action<Task> action, Func<Task, bool> predicate)
    {
        IEnumerable<Task>? currentTasks = Tasks?.Where(predicate);

        if (currentTasks == null)
            return;

        foreach (var task in currentTasks)
            action.Invoke(task);
    }

    private void LoadTasks(string path)
    {
        if (File.ReadAllText(path) == string.Empty || File.Exists(path) == false)
            return;

        string json = File.ReadAllText(path);
        Tasks = JsonSerializer.Deserialize<ObservableCollection<Task>>(json);
    }

    public void SaveTasks(string path, ObservableCollection<Task>? tasks)
    {
        if (File.Exists(path) == false)
            File.Create(path);

        string json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(path, json);
    }

    private void ChangeContent_RichTextBox(RichTextBox textBox, string? text)
    {
        if (textBox == null || text == null)
            return;
        
        textBox.Document.Blocks.Clear();
        textBox.AppendText(text);
    }
}
