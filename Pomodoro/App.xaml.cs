using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Pomodoro
{
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(Pages.TimerPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        public static string GetDescription(PomodoroSteps value)
        {
            DisplayAttribute attribute = value.GetType()
            .GetField(value.ToString())
            .GetCustomAttributes(typeof(DisplayAttribute), false)
            .SingleOrDefault() as DisplayAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static PomodoroSteps GetEnumFromDescription(string description)
        {
           switch (description)
            {
                case "Step0":
                    return (PomodoroSteps.Pomodoro_Step0);
                    break;
                case "Break0":
                    return (PomodoroSteps.Pomodoro_Break0);
                    break;
                case "Step1":
                    return (PomodoroSteps.Pomodoro_Step1);
                    break;
                case "Break1":
                    return (PomodoroSteps.Pomodoro_Break1);
                    break;
                case "Step2":
                    return (PomodoroSteps.Pomodoro_Step2);
                    break;
                case "Break2":
                    return (PomodoroSteps.Pomodoro_Break2);
                    break;
                case "Step3":
                    return (PomodoroSteps.Pomodoro_Step3);
                    break;
                case "Break3":
                    return (PomodoroSteps.Pomodoro_Break3);
                    break;
                case "Stop":
                    return PomodoroSteps.Pomodoro_Stop;
                    break;
                default: return PomodoroSteps.Pomodoro_Break0;
                    break;
            }
        }
    }

    public enum PomodoroSteps
    {
        [Display(Description = "Step0")]
        Pomodoro_Step0,
        [Display(Description = "Break0")]
        Pomodoro_Break0,
        [Display(Description = "Step1")]
        Pomodoro_Step1,
        [Display(Description = "Break1")]
        Pomodoro_Break1,
        [Display(Description = "Step2")]
        Pomodoro_Step2,
        [Display(Description = "Break2")]
        Pomodoro_Break2,
        [Display(Description = "Step3")]
        Pomodoro_Step3,
        [Display(Description = "Break3")]
        Pomodoro_Break3,
        [Display(Description = "Break3")]
        Pomodoro_Stop
    }

   
    public enum PomodoroBreakType
    {
        Pomodoro_ShortBreak,
        Pomodor_LongBreak
    }
}
