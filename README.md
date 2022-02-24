#Example of Multilingual of WPF App

*Reference website*
* *https://dotblogs.com.tw/ouch1978/2011/07/29/wpf-globalization-resourcedictionary*
* *https://dotblogs.com.tw/ouch1978/2011/07/28/wpf-globalization-resources-with-objectdataprovider*

Dynamic language resource switch testing in MainWindow and UserControl1 are success, but fail in DllUserControl(additional dll library).
The probable reasons are showing below,

* It's not able to import culture setting for DllUserControl which is trigger by MainWindow.
* It's not able to create a new culture resource in root folder of MainWindow.
