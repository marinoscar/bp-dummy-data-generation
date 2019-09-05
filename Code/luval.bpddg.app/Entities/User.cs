using Luval.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Entities
{
    [TableName("BPAUser")]
    public class User
    {
        [ColumnName("userid")]
        public string UserId { get; set; }

        [ColumnName("username"), PrimaryKey]
        public string UserName { get; set; }
    }
}
