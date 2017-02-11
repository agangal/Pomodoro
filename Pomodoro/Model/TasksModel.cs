using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Model
{
    public class TasksModel
    {
        public string task { get; set; }
        public List<TaskHistory> histiry { get; set; }
    }

    public class TaskHistory
    {
        public TimeSpan timespan_completed { get; set; }
        public timespan_type type { get; set; }
    }

    public enum timespan_type
    {
       pomodoro,
       pomodoro_break
    }
}
