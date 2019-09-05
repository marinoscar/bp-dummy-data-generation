using luval.bpddg.app.Entities;
using Luval.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Resolvers
{
    public class UserResolver : SingletonResolver<UserResolver, string, User>
    {
        public override User Get(string userName)
        {
            return Get(userName, (u) =>
            {
                var adapter = DbHelper.CreateAdapter(typeof(User));
                var user = new User() { UserName = "admin" };
                return adapter.Read<User>(DictionaryDataRecord.FromEntity(user));
            });
        }
    }
}
