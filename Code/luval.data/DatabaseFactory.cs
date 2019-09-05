using luval.data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace luval.data
{
    /// <summary>
    /// Provides a factory pattern to create new Database objects
    /// </summary>
    public class DatabaseFactory
    {
        /// <summary>
        /// Creates a new <see cref="Database"/> object based on the registered providers
        /// </summary>
        /// <param name="provider">
        /// The name of the provider, a list of providers names can be gather from the following code example
        /// <code>
        /// DbProviderFactories.GetFactoryClasses();
        /// </code>
        /// For example for Sql Server the provider name is System.Data.SqlClient
        /// </param>
        /// <param name="connString">The connection string to use</param>
        /// <returns>A new <see cref="Database" object/></returns>
        public static Database Create(string provider, string connString)
        {
            return new Database(() => {
                return CreateConnection(provider, connString);
            });
        }

        /// <summary>
        /// Creates a new <see cref="IDbConnection"/> object based on the registered providers
        /// </summary>
        /// <param name="provider">
        /// The name of the provider, a list of providers names can be gather from the following code example
        /// <code>
        /// DbProviderFactories.GetFactoryClasses();
        /// </code>
        /// For example for Sql Server the provider name is System.Data.SqlClient
        /// </param>
        /// <param name="connString">The connection string to use</param>
        /// <returns>A new <see cref="IDbConnection" object/></returns>
        public static IDbConnection CreateConnection(string provider, string connString)
        {
            if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(connString))
                throw new ArgumentException("All arguments are required");
            var factory = DbProviderFactories.GetFactory(provider);
            if (factory == null) throw new ArgumentException("Invalid provider name");
            var conn = factory.CreateConnection();
            conn.ConnectionString = connString;
            return conn;
        }
    }
}
