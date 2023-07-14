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

namespace TaskExplorer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<Task> Tasks { get; set; }
    private static readonly string path = "assets\\tasks.json";

    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = this;

        Tasks = new ObservableCollection<Task>();

        //if (File.Exists(path))
        //{
        //    string json = File.ReadAllText(path);
        //    Tasks = JsonSerializer.Deserialize<ObservableCollection<Task>>(json);
        //}
        //else
        //{
        //    File.Create(path);
        //    Tasks = new ObservableCollection<Task>();
        //}
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
            {
                task.IsSelected = !task.IsSelected;
            }
        }
    }


}
