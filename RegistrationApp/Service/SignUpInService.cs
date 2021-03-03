using RegistrationApp.Data;
using RegistrationApp.Models;
using System;
using System.Text.RegularExpressions;


namespace RegistrationApp.Service
{
    public class SignUpInService
    {
        public User Registration()
        {
            using (var userDataAccess = new UserDataAccess())
            {
                FileSystemService fileService = new FileSystemService();

                User user = new User();
                Console.WriteLine("Введите логин: ");
                var login = Console.ReadLine();
                if (userDataAccess.IsLoginExist(login) == false)
                {
                    user.Login = login;
                }

                Console.WriteLine("Придумайте пароль: ");
                user.Password = Console.ReadLine();

                Console.WriteLine("Введите полное имя: ");
                user.FullName = Console.ReadLine();

                Console.WriteLine("Введите почту");
                var email = Console.ReadLine();
                if (IsValid(email))
                {
                    user.Email = email;
                }

                user.PhotoURL = fileService.PucturePlace();

                return user;
            }
        }


        bool IsValid(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public void SingIn()
        {
            using (var userDataAccess = new UserDataAccess())
            {
                User user = new User();
                Console.WriteLine("Введите логин: ");
                var login = Console.ReadLine();
                if (userDataAccess.IsLoginExist(login) == true)
                {
                    user.Login = login;
                }

                Console.WriteLine("Введите пароль: ");
                var password = Console.ReadLine();
                if (userDataAccess.IsPasswordExist(password) == true)
                {
                    Console.WriteLine("Welcome");
                }
            }
            //TODO
        }

    }
}
