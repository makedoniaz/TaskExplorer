using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace TaskExplorer.models;

public class Task : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string? name;
    private string? text;
    private STATUS status;

    private DateTime creationDate;
    private string? creationDateStr;

    private bool isSelected;

    public string? Name
    {
        get => name;
        set {
            if (value == null)
                throw new ArgumentNullException(nameof(name));
            
            this.PropertyChangeMethod(out name, value); 
        }
    }

    public string? Text
    {
        get => text;
        set
        {
            this.PropertyChangeMethod(out text, value);
        }
    }

    public STATUS Status
    {
        get => status;
        set { this.PropertyChangeMethod(out status, value); }
    }

    public DateTime CreationDate
    {
        get => creationDate;
        set { this.PropertyChangeMethod(out creationDate, value); }
    }

    public string? CreationDateStr
    {
        get => creationDateStr;
        set { this.PropertyChangeMethod(out creationDateStr, value); }
    }

    public bool IsSelected
    {
        get => isSelected;
        set { this.PropertyChangeMethod(out isSelected, value); }
    }

    public Task(string? name, string? text, STATUS status)
    {
        Name = name;
        Text = text;
        Status = status;
        CreationDate = DateTime.Now;
        CreationDateStr = CreationDate.ToShortDateString();
    }

    protected void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
    {
        field = value;

        if (this.PropertyChanged != null)
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
    }

    public override string ToString() => $"{Name} - {Status} - {CreationDate}";
}
