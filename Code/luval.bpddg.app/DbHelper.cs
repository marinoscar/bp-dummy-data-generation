using Luval.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app
{
    public static class DbHelper
    {
        public static Database CreateDb()
        {
            var connStr = ConfigurationManager.ConnectionStrings["main"].ConnectionString;
            return new SqlServerDatabase(connStr);
        }

        public static EntityAdapter CreateAdapter<T>()
        {
            return CreateAdapter(typeof(T));
        }

        public static EntityAdapter CreateAdapter(Type entityType)
        {
            return new EntityAdapter(CreateDb(), new SqlServerDialectProvider(SqlTableSchema.Load(entityType)));
        }
    }
}
