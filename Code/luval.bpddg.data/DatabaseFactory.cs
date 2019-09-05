using luval.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace luval.bpddg.data
{
    public class DatabaseFactory
    {
        public static Database Create(string provider, string connString)
        {
            if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(connString))
                throw new ArgumentException("All arguments are required");
            var factory =
                DbProviderFactories.GetFactory(providerName);
        }
    }
}
