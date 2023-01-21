using System;
using System.Collections.Generic;
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

namespace Yu3zx.FactoryLine
{
    /// <summary>
    /// WinTagTemplate.xaml 的交互逻辑
    /// </summary>
    public partial class WinTagTemplate : Window
    {
        public WinTagTemplate()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gridBg.Visibility = Visibility.Collapsed;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
