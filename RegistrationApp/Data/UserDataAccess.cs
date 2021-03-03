using RegistrationApp.Models;
using RegistrationApp.Service;
using System.Collections.Generic;
using System.Data.Common;


namespace RegistrationApp.Data
{
    public class UserDataAccess : DbDataAccess<User>
    {
        public override void Insert(User user)
        {
            string insertSqlScript = "insert into Users (Login, Password, FullName, Email,PhotoURL) values (@Login, @Password, @FullName, @Email, @PhotoURL)";

            using (var transaction = connection.BeginTransaction())
            {
                using (var command = factory.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = insertSqlScript;
                    var crypt = new CryptService();

                    try
                    {
                        command.Transaction = transaction;

                        var loginParameter = factory.CreateParameter();
                        loginParameter.DbType = System.Data.DbType.String;
                        loginParameter.Value = user.Login;
                        loginParameter.ParameterName = "Login";

                        command.Parameters.Add(loginParameter);

                        var passwordParameter = factory.CreateParameter();
                        passwordParameter.DbType = System.Data.DbType.Guid;
                        passwordParameter.Value = crypt.GetHashString(user.Password);
                        passwordParameter.ParameterName = "Password";

                        command.Parameters.Add(passwordParameter);

                        var fullNameParameter = factory.CreateParameter();
                        fullNameParameter.DbType = System.Data.DbType.String;
                        fullNameParameter.Value = user.FullName;
                        fullNameParameter.ParameterName = "FullName";

                        command.Parameters.Add(fullNameParameter);

                        var emailParameter = factory.CreateParameter();
                        emailParameter.DbType = System.Data.DbType.String;
                        emailParameter.Value = user.Email;
                        emailParameter.ParameterName = "Email";

                        command.Parameters.Add(emailParameter);

                        var photoParameter = factory.CreateParameter();
                        photoParameter.DbType = System.Data.DbType.String;
                        photoParameter.Value = user.PhotoURL;
                        photoParameter.ParameterName = "PhotoURL";

                        command.Parameters.Add(photoParameter);

                        command.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (DbException)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }




        public bool IsLoginExist(string login)
        {
            var selectSqlScript = "select login from Users";

            var command = factory.CreateCommand();
            command.CommandText = selectSqlScript;
            command.Connection = connection;

            var dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                if (login == dataReader["Login"].ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            dataReader.Close();
            command.Dispose();

            return false;
        }



        public ICollection<User> Select(string sortVariant)
        {
            var selectSqlScript = "select * from Users ORDER BY [Login]";

            if(sortVariant == "ASC")
            {
                selectSqlScript += " ASC";
            } 
            else if (sortVariant == "DESC")
            {
                selectSqlScript += " DESC";
            }

            var command = factory.CreateCommand();
            command.CommandText = selectSqlScript;
            command.Connection = connection;

            var dataReader = command.ExecuteReader();

            var users = new List<User>();

            while (dataReader.Read())
            {
                users.Add(new User
                {
                    Id = int.Parse(dataReader["Id"].ToString()),
                    Login = dataReader["Login"].ToString(),
                    Password = dataReader["Password"].ToString(),
                    FullName = dataReader["FullName"].ToString(),
                    Email = dataReader["Email"].ToString(),
                    PhotoURL = dataReader["PhotoURL"].ToString()
                }); 
            }

            dataReader.Close();
            command.Dispose();

            return users;
        }
    }
}
