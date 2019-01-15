using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShacoBox
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            TimeUpdateThread = new Thread(RealTimeUpdate);
            TimeUpdateThread.IsBackground = true;
            TimeUpdateThread.Start();
            this.DataContext = this;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        string _ShowTime;
        public string ShowTime
        {
            get
            { return _ShowTime; }
            set
            {
                _ShowTime = value;
                OnPropertyChanged("ShowTime");
            }
        }
        public Thread TimeUpdateThread { get; set; }
        void RealTimeUpdate()
        {
            while (true)
            {
                ShowTime = DateTime.Now.ToString("HH:MM:ss ");
                Thread.Sleep(100);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
