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
        public int current_pomodoro { get; set; }
        public List<TaskHistory> history { get; set; }
    }

    public class TaskHistory
    {
        public DateTime startTime;
        public TimeSpan timespan_completed { get; set; }
        public PomodoroSteps Step { get; set; }
        public timespan_type PomodorType { get; set; }
    }

    public enum timespan_type
    {
       pomodoro,
       pomodoro_break
    }
}
