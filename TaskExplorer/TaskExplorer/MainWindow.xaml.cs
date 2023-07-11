using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskExplorer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ObservableCollection<Task>? Tasks { get; set; }

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
}
