using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using TaskClass;
using StatusEnum;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace TaskExplorer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<Task>? Tasks { get; set; }
    private readonly string path = "assets\\tasks.json";

    private Task selectedItem;

    public Task SelectedItem
    {
        get { return selectedItem; }
        set { selectedItem = value; }
    }

    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = this;

        string json = File.ReadAllText(path);

        if (string.IsNullOrWhiteSpace(json))
            Tasks = new ObservableCollection<Task>();
        else
            Tasks = JsonSerializer.Deserialize<ObservableCollection<Task>>(json);
        
        //Console.WriteLine(tasks == null);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button addButton)
        {
            this.Tasks?.Add(new Task("abbb", STATUS.Todo, DateTime.Now.ToShortDateString()));
        }
    }

    private void SelectTask_Click(object sender, RoutedEventArgs e)
    {
        if (sender is ListView listView)
        {
            if(listView.SelectedItem is Task task)
            {
                task.IsSelected = true;
            }
        }
    }


}
