using Notification.Wpf;
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

        private List<User> Users = User.usersList;
        public MainWindow()
        {
            InitializeComponent();

            notificationManager = new NotificationManager();
        }

        public void ShowToastNotification(ToastNotification toastNotification)
        {
            notificationManager.Show(toastNotification.Title,
           toastNotification.Message, toastNotification.Type, "WindowNotificationArea");
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Password;

            User testUser = AuthenticateUser(username, password);

            AddUser(testUser);

            if(testUser != null)
            {
                MessageBox.Show($"Dobrodosli, {testUser.Username}!");

                if(testUser.Role == UserRole.Admin)
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                }
                else
                {
                    VisitorWindow visitorWindow = new VisitorWindow();
                    visitorWindow.Show();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Pogresno korisnicko ime ili lozinka, probajte ponovo");
            }

        }

        private void AddUser(User user)
        {
            if (!User.usersList.Any(u => u.Username == user.Username))
            {
                User.usersList.Add(user);
                SerializeUsers();
            }
            else
            {
                
               // MessageBox.Show($"Korisnik {user.Username} već postoji.");
            }
        }

        private User AuthenticateUser(string username, string password)
        {
            return Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        private static void SerializeUsers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (FileStream stream = new FileStream("users.xml", FileMode.Create))
            {
                serializer.Serialize(stream, User.usersList);
            }
        }


    }
}