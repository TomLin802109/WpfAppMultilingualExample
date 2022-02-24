using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlLibrary1
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DllUserControl1 : UserControl
    {
        public DllUserControl1()
        {
            InitializeComponent();
        }

        public static void ChangeCulture(System.Globalization.CultureInfo culture)
        {
            string LoadedFileName = string.Format("{0}\\{1}\\{2}.{3}.xaml", System.AppDomain.CurrentDomain.BaseDirectory, "CultureResource", "Lib", culture.Name);
            var str = $"Lib.{culture.Name}.xaml";
            try
            {
                //FileStream fileStream = new FileStream(@LoadedFileName, FileMode.Open);

                //ResourceDictionary resourceDictionary = XamlReader.Load(fileStream) as ResourceDictionary;
                
                var res = new ResourceDictionary() { Source = new Uri(str, UriKind.Relative) };
                Application.Current.MainWindow.Resources.MergedDictionaries.Add(res);
            }
            catch(Exception ex)
            {
                //throw ex;
            }
        }
    }
}
