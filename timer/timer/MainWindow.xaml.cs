using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace timer
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private TimeSpan _timeRemaining;
        private bool _isPaused = false;


        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isPaused)
            {
                _timer.Start();
                _isPaused = false;
                return;
            }
            if (double.TryParse(MinutesTextBox.Text, out double minutes) && minutes > 0)
            {
                _timeRemaining = TimeSpan.FromMinutes(minutes);
                UpdateTimeDisplay();
                _timer.Start();
                MinutesTextBox.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректне число хвилин.", "Помилка вводу", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                _isPaused = true;
            }



        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _isPaused = false; 
            MinutesTextBox.IsEnabled = true;
            _timeRemaining = TimeSpan.Zero;
            UpdateTimeDisplay();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_timeRemaining.TotalSeconds > 0)
            {
                _timeRemaining = _timeRemaining.Subtract(TimeSpan.FromSeconds(1));
                UpdateTimeDisplay();
            }
            else
            {
                _timer.Stop();
                MinutesTextBox.IsEnabled = true;
                MessageBox.Show("Час вийшов!", "Сповіщення", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateTimeDisplay()
        {
            TimeDisplayTextBlock.Text = _timeRemaining.ToString(@"hh\:mm\:ss");
        }
    }
}