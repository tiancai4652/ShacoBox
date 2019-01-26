using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace ShacoBox
{
    /// <summary>
    /// InputView.xaml 的交互逻辑
    /// </summary>
    public partial class InputView : Window, INotifyPropertyChanged
    {
        public InputView()
        {
            InitializeComponent();
            txtBox.Focus();
            this.DataContext = this;
        }

        string _TimeStr = DateTime.Now.AddMinutes(15).ToString(Configuration.ShowFormat);
        public string TimeStr
        {
            get
            {
                return _TimeStr;
            }
            set
            {
                _TimeStr = value;
            }
        }

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
