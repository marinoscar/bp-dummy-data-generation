using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Entities
{
    public class GroupHierarchy
    {
        public Group RegionGroup { get; set; }
        public Group ServiceLineGroup { get; set; }
        public Group BusinessUnitGroup { get; set; }
        public Group ProcessGroup { get; set; }
        public GroupProcess GroupProcess { get; set; }
    }
}
