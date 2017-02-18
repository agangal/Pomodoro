using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Pomodoro.Helpers
{
    public class Settings
    {

        public static int CompletedPomodoro
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["CompletedPomodoro"] == null)
                {
                    return 0;
                }
                else
                {
                    int res;
                    int.TryParse(ApplicationData.Current.LocalSettings.Values["CompletedPomodoro"].ToString(), out res);
                    return (res);
                }
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values["CompletedPomodoro"] = value.ToString();
            }
        }
        public static long TimerLengthInSec
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["TimerLengthInSec"] == null)
                {
                    return Settings.PomodoroTimerLengthInSec;
                }
                else
                {
                    long res;
                    long.TryParse(ApplicationData.Current.LocalSettings.Values["TimerLengthInSec"].ToString(), out res);
                    return (res);
                }
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values["TimerLengthInSec"] = value.ToString();
            }
        }

        //public static long RemainingTimerInSec
        //{
        //    get
        //    {
        //        if (ApplicationData.Current.LocalSettings.Values["RemainingTimerInSec"] == null)
        //        {
        //            return Settings.PomodoroTimerLengthInSec;
        //        }
        //        else
        //        {
        //            long res;
        //            long.TryParse(ApplicationData.Current.LocalSettings.Values["RemainingTimerInSec"].ToString(), out res);
        //            return (res);
        //        }
        //    }

        //    set
        //    {
        //        ApplicationData.Current.LocalSettings.Values["RemainingTimerInSec"] = value.ToString();
        //    }
        //}

        public static long TickWindowBegin
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["TickWindowBegin"] == null)
                {
                    return 0;
                }
                else
                {
                    long res;
                    long.TryParse(ApplicationData.Current.LocalSettings.Values["TickWindowBegin"].ToString(), out res);
                    return (res);
                }
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values["TickWindowBegin"] = value.ToString();
            }
        }

        public static long ElapsedSeconds
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["ElapsedSeconds"] == null)
                {
                    return 0;
                }
                else
                {
                    long res;
                    long.TryParse(ApplicationData.Current.LocalSettings.Values["ElapsedSeconds"].ToString(), out res);
                    return (res);
                }
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values["ElapsedSeconds"] = value.ToString();
            }
        }

        public static long ShortTimerBreakInSec
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["ShortTimerBreakInSec"] == null)
                {
                    return 5;
                }
                else
                {
                    long res;
                    long.TryParse(ApplicationData.Current.LocalSettings.Values["ShortTimerBreakInSec"].ToString(), out res);
                    return (res);
                }
            }

            //set
            //{
            //    ApplicationData.Current.LocalSettings.Values["ShortTimerBreakInSec"] = value.ToString();
            //}
        }

        public static long PomodoroTimerLengthInSec
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["ShortTimerBreakInSec"] == null)
                {
                    return 120;
                }
                else
                {
                    long res;
                    long.TryParse(ApplicationData.Current.LocalSettings.Values["ShortTimerBreakInSec"].ToString(), out res);
                    return (res);
                }
            }

            //set
            //{
            //    ApplicationData.Current.LocalSettings.Values["ShortTimerBreakInSec"] = value.ToString();
            //}
        }

        public static long LongTimerBreakInSec
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["LongTimerBreakInSec"] == null)
                {
                    return 15;
                }
                else
                {
                    long res;
                    long.TryParse(ApplicationData.Current.LocalSettings.Values["LongTimerBreakInSec"].ToString(), out res);
                    return (res);
                }
            }

            set
            {
                ApplicationData.Current.LocalSettings.Values["LongTimerBreakInSec"] = value.ToString();
            }
        }

        public static string CurrentPomodoroStep
        {
            get
            {
                if (ApplicationData.Current.LocalSettings.Values["CurrentPomodoroStep"] == null)
                {
                    return (App.GetDescription(PomodoroSteps.Pomodoro_Step0));
                }
                else
                {
                    return (ApplicationData.Current.LocalSettings.Values["CurrentPomodoroStep"].ToString());
                }
            }
            set
            {
                ApplicationData.Current.LocalSettings.Values["CurrentPomodoroStep"] = value;
            }
        }
    }
}
