using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

namespace XmlPlayground
{
    /// <summary>
    /// Interaction logic for ReadXml.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static StackPanel CurrentTab = new StackPanel();

        public MainWindow()
        {
            InitializeComponent();
            TabXmlReader.Focus();
            PanelXmlReader.Visibility = Visibility.Visible;
        }


        private void TabXmlReader_OnClick(object sender, RoutedEventArgs e)
        {
            Button currentButton = (Button) sender;
            currentButton.Focus();
            CurrentTab.Visibility = Visibility.Hidden;
            PanelXmlReader.Visibility = Visibility.Visible;
            CurrentTab = PanelXmlReader;
        }

        private void TabXDocument_OnClick(object sender, RoutedEventArgs e)
        {
            Button currentButton = (Button)sender;
            currentButton.Focus();
            CurrentTab.Visibility = Visibility.Hidden;
            PanelXDocument.Visibility = Visibility.Visible;
            CurrentTab = PanelXDocument;
        }

        private void TabDataSet_OnClick(object sender, RoutedEventArgs e)
        {
            Button currentButton = (Button)sender;
            currentButton.Focus();
            CurrentTab.Visibility = Visibility.Hidden;
            PanelDataSet.Visibility = Visibility.Visible;
            CurrentTab = PanelDataSet;
        }

        private void TabLinq_OnClick(object sender, RoutedEventArgs e)
        {
            Button currentButton = (Button)sender;
            currentButton.Focus();
            CurrentTab.Visibility = Visibility.Hidden;
            PanelLinq.Visibility = Visibility.Visible;
            CurrentTab = PanelLinq;
        }
    }
}
