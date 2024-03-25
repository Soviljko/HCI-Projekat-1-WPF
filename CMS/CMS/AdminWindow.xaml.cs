using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CMS
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public ObservableCollection<Champion> Champions { get; set; }

        MainWindow mainWindow = new MainWindow();

        public AdminWindow()
        {
            // MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            InitializeComponent();

            DataContext = this;

            Champions = new ObservableCollection<Champion>();
            

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Show();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            mainWindow.Show();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
