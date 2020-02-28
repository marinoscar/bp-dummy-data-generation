using Luval.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Entities
{
    [TableName("BPAWorkQueueItem")]
    public class WorkQueueItem
    {
        public WorkQueueItem()
        {
            Attempt = 1;
            Id = Guid.NewGuid().ToString();
            KeyValue = Id;
            Data = @"<collection><row><field name=""Invoice No"" type=""text"" value=""REPLACE"" /></row></collection>".Replace("REPLACE", Id);
        }
        [PrimaryKey]
        public string Id { get; set; }
        public string QueueId { get; set; } 
        public string KeyValue { get; set; }
        public string Status { get; set; }
        public int Attempt { get; set; }
        public DateTime Loaded { get; set; }
        public DateTime? Completed { get; set; }
        public DateTime? Exception { get; set; }
        public string ExceptionReason { get; set; }
        public int WorkTime { get; set; }
        public string Data { get; set; }
        public int QueueIdent { get; set; }
        [IdentityColumn]
        public long Ident { get; set; }
        public string SessionId { get; set; }
        public int PrevWorkTime { get; set; }
        [NotMapped]
        public DateTime SimCompleted { get; set; }
    }

    [TableName("BPAWorkQueueItemTag")]
    public class WorkQueueItemTag
    {
        public long QueueItemIdent { get; set; }
        public int TagId { get; set; }
    }

}
