using luval.bpddg.app.Entities;
using luval.bpddg.app.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Generator
{
    public class ProcessGenerator : GeneratorBase<Process>
    {
        public Process GetOrCreate(string name)
        {
            return Get(name) ?? Create(name);
        }

        private Process Create(string name)
        {
            var user = UserResolver.Instance.Get("admin");
            var process = new Process()
            {
                Name = name,
                CreatedBy = user.UserId,
                AttributeID = name.Contains("Main") ? 2 : 0,
                RunMode = 1,
                LastModifiedBy = user.UserId
            };
            process.UpdateXml();
            Create(process);
            return Get(name);
        }

        private Process Get(string name)
        {
            return Get(new Process() { Name = name });
        }
    }
}
