using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using TaskExplorer.models;

namespace TaskExplorer.views;

public partial class AddWindow : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly ObservableCollection<Task>? tasks;

    private static int maxNameSymbols = 16;
    private static int maxTextSymbols = 700;

    public ObservableCollection<STATUS> Statuses { get; set; } = new ObservableCollection<STATUS>();

    private STATUS taskInputStatus;

    private string? taskNameInputMessage;
    private string? taskTextInputMessage;
    private bool canAdd;

    public STATUS TaskInputStatus
    {
        get => taskInputStatus;
        set => taskInputStatus = value;
    }

    public string? TaskNameInputMessage
    {
        get => taskNameInputMessage;
        set {
            if (value == null)
                throw new ArgumentNullException(nameof(taskNameInputMessage));

            PropertyChangeMethod(out taskNameInputMessage, value);
        }
    }

    public string? TaskTextInputMessage
    {
        get => taskTextInputMessage;
        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(taskTextInputMessage));

            PropertyChangeMethod(out taskTextInputMessage, value);
        }
    }

    public bool CanAdd
    {
        get => canAdd;
        set => PropertyChangeMethod(out canAdd, value);
    }

    public AddWindow()
    {
        InitializeComponent();

        this.DataContext = this;

        Statuses.Add(STATUS.Todo);
        Statuses.Add(STATUS.InProgress);
    }

    public AddWindow(ObservableCollection<Task>? tasks) : this()
    {
        this.CanAdd = false;
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
        string taskInputText = ConvertRichTextBoxContentsToString(this.TaskRichTextBox);
        this.tasks?.Add(new Task(name: this.NameInputTextBox.Text, text: taskInputText, status: TaskInputStatus));

        this.Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e) => this.Close();

    private string ConvertRichTextBoxContentsToString(RichTextBox richTextBox)
    {
        TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
        return textRange.Text;
    }

    private void InputChanged(object sender, TextChangedEventArgs e)
    {
        string taskInputText = ConvertRichTextBoxContentsToString(this.TaskRichTextBox);
        string taskInputName = NameInputTextBox.Text;


        EnableButton(taskInputName, taskInputText);
    }

    private bool NameValidation(string name)
    {
        if (name.Length <= 0)
        {
            this.TaskNameInputMessage = "Name cannot be empty!";
            return false;
        }

        else if (name.Length > maxNameSymbols)
        {
            this.TaskNameInputMessage = "Name is too long!";
            return false;
        }

        this.TaskNameInputMessage = String.Empty;
        return true;
    }

    private bool TextValidation(string text)
    {
        if (text.Length > maxTextSymbols)
        {
            this.CanAdd = false;
            this.TaskTextInputMessage = "Text is too long!";
            return false;
        }

        this.TaskTextInputMessage = String.Empty;
        return true;
    }

    private void EnableButton(string name, string text)
    {
        bool isTextValid = TextValidation(text);
        bool isNameValid = NameValidation(name);

        this.CanAdd = isTextValid && isNameValid;
    }

}
