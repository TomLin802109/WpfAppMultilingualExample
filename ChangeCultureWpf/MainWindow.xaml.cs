using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace MultilingualExample
{
    class ViewModel: INotifyPropertyChanged
    {
        private List<CultureInfo> availableCultures = new List<CultureInfo>();
        public List<CultureInfo> AvailableCultures
        {
            get => availableCultures;
            set
            {
                availableCultures = value;
                OnPropertyChanged();
            }
        }

        public List<string> AvailableCultureString
        {
            get => AvailableCultures.Select(v => v.NativeName).ToList();
        }

        private int selectedCultureIndex = 0;
        public int SelectedCultureIndex 
        {
            get => selectedCultureIndex;
            set
            {
                selectedCultureIndex = value;
                CulturesHelper.ChangeCulture(AvailableCultures[SelectedCultureIndex]);
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Reference website
    /// https://dotblogs.com.tw/ouch1978/2011/07/29/wpf-globalization-resourcedictionary
    /// https://dotblogs.com.tw/ouch1978/2011/07/28/wpf-globalization-resources-with-objectdataprovider
    /// Dynamic language resource switch testing in MainWindow and UserControl1 are success.
    /// But switch testing in DllUserControl is fail.
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _vm = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            _vm.AvailableCultures = CulturesHelper.ListSupportedCulture();
            this.DataContext = _vm;
        }
    }
}
