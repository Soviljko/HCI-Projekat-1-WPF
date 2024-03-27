using FontAwesome5;
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
        public static ObservableCollection<Champion> Champions { get; set; }

        MainWindow mainWindow = new MainWindow();

        public DataIO serializer = new DataIO();

        private NotificationManager notificationManager = new NotificationManager();
        public AdminWindow()
        {
            InitializeComponent();

            Champions = mainWindow.Champions;

            DataContext = this;

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Show();
        }

        public void ShowToastNotification(ToastNotification toastNotification)
        {
            notificationManager.Show(toastNotification.Title,
           toastNotification.Message, toastNotification.Type, "WindowNotificationArea");
        }

        private void SaveDataAsXML()
        {
            serializer.SerializeObject<ObservableCollection<Champion>>(Champions, "Champions.xml");
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            SaveDataAsXML();
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete selected champions from table", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var championsToRemove = Champions.Where(c => c.IsChecked == true).ToList();
                foreach (var champion in championsToRemove)
                {
                    Champions.Remove(champion);
                }
            }
            else if(result == MessageBoxResult.No)
            {
                foreach (var champion in Champions)
                {
                    champion.IsChecked = false;
                }
            }
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            //EditWindow editWindow = new EditWindow(championsDataGrid.SelectedIndex);
            //editWindow.Show();
        }
    }
}
