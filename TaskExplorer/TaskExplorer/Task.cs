using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StatusEnum;

namespace TaskClass;

public class Task : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string name;
	private string text;
	private STATUS status;
	private string creationDate;

	private bool isSelected;

	public string Name
	{
		get { return name; }
		set { this.PropertyChangeMethod(out name, value); }
	}

	public string Text
	{
		get { return text; }
		set { 
			if (value == null)
				throw new ArgumentNullException(nameof(text));

            this.PropertyChangeMethod(out text, value);
        }
	}

	public STATUS Status
    {
		get { return status; }
		set { this.PropertyChangeMethod(out status, value); }
	}

	public string CreationDate
    {
		get { return creationDate; }
		set { this.PropertyChangeMethod(out creationDate, value); }
	}

	public bool IsSelected { 
		get { return isSelected; }
		set { this.PropertyChangeMethod(out isSelected, value); }
	}

	public Task(string text, STATUS status, string creationDate)
    {
        Text = text;
        Status = status;
        CreationDate = creationDate;
    }

    protected void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
    {
        field = value;

        if (this.PropertyChanged != null)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    public override string ToString() => $"{Text} - {Status} - {creationDate}";
}
