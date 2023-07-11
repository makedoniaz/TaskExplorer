using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using TaskClass;
using StatusEnum;
using System;

namespace TaskExplorer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<Task>? Tasks { get; set; }

    private readonly string path = "assets\\tasks.json";

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

        this.Tasks?.Add(new Task("ABOBAAFDASGASDGADSGASD", STATUS.InProcess, DateTime.Now.ToShortDateString()));
        // Console.WriteLine(Tasks.Count);
    }
}
