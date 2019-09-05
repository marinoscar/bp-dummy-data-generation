using Luval.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Entities
{
    [TableName("BPAGroup")]
    public class Group
    {
        public Group()
        {
            Id = Guid.NewGuid().ToString();
            TreeId = 2;
        }

        [ColumnName("id")]
        public string Id { get; set; }
        [ColumnName("name"), PrimaryKey]
        public string Name { get; set; }
        [ColumnName("treeid"), PrimaryKey]
        public int TreeId { get; set; }
        [ColumnName("isrestricted")]
        public bool IsRestricted { get; set; }
    }

    [TableName("BPAGroupGroup")]
    public class GroupGroup
    {
        [ColumnName("groupid")]
        public string GroupId { get; set; }
        [ColumnName("memberid")]
        public string MemberId { get; set; }
    }

    [TableName("BPAGroupProcess")]
    public class GroupProcess
    {
        [ColumnName("groupid")]
        public string GroupId { get; set; }
        [ColumnName("processid")]
        public string ProcessId { get; set; }
    }
}
