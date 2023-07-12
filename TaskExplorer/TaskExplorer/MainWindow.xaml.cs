﻿using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using TaskClass;
using StatusEnum;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;

namespace TaskExplorer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<Task>? Tasks { get; set; }
    private readonly string path = "assets\\tasks.json";

    private int selectedIndex;

    private int SelectedIndex
    {
        get => selectedIndex;
        set => this.PropertyChangeMethod(out selectedIndex, value);
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
    }
    protected void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
    {
        field = value;

        if (this.PropertyChanged != null)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }
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
            //Console.WriteLine(SelectedIndex);
            if (listView.SelectedItem is ListViewItem item)
            {
                //item.IsEnabled.

                //Task newTask = new Task(task.Text, task.Status, task.CreationDate);
                //newTask.IsSelected = true;
                //task = newTask;
                //task.IsSelected = true;
                //Console.WriteLine(task.IsSelected);
            }
        }
    }


}
