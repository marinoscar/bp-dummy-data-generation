using luval.bpddg.app.Entities;
using luval.bpddg.app.Generator;
using luval.bpddg.app.Resolvers;
using Luval.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Loader
{
    public class ProcessLoader
    {
        private Database _db;
        private EntityAdapter _processAdapter;
        private UserResolver _userResolver;
        private EntityAdapter _queueAdapter;

        public ProcessLoader(ProcessSim sim)
        {
            Simulator = sim;
            _db = DbHelper.CreateDb();
            _processAdapter = DbHelper.CreateAdapter(typeof(Process));
            _queueAdapter = DbHelper.CreateAdapter(typeof(WorkQueue));
        }

        public ProcessSim Simulator { get; private set; }
        public Process Load { get; set; }
        public Process Main { get; set; }
        public Process Execute { get; set; }
        protected UserResolver UserResolver
        {
            get
            {
                if (_userResolver == null) _userResolver = new UserResolver();
                return _userResolver;
            }
        }

        public void LoadOrCreateProcessRoles()
        {
            Load = GetOrCreate(Simulator.GetProcessName("Load"));
            Main = GetOrCreate(Simulator.GetProcessName("Main"));
            Execute = GetOrCreate(Simulator.GetProcessName("Execute"));
            Main.Queue = GetOrCreateQueue();
        }

        private WorkQueue GetOrCreateQueue()
        {
            var name = Simulator.GetProcessName("WQ");
            return GetQueue(name) ?? CreateQueue(name);
        }

        private WorkQueue CreateQueue(string name)
        {
            var ident = Convert.ToInt32(_db.ExecuteScalar("SELECT MAX(ident) FROM BPAWorkQueue")) + 1;
            var queue = new WorkQueue() { Ident = ident, Name = name };
            _queueAdapter.Insert(DictionaryDataRecord.FromEntity(queue));
            return queue;
            
        }

        private WorkQueue GetQueue(string name)
        {
            return _db.ExecuteToEntityList<WorkQueue>("select * from BPAWorkQueue where name = {0}".FormatSql(name))
                .FirstOrDefault();
        }

        private Process GetOrCreate(string name)
        {
            var process = GetProcess(name);
            if (process == null) return CreateProcess(name);
            return process;
        }

        private Process CreateProcess(string name)
        {
            var user = UserResolver.Get("admin");
            var process = new Process()
            {
                Name = name,
                CreatedBy = user.UserId,
                AttributeID = name.Contains("Main") ? 2 : 0,
                RunMode = 1,
                LastModifiedBy = user.UserId
            };
            process.UpdateXml();
            var record = DictionaryDataRecord.FromEntity(process);
            _processAdapter.Insert(record);
            return GetProcess(name);
        }

        private Process GetProcess(string name)
        {
            var record = DictionaryDataRecord.FromEntity(new Process() { Name = name });
            var process = _processAdapter.Read<Process>(record);
            if (process == null) return null;
            var groupGenerator = new GroupGenerator();
            process.Groups = groupGenerator.GetOrCreate(process, Simulator.Region, Simulator.ServiceLine, Simulator.BusinessUnit, Simulator.GetProcessGroup());
            return process;
        }

    }
}
