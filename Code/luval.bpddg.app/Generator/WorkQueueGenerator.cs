using luval.bpddg.app.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Generator
{
    public class WorkQueueGenerator : GeneratorBase<WorkQueue>
    {

        public WorkQueue GetOrCreate(string name)
        {
            return Get(name) ?? Create(name);
        }

        private WorkQueue Create(string name)
        {
            var db = DbHelper.CreateDb();
            var ident = Convert.ToInt32(db.ExecuteScalar("SELECT MAX(ident) FROM BPAWorkQueue")) + 1;
            var queue = new WorkQueue() { Ident = ident, Name = name };
            Create(queue);
            return Get(name);

        }

        private WorkQueue Get(string name)
        {
            return Get(new WorkQueue() { Name = name });
        }
    }
}
