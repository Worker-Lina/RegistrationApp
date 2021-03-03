using RegistrationApp.Data;
using RegistrationApp.Service;
using System;

namespace RegistrationApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConfiguration();

            var sing = new SignUpInService();
            var user = sing.Registration();

            using (var userDataAccess = new UserDataAccess())
            {
                userDataAccess.Insert(user);
            }

        }

        private static void InitConfiguration()
        {
            ConfigurationService.Init();
        }
    }
}
