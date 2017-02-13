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
            Settings.TickWindowBegin = DateTime.UtcNow.Ticks;         
        }

        private void StartFreshTimer()
        {
            try
            {
                Settings.CompletedPomodoro = 0;
                Timer = new DispatcherTimer();
                Timer.Tick += Timer_Tick;
                Timer.Interval = new TimeSpan(0, 0, 1);
                Timer.Start();
                Timer_Tick(null, null);
            }
            catch (Exception ex)
            {
                
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
            elapsedTicks = DateTime.UtcNow.Ticks - Settings.TickWindowBegin;
            elapsedSeconds = (long)TimeSpan.FromTicks(elapsedTicks).TotalSeconds + Settings.ElapsedSeconds;
            long remtime = 0;
            if (elapsedSeconds >= Settings.TimerLengthInSec)
            {
                Timer.Tick -= Timer_Tick;
                Timer.Stop();
                Settings.CompletedPomodoro = Settings.CompletedPomodoro + 1;
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
            //long remtime = Settings.RemainingTimerInSec;
            TimerTextBlock.Text = ConvertSecToMMSS(remtime);
            //Settings.RemainingTimerInSec = remtime;            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Timer.Tick -= Timer_Tick;
            Timer.Stop();
        }

        private async void UpdatePomodoros()
        {
            if (Settings.CompletedPomodoro == 0)
            {
                pomodoro_1.Visibility = Visibility.Visible;
                pomodoro_2.Visibility = Visibility.Visible;
                pomodoro_3.Visibility = Visibility.Visible;
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
        }

        private void StartTime_Click(object sender, RoutedEventArgs e)
        {
            Settings.TickWindowBegin = DateTime.UtcNow.Ticks;
            //Settings.RemainingTimerInSec = Settings.TimerLengthInSec;
            StartTime.Visibility = Visibility.Collapsed;
            AllOptionsGrid.Visibility = Visibility.Visible;
            StartFreshTimer();
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

    }
}
