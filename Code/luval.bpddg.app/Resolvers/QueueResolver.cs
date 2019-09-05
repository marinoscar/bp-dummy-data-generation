using luval.bpddg.app.Entities;
using luval.bpddg.app.Generator;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Resolvers
{
    public class QueueResolver : SingletonResolver<QueueResolver, string, WorkQueue>
    {
        public override WorkQueue Get(string key)
        {
            return Get(key, (name) =>
            {
                var queueGen = new WorkQueueGenerator();
                return queueGen.GetOrCreate(key);
            });
        }
    }
}
