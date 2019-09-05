using Luval.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Entities
{
    [TableName("BPATag")]
    public class TagEntity
    {
        public int Id { get; set; }
        public string Tag { get; set; }
    }
}
