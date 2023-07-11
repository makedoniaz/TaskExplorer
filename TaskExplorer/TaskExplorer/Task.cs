using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using StatusEnum;

namespace TaskClass;

public class Task
{
	private string? text;
	private STATUS status;
	private DateTime creationDate;

	public string Text
	{
		get { return text; }
		set { text = value; }
	}

	public STATUS Status
    {
		get { return status; }
		set { status = value; }
	}

	public DateTime CreationDate
    {
		get { return creationDate; }
		set { creationDate = value; }
	}
}
