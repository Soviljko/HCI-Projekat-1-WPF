using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace CMS
{
    public enum UserRole
    {
        Admin,
        Visitor
    }
    public class User
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public UserRole Role { get; set; }

        public User() { }

        public User(string username,string password, UserRole role)
        {
            this.Username = username;
            this.Password = password;
            this.Role = role;
        }

        public static List<User> usersList;

        static User()
        {
            usersList = new List<User>
            {
                new User("admin","admin",UserRole.Admin),
                new User("visitor","visitor",UserRole.Visitor)
            };

            SerializeUsers();
        }

        private static void SerializeUsers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (FileStream stream = new FileStream("users.xml", FileMode.Create))
            {
                serializer.Serialize(stream, usersList);
            }
        }

        


    }
}
