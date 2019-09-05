using Luval.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Entities
{
    [TableName("BPAResource")]
    public class Resource
    {
        public Resource()
        {
            LogToEventLog = true;
            StatusId = 2;
            ResourceId = Guid.NewGuid().ToString();
        }
        public string ResourceId { get; set; }
        [PrimaryKey]
        public string Name { get; set; }
        public int ProcessRunning { get; set; }
        public int ActionsRunning { get; set; }
        public int UnitsAllocated { get; set; }
        public int Diagnostics { get; set; }
        public bool LogToEventLog {get;set;}
        public bool Ssl { get; set; }
        public int StatusId { get; set; }
    }
}
