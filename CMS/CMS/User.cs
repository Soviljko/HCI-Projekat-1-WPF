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

        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public User() { }

        public User(string username, string password, UserRole role)
        {
            Username = username;
            Password = password;
            Role = role;
        }
    }

    public static class UserDataInitializer
    {
        // Staticka lista korisnika
        public static List<User> users = new List<User>();

        // Staticki konstruktor za inicijalizaciju
        static UserDataInitializer()
        {
            // Dodavanje inicijalnih korisnika
            users.Add(new User("admin", "admin", UserRole.Admin));
            users.Add(new User("user", "user", UserRole.Visitor));

            // Serijalizacija inicijalnih korisnika u XML datoteku
            SerializeUsers();
        }

        // Metoda za serijalizaciju korisnika u XML fajl
        private static void SerializeUsers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (FileStream stream = new FileStream("Users.xml", FileMode.Create))
            {
                serializer.Serialize(stream, users);
            }
        }
    }
}
