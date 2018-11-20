using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Alarm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtUserEntry_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (e.Key == Key.Enter)
                FocusManager.SetFocusedElement(this, this);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Border_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Fires as an attached event when Slider.ValueChanged is fired
            // Border.Tag is bound to cbxPopup.IsChecked in the XAML as a hack to access it here
            // Slider.Tag is already used for "Started" to avoid the animation firing when starting up
            Border b = (Border)sender;
            Slider s = e.Source as Slider;
            if (s == null)
                return;
            s.Tag = "Started";
            if (Keyboard.Modifiers == ModifierKeys.Shift || s.Value < s.TickFrequency)
                s.IsSnapToTickEnabled = false;
            else
                s.IsSnapToTickEnabled = true;
            if (s.Value == 0 && (bool)b.Tag == true)
                // ShowActivated is false so we should popup without taking focus
                WindowState = WindowState.Normal;
        }

        private void txtUserEntry_Loaded(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Focus();
        }
    }
}
