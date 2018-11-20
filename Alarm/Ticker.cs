using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.ComponentModel;
using System.Windows.Input;

namespace Alarm
{
    class Ticker : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Closed;
        DispatcherTimer timer;
        bool sound;
        public bool Sound { get { return sound; } set { sound = value; OnPropertyChanged("Sound"); } }
        double timeLeft;
        public double TimeLeft
        {
            get { return timeLeft; }
            set
            {
                timeLeft = value;
                dday = DateTime.Now + TimeSpan.FromMilliseconds(timeLeft);
                if (timeLeft > 0) timer.Start();
                OnPropertyChanged("TimeLeft");
            }
        }
        DateTime dday;              // The time at which to stop the timer
        readonly ICommand closeCommand;
        public ICommand CloseCommand { get { return closeCommand; } }

        public Ticker()
        {
            Sound = false;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            closeCommand = new DelegateCommand(close);
            timeLeft = 0;
        }

        void close(object o)
        {
            timer.Stop();
            timer.Tick -= timer_Tick;
            OnClosed();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            double t = (dday - DateTime.Now).TotalMilliseconds;
            if (t <= 0)
            {
                // Stop the timer when we reach dday and set TimeLeft to zero
                TimeLeft = 0;
                timer.Stop();
            }
            else
            {
                // The timer ticked so don't update dday
                timeLeft = t;
                OnPropertyChanged("TimeLeft");
            }
        }

        protected virtual void OnClosed()
        {
            if (Closed != null)
                Closed(this, EventArgs.Empty);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
