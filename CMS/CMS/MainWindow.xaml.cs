﻿using Notification.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace CMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotificationManager notificationManager;

        public ObservableCollection<Champion> Champions;

        public DataIO serializer = new DataIO();
        public MainWindow()
        {
            InitializeComponent();

            notificationManager = new NotificationManager();

            usernameTextBox.Text = "Input username here";

            //Champions = new ObservableCollection<Champion>();

            //Champions.Add(new Champion(3150, "Akali", DateTime.Now, "D:\\Desktop\\Projekat HCI\\CMS\\CMS\\Images\\Akali.png", "Yasuo.rtf"));
            //Champions.Add(new Champion(6400, "Yasuo", DateTime.Now, "D:\\Desktop\\Projekat HCI\\CMS\\CMS\\Images\\Yasuo.png", "Ognjen.rtf"));

            Champions = serializer.DeSerializeObject<ObservableCollection<Champion>>("Champions.xml");

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
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                // SaveDataAsXML();
                Application.Current.Shutdown();
            }
        }


        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            try
            {
                string username = usernameTextBox.Text;
                string password = passwordTextBox.Password;

                User user = UserDataInitializer.users.FirstOrDefault(u => u.Username == username && u.Password == password);
                if (user != null)
                {

                    if (user.Role == UserRole.Admin)
                    {
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                        adminWindow.ShowToastNotification(new ToastNotification("Welcome", "You successfully logged in", NotificationType.Success));

                    }
                    else if (user.Role == UserRole.Visitor)
                    {
                        VisitorWindow visitorWindow = new VisitorWindow();
                        visitorWindow.Show();
                        visitorWindow.ShowToastNotification(new ToastNotification("Welcome", "You successfully logged in", NotificationType.Success));
                    }

                    this.Close();
                }
                else
                {
                    usernameTextBox.Text = "";
                    passwordTextBox.Password = "";
                    mainWindow.ShowToastNotification(new ToastNotification("Login Failed", "User does not exist, please try again.", NotificationType.Warning));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do greške: {ex.Message}");
            }

        }

        private void usernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(usernameTextBox.Text.Trim().Equals("Input username here"))
            {
                usernameTextBox.Text = "";
                usernameTextBox.Foreground = Brushes.Black;
            }
        }

        private void usernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(usernameTextBox.Text.Trim().Equals(string.Empty))
            {
                usernameTextBox.Text = "Input username here";
                usernameTextBox.Foreground = Brushes.DarkBlue;
            }
        }

    }
}