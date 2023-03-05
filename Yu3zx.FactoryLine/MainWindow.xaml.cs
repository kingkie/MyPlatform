using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Yu3zx.FactoryLine
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowModel ViewModel { get => this.DataContext as MainWindowModel; }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowModel();
        }

        private void MainMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.Source is Grid))
            {
                return;
            }

            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(ViewModel.MenuLists.Count > 0)
            {
                menuContainer.Children.Clear();
                foreach(var main in ViewModel.MenuLists)
                {
                    Expander expander = new Expander()
                    {
                        Header = main.HeaderName,
                        Width = 210,
                        IsExpanded = main.IsChecked
                    };

                    StackPanel stack = new StackPanel();
                    stack.Orientation = Orientation.Vertical;

                    if (main.SubMenus?.Count > 0)
                    {
                        int iFirst = 0;
                        foreach(var subMenu in main.SubMenus)
                        {
                            RadioButton radioButton = new RadioButton();
                            radioButton.IsChecked = subMenu.IsChecked;
                            radioButton.Content = subMenu.HeaderName;
                            radioButton.Checked += RadioButton_Checked;
                            radioButton.Tag = subMenu;
                            stack.Children.Add(radioButton);
                            if(iFirst == 0)
                            {
                                //radioButton.IsChecked  = true;
                            }
                            iFirst++;
                        }
                    }
                    expander.Content = stack;

                    menuContainer.Children.Add(expander);
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var menu = (sender as RadioButton).Tag as MyMenu;
            if(menu != null)
            {
                Console.WriteLine("名称：" + menu.HeaderName);
                Console.WriteLine("地址：" + menu.MenuUrl);
                mainFrame.Navigate(new Uri("Views/" + menu.MenuUrl, UriKind.RelativeOrAbsolute));
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
