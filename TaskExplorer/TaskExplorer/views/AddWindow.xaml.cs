using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TaskExplorer.models;

namespace TaskExplorer.views;

public partial class AddWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly ObservableCollection<Task>? tasks;

    public ObservableCollection<STATUS> Statuses { get; set; } = new ObservableCollection<STATUS>();

    private string? inputTaskName;
    private string? inputTaskText;
    private STATUS inputTaskStatus;

    public string? InputTaskName
    {
        get => inputTaskName;
        set {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(inputTaskName));

            inputTaskName = value;
        }
    }

    public string? InputTaskText
    {
        get => inputTaskText;
        set
        {
            inputTaskText = value;
        }
    }

    public STATUS InputTaskStatus
    {
        get => inputTaskStatus;
        set => inputTaskStatus = value;
    }

    public AddWindow()
    {
        InitializeComponent();

        this.DataContext = this;

        Statuses.Add(STATUS.Todo);
        Statuses.Add(STATUS.InProgress);
    }

    public AddWindow(ObservableCollection<Task> tasks) : this()
    {
        this.tasks = tasks;
    }

    protected void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
    {
        field = value;

        if (this.PropertyChanged != null)
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        InputTaskText = ConvertRichTextBoxContentsToString(this.TaskRichTextBox);
        Console.WriteLine(InputTaskStatus);
        this.tasks?.Add(new Task(name: InputTaskName, text: InputTaskText, status: InputTaskStatus));

        this.Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e) => this.Close();

    string ConvertRichTextBoxContentsToString(RichTextBox richTextBox)
    {
        TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
        return textRange.Text;
    }
}
