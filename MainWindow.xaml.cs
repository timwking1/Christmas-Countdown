using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Input;

namespace xmascd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class ChristmasCountdown : Window
    {
        const int christmasMonth = 12;
        const int christmasDay = 25; //Our Magic Numbers for Christmas day :)
        private Countdown countdown; //Countdown instance

        public ChristmasCountdown()
        {
            InitializeComponent();

            DateTime dCurrentDate = DateTime.Now;
            DateTime dChristmasDate = new DateTime(dCurrentDate.Year, christmasMonth, christmasDay, 0, 0, 0); //Convert our constants to DateTime format with correct year

            countdown = new Countdown(dCurrentDate, dChristmasDate, 1000); //Create our countdown instance
            countdown.eCountdownTick += UpdateUI; //Subscribe the UpdateUI function to the Countdown tick event
        }
        public void UpdateUI(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() => //Thread dispatcher needed as countdown tick event is on another thread.
            {
                dayDisplay.Text = countdown.tCountdownTimeRemaining.Days.ToString() + " Days";
                hourDisplay.Text = countdown.tCountdownTimeRemaining.Hours.ToString() + " Hours";
                minuteDisplay.Text = countdown.tCountdownTimeRemaining.Minutes.ToString() + " Minutes";
                secondDisplay.Text = countdown.tCountdownTimeRemaining.Seconds.ToString() + " Seconds";
            });
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) //Window Dragging
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void ContextMenuClose_Click(object sender, RoutedEventArgs e) //Context Menu
        {
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) //X Button
        {
            this.Close();
        }
    }
}