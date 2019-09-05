using Luval.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Entities
{
    [TableName("BPAWorkQueue")]
    public class WorkQueue
    {
        public WorkQueue()
        {
            Id = Guid.NewGuid().ToString();
            Running = true;
            MaxAttempts = 5;
            TargetSessions = 0;
            RequireFeature = "";
            KeyField = "Invoice No";
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string KeyField { get; set; }
        public bool Running { get; set; }
        public int MaxAttempts { get; set; }
        [ColumnName("ident"), PrimaryKey]
        public int Ident { get; set; }
        public string ProcessId { get; set; }
        public int TargetSessions { get; set; }
        public string RequireFeature { get; set; }

    }
}
