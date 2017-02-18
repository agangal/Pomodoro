using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Pomodoro.Pages
{
    using System.Diagnostics;
    using Pomodoro.Helpers;
    using Microsoft.Toolkit.Uwp.UI.Animations;
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimerPage : Page
    {
        private DispatcherTimer Timer;
        private long elapsedSeconds;
        private long elapsedTicks;
        private bool fadestatus = false; // currently visible
        public TimerPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            long remtime = UpdateTimerTrackers();
            if (remtime == 0 && Settings.CurrentPomodoroStep == App.GetDescription(PomodoroSteps.Pomodoro_Stop))
            {

            }
            Settings.TickWindowBegin = DateTime.UtcNow.Ticks;
           // string str = App.GetDescription(PomodoroSteps.Pomodoro_Step0);
           // System.Diagnostics.Debug.WriteLine(str);
        }

        private void StartFreshTimer()
        {
            try
            {
                Settings.CompletedPomodoro = 0;
                CreateTimer();
            }
            catch (Exception ex)
            {
                //System.Diagnostics.Tracing.
            }
        }

        private void CreateTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 1);
            Settings.TickWindowBegin = DateTime.UtcNow.Ticks;
            Timer.Start();
            Timer_Tick(null, null);
        }

        private void StartNewPomodoro()
        {
            if (Settings.CurrentPomodoroStep == App.GetDescription(PomodoroSteps.Pomodoro_Step0))
            {
                StartFreshTimer();
            }
            else if (Settings.CurrentPomodoroStep == App.GetDescription(PomodoroSteps.Pomodoro_Stop))
            {
                StopTimer();
            }
            else
            {
                
                CreateTimer();
            }
        }
        private void StopTimer()
        {
            try
            {
                Timer.Tick -= Timer_Tick;
                Timer.Stop();
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Timer_Tick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, object e)
        {
            Debug.WriteLine("---Timer_Tick---");
            long remtime = UpdateTimerTrackers();
            //long remtime = Settings.RemainingTimerInSec;
            TimerTextBlock.Text = ConvertSecToMMSS(remtime);
            //Settings.RemainingTimerInSec = remtime;            
        }


        private long UpdateTimerTrackers()
        {
            elapsedTicks = DateTime.UtcNow.Ticks - Settings.TickWindowBegin;
            elapsedSeconds = (long)TimeSpan.FromTicks(elapsedTicks).TotalSeconds + Settings.ElapsedSeconds;
            long remtime = 0;
            if (elapsedSeconds >= Settings.TimerLengthInSec)
            {
                MarkPomodoroStepComplete();
                //Set everything to 0;
                //Save data
            }
            else
            {
                remtime = Settings.TimerLengthInSec - elapsedSeconds;
                Debug.WriteLine("Elapsed seconds : " + elapsedSeconds);
                Debug.WriteLine("Remaining Time : " + remtime);
            }
            UpdatePomodoros();
            return remtime;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Timer.Tick -= Timer_Tick;
            Timer.Stop();
        }

        private async void UpdatePomodoros()
        {
            if (Settings.CurrentPomodoroStep == App.GetDescription(PomodoroSteps.Pomodoro_Step0))
            {
                pomodoro_1.Visibility = Visibility.Visible;
                pomodoro_2.Visibility = Visibility.Visible;
                pomodoro_3.Visibility = Visibility.Visible;
                done_0.Visibility = Visibility.Collapsed;
                done_1.Visibility = Visibility.Collapsed;
                done_2.Visibility = Visibility.Collapsed;
                done_3.Visibility = Visibility.Collapsed;
                if (fadestatus) // it is hidden
                {
                    await pomodoro_0.Fade(1, 500).StartAsync();
                    fadestatus = false;
                }
                else
                {
                    await pomodoro_0.Fade(0, 500).StartAsync();
                    fadestatus = true;
                }
            }
            else if (Settings.CurrentPomodoroStep == App.GetDescription(PomodoroSteps.Pomodoro_Break0))
            {
                pomodoro_1.Visibility = Visibility.Visible;
                pomodoro_2.Visibility = Visibility.Visible;
                pomodoro_3.Visibility = Visibility.Visible;
                done_0.Visibility = Visibility.Visible;
                done_1.Visibility = Visibility.Collapsed;
                done_2.Visibility = Visibility.Collapsed;
                done_3.Visibility = Visibility.Collapsed;
                pomodoro_0.Visibility = Visibility.Collapsed;
            }
            else if (Settings.CurrentPomodoroStep == App.GetDescription(PomodoroSteps.Pomodoro_Step1))
            {
                pomodoro_0.Visibility = Visibility.Collapsed;
                // pomodoro_.Visibility = Visibility.Visible;
                pomodoro_2.Visibility = Visibility.Visible;
                pomodoro_3.Visibility = Visibility.Visible;
                done_0.Visibility = Visibility.Visible;
                done_1.Visibility = Visibility.Collapsed;
                done_2.Visibility = Visibility.Collapsed;
                done_3.Visibility = Visibility.Collapsed;
                
                if (fadestatus) // it is hidden
                {
                    await pomodoro_1.Fade(1, 500).StartAsync();
                    fadestatus = false;
                }
                else
                {
                    await pomodoro_1.Fade(0, 500).StartAsync();
                    fadestatus = true;
                }
            }
            else if (Settings.CurrentPomodoroStep == App.GetDescription(PomodoroSteps.Pomodoro_Break1))
            {
                pomodoro_1.Visibility = Visibility.Collapsed;
                pomodoro_2.Visibility = Visibility.Visible;
                pomodoro_3.Visibility = Visibility.Visible;
                done_0.Visibility = Visibility.Visible;
                done_1.Visibility = Visibility.Visible;
                done_2.Visibility = Visibility.Collapsed;
                done_3.Visibility = Visibility.Collapsed;
                pomodoro_0.Visibility = Visibility.Collapsed;
            } 
            else if (Settings.CurrentPomodoroStep == App.GetDescription(PomodoroSteps.Pomodoro_Step2))
            {
                pomodoro_0.Visibility = Visibility.Collapsed;
                pomodoro_1.Visibility = Visibility.Collapsed;
                //pomodoro_2.Visibility = Visibility.Visible;
                pomodoro_3.Visibility = Visibility.Visible;
                done_0.Visibility = Visibility.Visible;
                done_1.Visibility = Visibility.Visible;
                done_2.Visibility = Visibility.Collapsed;
                done_3.Visibility = Visibility.Collapsed;
                if (fadestatus) // it is hidden
                {
                    await pomodoro_2.Fade(1, 500).StartAsync();
                    fadestatus = false;
                }
                else
                {
                    await pomodoro_2.Fade(0, 500).StartAsync();
                    fadestatus = true;
                }
            }
            else if (Settings.CurrentPomodoroStep == App.GetDescription(PomodoroSteps.Pomodoro_Break2))
            {
                pomodoro_1.Visibility = Visibility.Collapsed;
                pomodoro_2.Visibility = Visibility.Collapsed;
                pomodoro_3.Visibility = Visibility.Visible;
                done_0.Visibility = Visibility.Visible;
                done_1.Visibility = Visibility.Visible;
                done_2.Visibility = Visibility.Visible;
                done_3.Visibility = Visibility.Collapsed;
                pomodoro_0.Visibility = Visibility.Collapsed;
            }
            else if (Settings.CurrentPomodoroStep == App.GetDescription(PomodoroSteps.Pomodoro_Step3))
            {
                pomodoro_0.Visibility = Visibility.Collapsed;
                pomodoro_1.Visibility = Visibility.Collapsed;
                pomodoro_2.Visibility = Visibility.Collapsed;
                //pomodoro_3.Visibility = Visibility.Visible;
                done_0.Visibility = Visibility.Visible;
                done_1.Visibility = Visibility.Visible;
                done_2.Visibility = Visibility.Visible;
                done_3.Visibility = Visibility.Collapsed;
                if (fadestatus) // it is hidden
                {
                    await pomodoro_1.Fade(1, 500).StartAsync();
                    fadestatus = false;
                }
                else
                {
                    await pomodoro_1.Fade(0, 500).StartAsync();
                    fadestatus = true;
                }
            }
            else if (Settings.CurrentPomodoroStep == App.GetDescription(PomodoroSteps.Pomodoro_Break3))
            {
                pomodoro_1.Visibility = Visibility.Collapsed;
                pomodoro_2.Visibility = Visibility.Collapsed;
                pomodoro_3.Visibility = Visibility.Collapsed;
                done_0.Visibility = Visibility.Visible;
                done_1.Visibility = Visibility.Visible;
                done_2.Visibility = Visibility.Visible;
                done_3.Visibility = Visibility.Visible;
                pomodoro_0.Visibility = Visibility.Collapsed;
            }
            else if (Settings.CurrentPomodoroStep == App.GetDescription(PomodoroSteps.Pomodoro_Stop))
            {
                pomodoro_1.Visibility = Visibility.Collapsed;
                pomodoro_2.Visibility = Visibility.Collapsed;
                pomodoro_3.Visibility = Visibility.Collapsed;
                done_0.Visibility = Visibility.Visible;
                done_1.Visibility = Visibility.Visible;
                done_2.Visibility = Visibility.Visible;
                done_3.Visibility = Visibility.Visible;
                pomodoro_0.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Start button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartTime_Click(object sender, RoutedEventArgs e)
        {
            Settings.TickWindowBegin = DateTime.UtcNow.Ticks;
            //Settings.RemainingTimerInSec = Settings.TimerLengthInSec;
            StartTime.Visibility = Visibility.Collapsed;
            AllOptionsGrid.Visibility = Visibility.Visible;
            StartNewPomodoro();
        }
      
        private void PauseTimer_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("---PauseTimer---");
            PauseTimer.Visibility = Visibility.Collapsed;
            ContinueTimer.Visibility = Visibility.Visible;
            Timer.Tick -= Timer_Tick;
            //long elapsedTicks = DateTime.UtcNow.Ticks - Settings.TickWindowBegin;
            Settings.ElapsedSeconds = elapsedSeconds;
            Debug.WriteLine("Tick Window Begins - " + TimeSpan.FromTicks(Settings.TickWindowBegin).TotalSeconds.ToString());
            Debug.WriteLine("Elapsed Seconds - " + Settings.ElapsedSeconds);
            
        }

        private void ContinueTimer_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("---ContinueTimer---");
            Settings.TickWindowBegin = DateTime.UtcNow.Ticks;
            Debug.WriteLine("Tick Window Begin Seconds - " + TimeSpan.FromTicks(Settings.TickWindowBegin).TotalSeconds.ToString());
            Timer.Tick += Timer_Tick;
            PauseTimer.Visibility = Visibility.Visible;
            ContinueTimer.Visibility = Visibility.Collapsed;
        }

        private void NextPomodoro_Click(object sender, RoutedEventArgs e)
        {
            MarkPomodoroStepComplete();
        }

        /// <summary>
        /// Convert time to string
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public string ConvertSecToMMSS(long seconds)
        {
            string mm = ((int)seconds / 60).ToString();
            if (mm.Length <= 1)
            {
                mm = "0" + mm;
            }
            string ss = ((int)seconds % 60).ToString();
            if (ss.Length <= 1)
            {
                ss = "0" + ss;
            }
            //string res = ((seconds / 60).ToString().Length <= 1 ? ("0" + (seconds / 60).ToString()) : ((seconds / 60).ToString()) + " : " + ((seconds % 60).ToString().Length <= 1 ? ("0" + (seconds % 60).ToString()) : (seconds % 60).ToString()));
            string res = mm + " : " + ss;
            return res;
        }

        public void MarkPomodoroStepComplete()
        {
            StopTimer();
            Settings.CompletedPomodoro = Settings.CompletedPomodoro + 1;
            NextPomodoroStep();
            StartNewPomodoro();
        }

        public void NextPomodoroStep()
        {
            switch (Settings.CurrentPomodoroStep)
            {
                case "Step0":
                    Settings.TimerLengthInSec = Settings.ShortTimerBreakInSec;
                    Settings.CurrentPomodoroStep = "Break0";
                    break;
                case "Break0":
                    Settings.TimerLengthInSec = Settings.PomodoroTimerLengthInSec;
                    Settings.CurrentPomodoroStep = "Step1";
                    break;
                case "Step1":
                    Settings.TimerLengthInSec = Settings.ShortTimerBreakInSec;
                    Settings.CurrentPomodoroStep = "Break1";
                    break;
                case "Break1":
                    Settings.TimerLengthInSec = Settings.PomodoroTimerLengthInSec;
                    Settings.CurrentPomodoroStep = "Step2";
                    break;
                case "Step2":
                    Settings.TimerLengthInSec = Settings.ShortTimerBreakInSec;
                    Settings.CurrentPomodoroStep = "Break2";
                    break;
                case "Break2":
                    Settings.TimerLengthInSec = Settings.PomodoroTimerLengthInSec;
                    Settings.CurrentPomodoroStep = "Step3";
                    break;
                case "Step3":
                    Settings.TimerLengthInSec = Settings.ShortTimerBreakInSec;
                    Settings.CurrentPomodoroStep = "Break3";
                    break;
                case "Break3":
                    Settings.TimerLengthInSec = Settings.PomodoroTimerLengthInSec;
                    Settings.CurrentPomodoroStep = "Stop";
                    break;
                case "Stop":
                    Settings.TimerLengthInSec = Settings.PomodoroTimerLengthInSec;
                    Settings.CurrentPomodoroStep = "Step0";
                    break;
                default: break;
            }
        }
        public void MarkTaskComplete()
        {
            Settings.TimerLengthInSec = Settings.PomodoroTimerLengthInSec;
        }

    }
}
