using luval.bpddg.app.Entities;
using Luval.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Generator
{
    public class GroupGenerator
    {
        private EntityAdapter _groupAdapter;
        private Database _db;

        public GroupGenerator()
        {
            _db = DbHelper.CreateDb();
            _groupAdapter = DbHelper.CreateAdapter(typeof(Group));
        }

        public GroupHierarchy GetOrCreate(Process process, string regionName, string serviceLine, string businessUnit, string processGroupName)
        {
            var regionGrp = Get(regionName);
            if (regionGrp == null)
                return CreateFromRegion(process, regionName, serviceLine, businessUnit, processGroupName);
            var serviceLineGroup = Get(serviceLine, regionGrp.Id);
            if(serviceLineGroup == null)
                return CreateFromServiceLine(process, regionGrp, serviceLine, businessUnit, processGroupName);
            var businessUnitGroup = Get(businessUnit, serviceLineGroup.Id);
            if (businessUnitGroup == null)
                return CreateFromBusinessUnit(process, regionGrp, serviceLineGroup, businessUnit, processGroupName);
            var processGroup = Get(processGroupName, businessUnitGroup.Id);
            if (processGroup == null)
                return CreateFromProcessGroup(process, regionGrp, serviceLineGroup, businessUnitGroup, processGroupName);
            var groupProcess = GetOrCreate(process, processGroup);
            return new GroupHierarchy() { RegionGroup = regionGrp, ServiceLineGroup = serviceLineGroup, BusinessUnitGroup = businessUnitGroup, ProcessGroup = processGroup, GroupProcess = groupProcess };
        }

        private GroupProcess GetOrCreate(Process process, Group processGroup)
        {
            var res = GetGroupProcess(processGroup.Id, process.ProcessId);
            if (res != null) return res;
            MarryGroupProcess(processGroup.Id, process.ProcessId);
            return GetGroupProcess(processGroup.Id, process.ProcessId);
        }

        private GroupProcess GetGroupProcess(string groupId, string processId)
        {
            return _db.ExecuteToEntityList<GroupProcess>("SELECT * FROM BPAGroupProcess WHERE groupid = {0} and processid = {1}"
                .FormatSql(groupId, processId)).FirstOrDefault();
        }

        private GroupHierarchy CreateFromRegion(Process process, string regionName, string serviceLine, string businessUnit, string processGroup)
        {
            var regionGrp = Create(regionName);
            var slGrp = Create(serviceLine);
            var buGrp = Create(businessUnit);
            var prGrp = Create(processGroup);
            MarryGroup(regionGrp.Id, slGrp.Id);
            MarryGroup(slGrp.Id, buGrp.Id);
            MarryGroup(buGrp.Id, prGrp.Id);
            MarryGroupProcess(prGrp.Id, process.ProcessId);
            return new GroupHierarchy() { RegionGroup = regionGrp, ServiceLineGroup = slGrp, BusinessUnitGroup = buGrp, ProcessGroup = prGrp, GroupProcess = GetGroupProcess(prGrp.Id, process.ProcessId) };
        }

        private GroupHierarchy CreateFromServiceLine(Process process, Group regionGroup, string serviceLine, string businessUnit, string processGroup)
        {
            var slGrp = Create(serviceLine);
            var buGrp = Create(businessUnit);
            var prGrp = Create(processGroup);
            MarryGroup(regionGroup.Id, slGrp.Id);
            MarryGroup(slGrp.Id, buGrp.Id);
            MarryGroup(buGrp.Id, prGrp.Id);
            MarryGroupProcess(prGrp.Id, process.ProcessId);
            return new GroupHierarchy() { RegionGroup = regionGroup, ServiceLineGroup = slGrp, BusinessUnitGroup = buGrp, ProcessGroup = prGrp, GroupProcess = GetGroupProcess(prGrp.Id, process.ProcessId) };
        }

        private GroupHierarchy CreateFromBusinessUnit(Process process, Group regionGroup, Group serviceLineGroup, string businessUnit, string processGroup)
        {
            var buGrp = Create(businessUnit);
            var prGrp = Create(processGroup);
            MarryGroup(serviceLineGroup.Id, buGrp.Id);
            MarryGroup(buGrp.Id, prGrp.Id);
            MarryGroupProcess(prGrp.Id, process.ProcessId);
            return new GroupHierarchy() { RegionGroup = regionGroup, ServiceLineGroup = serviceLineGroup, BusinessUnitGroup = buGrp, ProcessGroup = prGrp, GroupProcess = GetGroupProcess(prGrp.Id, process.ProcessId) };
        }

        private GroupHierarchy CreateFromProcessGroup(Process process, Group regionGroup, Group serviceLineGroup, Group businessUnitGroup, string processGroup)
        {
            var prGrp = Create(processGroup);
            MarryGroup(businessUnitGroup.Id, prGrp.Id);
            MarryGroupProcess(prGrp.Id, process.ProcessId);
            return new GroupHierarchy() { RegionGroup = regionGroup, ServiceLineGroup = serviceLineGroup, BusinessUnitGroup = businessUnitGroup, ProcessGroup = prGrp, GroupProcess = GetGroupProcess(prGrp.Id, process.ProcessId) };
        }

        private void MarryGroup(string parent, string child)
        {
            _db.ExecuteNonQuery("INSERT INTO BPAGroupGroup VALUES ({0}, {1})".FormatSql(parent, child));
        }

        private void MarryGroupProcess(string group, string process)
        {
            _db.ExecuteNonQuery("INSERT INTO BPAGroupProcess VALUES ({0}, {1})".FormatSql(group, process));
        }

        private Group Get(string name)
        {
            var record = DictionaryDataRecord.FromEntity(new Group() { Name = name });
            return _groupAdapter.Read<Group>(record);
        }

        private Group Get(string name, string parent)
        {
            var sql = @"
select
SG.*
from BPAGroupGroup
inner join BPAGroup As PG on PG.id = BPAGroupGroup.groupid
inner join BPAGroup As SG on SG.id = BPAGroupGroup.memberid
where PG.treeid = 2 and PG.id = {0} and SG.name = {1}
".FormatSql(parent, name);
            return _db.ExecuteToEntityList<Group>(sql).FirstOrDefault();
        }

        private Group Create(string name)
        {
            var group = new Group() { Name = name };
            var record = DictionaryDataRecord.FromEntity(group);
            _groupAdapter.Insert(record);
            return group;
        }
    }
}
