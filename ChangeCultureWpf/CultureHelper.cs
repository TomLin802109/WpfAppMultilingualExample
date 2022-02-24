using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Markup;

namespace MultilingualExample
{
    public class CulturesHelper
    {
        //private static bool _isFoundInstalledCultures = false;

        private static string _resourcePrefix = "Culture";

        private static string _culturesFolder = "Culture";

        private static List<CultureInfo> _supportedCultures = null;

        //public CulturesHelper()
        //{
        //    if (!_isFoundInstalledCultures)
        //    {
        //        ListSupportedCulture();

        //        if (_supportedCultures.Count > 0 && Properties.Settings.Default.DefaultCulture != null)
        //        {
        //            ChangeCulture(Properties.Settings.Default.DefaultCulture);
        //        }

        //        _isFoundInstalledCultures = true;
        //    }
        //}

        public static List<CultureInfo> ListSupportedCulture()
        {
            _supportedCultures = new List<CultureInfo>();
            List<string> files = Directory.GetFiles(string.Format("{0}\\{1}", System.Windows.Forms.Application.StartupPath, _culturesFolder))
                    .Where(s => s.Contains(_resourcePrefix) && s.ToLower().EndsWith("xaml")).ToList();

            foreach (string file in files)
            {
                try
                {
                    string cultureName = file.Substring(file.IndexOf(".") + 1).Replace(".xaml", "");
                    var cultureInfo = CultureInfo.GetCultureInfo(cultureName);
                    if (cultureInfo != null)
                    {
                        _supportedCultures.Add(cultureInfo);
                    }
                }
                catch (ArgumentException ex)
                {
                    var m = ex.Message;
                }
            }
            return _supportedCultures;
        }

        public static void ChangeCulture(CultureInfo culture)
        {
            if (_supportedCultures == null) ListSupportedCulture();
            if (_supportedCultures.Contains(culture))
            {
                WpfControlLibrary1.DllUserControl1.ChangeCulture(culture);
                string LoadedFileName = string.Format("{0}\\{1}\\{2}.{3}.xaml", 
                                                      AppDomain.CurrentDomain.BaseDirectory, 
                                                      _culturesFolder, _resourcePrefix, culture.Name);

                FileStream fileStream = new FileStream(@LoadedFileName, FileMode.Open);
                
                ResourceDictionary resourceDictionary = XamlReader.Load(fileStream) as ResourceDictionary;
                
                System.Windows.Application.Current.MainWindow.Resources.MergedDictionaries.Add(resourceDictionary);

                Properties.Settings.Default.DefaultCulture = culture;
                Properties.Settings.Default.Save();
            }
        }

    }
}
