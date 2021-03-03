using RegistrationApp.Service;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RegistrationApp.Data
{
    public abstract class DbDataAccess<T> : IDisposable
    {
        protected readonly DbProviderFactory factory;
        protected readonly DbConnection connection;


        public DbDataAccess()
        {
            factory = DbProviderFactories.GetFactory("RegistrationAppProvider");

            connection = factory.CreateConnection();
            connection.ConnectionString = ConfigurationService.Configuration["dataAccessConnectionString"];
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public abstract void Insert(T entity);
    }
}
