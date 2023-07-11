using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using StatusEnum;

namespace TaskClass;

public class Task
{
	private string name;
	private string text;
	private STATUS status;
	private string creationDate;

	public string Name
	{
		get { return name; }
		set { name = value; }
	}

	public string Text
	{
		get { return text; }
		set { 
			if (value == null)
				throw new ArgumentNullException(nameof(text));

			text = value;
		}
	}

	public STATUS Status
    {
		get { return status; }
		set { status = value; }
	}

	public string CreationDate
    {
		get { return creationDate; }
		set { creationDate = value; }
	}

	public Task(string text, STATUS status, string creationDate)
    {
        Text = text;
        Status = status;
        CreationDate = creationDate;
    }
    public override string ToString() => $"{Text} - {Status} - {creationDate}";
}
