using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using TimeAndTimePeriod;

namespace TimeAndTimePeriodApp
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer? timer;
        private DispatcherTimer? timer2;
        public MainWindow()
        {
            InitializeComponent();
            Clock();
            TimerDisplay.Text = timerTime.ToString();
            InitializeTimer();
        }

        #region Clock
        private void Clock()
        {
            DateInClock.Text = DateTime.Now.ToString("dd.MM.yyyy");
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Clock_Tick;
            timer.Start();
        }
        private void Clock_Tick(object sender, EventArgs e)
        {
            Time time = new Time(DateTime.Now.ToString("HH:mm:ss"));
            clock.Text = time.ToString();
        }
        #endregion

        #region Timer
        public TimePeriod timerTime = new TimePeriod(0);
        private bool isTimerRunning = false;
        private void Timer()
        {
            TimerDisplay.Text = timerTime.ToString();
        }

        private void InitializeTimer()
        {
            timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(1);
            timer2.Tick += Timer_Tick;
            isTimerRunning = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timerTime += new TimePeriod(0, 0, 1);
            TimerDisplay.Text = timerTime.ToString();
        }

        private void startstop_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerRunning)
            {
                timer2.Stop();
                startstop.Content = "START";
                startstop.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0, 139));
            }
            else
            {
                timer2.Start();
                startstop.Content = "STOP";
                startstop.Background = Brushes.DarkCyan;
            }

            isTimerRunning = !isTimerRunning;
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            timerTime = new TimePeriod(0);
            Timer();
        }
        #endregion
    }
}
