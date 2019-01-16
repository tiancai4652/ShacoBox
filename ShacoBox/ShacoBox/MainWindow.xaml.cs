using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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

            IntPtr hWnd = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            IntPtr hWndProgMan = FindWindow("Progman", "Program Manager");
            SetParent(hWnd, hWndProgMan);
            var handle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            IntPtr hprog = FindWindowEx(
                FindWindowEx(
                    FindWindow("Progman", "Program Manager"),
                    IntPtr.Zero, "SHELLDLL_DefView", ""
                ),
                IntPtr.Zero, "SysListView32", "FolderView"
            );
            SetWindowLong(handle, GWL_HWNDPARENT, hprog);
        }

        string _ShowTime;
        public string ShowTime
        {
            get
            {
                return _ShowTime;
            }
            set
            {
                _ShowTime = value;
                OnPropertyChanged("ShowTime");
            }
        }
        public Thread TimeUpdateThread { get; set; }
        private void RealTimeUpdate()
        {
            while (true)
            {
                ShowTime = DateTime.Now.ToString(Configuration.ShowFormat);
                Thread.Sleep(100);
            }
        }



        #region Import
        [DllImport("user32.dll", SetLastError = true)]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpWindowClass, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);
        const int GWL_HWNDPARENT = -8;
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        #endregion

        #region Event

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Escape))
            {
                if (MessageBox.Show("确认退出吗?", "温馨提示", MessageBoxButton.OKCancel).Equals(MessageBoxResult.OK))
                {
                    if (TimeUpdateThread.IsAlive)
                    {
                        TimeUpdateThread.Abort();
                    }
                    while (TimeUpdateThread.ThreadState != ThreadState.Aborted)
                    {
                        Thread.Sleep(100);
                    }
                    this.Close();
                }
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
