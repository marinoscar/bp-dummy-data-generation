using luval.bpddg.app.Entities;
using Luval.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Resolvers
{
    public class ProcessResolver : SingletonResolver<ProcessResolver, string, Process>
    {
        private Database _db;
        private EntityAdapter _processAdapter;
        public ProcessResolver()
        {
            _db = DbHelper.CreateDb();
            _processAdapter = DbHelper.CreateAdapter(typeof(Process));
        }
        public override Process Get(string key)
        {
            return Get(key, (u) =>
            {
                var adapter = DbHelper.CreateAdapter(typeof(Process));
                var user = new User() { UserName = "admin" };
                return new Process();
            });
        }

        private Process GetProcess(string key)
        {
            var record = DictionaryDataRecord.FromEntity(new Process() { Name = key });
            var res = _processAdapter.Read<Process>(record);
            if (res == null) return res;
            //res.GroupProcess = GetGroupProcess(res.ProcessId);
            return res;
        }

        private GroupProcess GetGroupProcess(string processId)
        {
            return _db.ExecuteToEntityList<GroupProcess>("SELECT * FROM BPAGroupProcess WHERE processid = {0}".FormatSql(processId))
                .FirstOrDefault();
        }
    }
}
