using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CMS
{
    /// <summary>
    /// Interaction logic for VisitorWindow.xaml
    /// </summary>
    public partial class VisitorWindow : Window
    {
        private NotificationManager notificationManager = new NotificationManager();
        public static ObservableCollection<Champion> Champions { get; set; }

        MainWindow mainWindow = new MainWindow();

        public VisitorWindow()
        {
            InitializeComponent();

            Champions = mainWindow.Champions;

            DataContext = this;
        }

        public void ShowToastNotification(ToastNotification toastNotification)
        {
            notificationManager.Show(toastNotification.Title,
           toastNotification.Message, toastNotification.Type, "WindowNotificationArea");
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            DetailedView detailedView = new DetailedView(championsDataGrid.SelectedIndex);
            detailedView.Show();
        }
    }
}
