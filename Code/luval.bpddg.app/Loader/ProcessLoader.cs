using luval.bpddg.app.Entities;
using luval.bpddg.app.Generator;
using luval.bpddg.app.Resolvers;
using Luval.Data;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
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
        private EntityAdapter _sessionAdapter;
        private EntityAdapter _wqiAdapter;

        public ProcessLoader(ProcessSim sim)
        {
            Simulator = sim;
            _db = DbHelper.CreateDb();
            _sessionAdapter = DbHelper.CreateAdapter<Session>();
            _wqiAdapter = DbHelper.CreateAdapter<WorkQueueItem>();
        }

        public ProcessSim Simulator { get; private set; }
        public Process Load { get; set; }
        public Process Main { get; set; }
        public Process Execute { get; set; }

        public void LoadOrCreateProcessRoles()
        {
            var processGen = new ProcessGenerator();
            var groupGen = new GroupGenerator();
            var queueGen = new WorkQueueGenerator();
            Load = processGen.GetOrCreate(Simulator.GetProcessName("Load"));
            Main = processGen.GetOrCreate(Simulator.GetProcessName("Main"));
            Execute = processGen.GetOrCreate(Simulator.GetProcessName("Execute"));
            Load.Groups = groupGen.GetOrCreate(Load, Simulator.Region, Simulator.ServiceLine, Simulator.BusinessUnit, Simulator.GetProcessGroup());
            Main.Groups = groupGen.GetOrCreate(Main, Simulator.Region, Simulator.ServiceLine, Simulator.BusinessUnit, Simulator.GetProcessGroup());
            Execute.Groups = groupGen.GetOrCreate(Execute, Simulator.Region, Simulator.ServiceLine, Simulator.BusinessUnit, Simulator.GetProcessGroup());
            Main.Queue = queueGen.GetOrCreate(Simulator.GetProcessName("WQ"));
        }

        public void GenerateSessions()
        {
            var startDate = GetLastDateForProcess();
            var endDate = DateTime.Today.AddDays(1);
            var currentDate = startDate;
            while (currentDate < endDate)
            {
                var startTime = currentDate.AddMinutes(Simulator.StartMinDate);
                var endTime = currentDate.AddMinutes(Simulator.EndMinDate);
                while (startTime < endTime)
                {
                    var tranCount = (new Random()).Next(Simulator.MinTransaction, Simulator.MaxTransaction);
                    var maxDuration = Convert.ToDouble(Math.Ceiling((double)(Simulator.Interval / tranCount)));
                    var minDuration = Convert.ToDouble(Math.Floor(maxDuration * 0.85));
                    var resourceId = ResourceResolver.Instance.Get("BPRUNTIME.EAST01.{0}".Fi(Simulator.Id.ToString().PadLeft(5, '0'))).ResourceId;
                    var session = new Session()
                    {
                        StartDateTime = startDate,
                        ProcessId = Main.ProcessId,
                        LastUpdated = DateTime.Now,
                        RunningResourceId = resourceId,
                        StarterResourceId = resourceId,
                        QueueId = Main.Queue.Ident,
                        EndDateTime = startDate,
                    };
                    _sessionAdapter.Insert(session);
                    session = _sessionAdapter.Read<Session>(session); //get updated identity value
                    for (int i = 0; i < tranCount; i++)
                    {
                        var probability = new Random().NextDouble();
                        var completed = startTime.AddMinutes(GetRandomNumber(minDuration, maxDuration));
                        var tag = default(string);
                        var wq = new WorkQueueItem()
                        {
                            Ident = GetNextQueueItemIdent(),
                            QueueIdent = Main.Queue.Ident,
                            QueueId = Main.Queue.Id,
                            Loaded = startTime
                        };
                        if (probability <= Simulator.FPY)
                        {
                            wq.Completed = completed;
                        }
                        else if (probability <= (Simulator.FPY + Simulator.BusinessException))
                        {
                            tag = "Business Exception";
                            wq.Exception = completed;
                            wq.ExceptionReason = "Bussiness Exception: Failed to complete transaction";
                        }
                        else
                        {
                            tag = "System Exception";
                            wq.Exception = completed;
                            wq.ExceptionReason = "System Exception: Failed to complete transaction";
                        }
                        _wqiAdapter.Insert(wq);
                        MarryTag(wq, tag);
                        startTime = completed.AddSeconds(1);
                        System.Threading.Thread.Sleep(20);//Force next random value
                    }
                }
                currentDate = currentDate.Date.AddDays(1);//move to the next day
            }
        }

        private void MarryTag(WorkQueueItem wqi, string tag)
        {
            if (string.IsNullOrWhiteSpace(tag)) return;
            var tagItem = TagResolver.Instance.Get(tag);
            _db.ExecuteNonQuery("INSERT INTO BPAWorkQueueItemTag VALUES ({0} {1})".FormatSql(wqi.Ident, tagItem.Id));
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private long GetNextQueueItemIdent()
        {
            var res = _db.ExecuteScalar("SELECT MAX(ident) FROM BPAWorkQueueItem");
            if (res.IsNullOrDbNull()) return 1;
            return Convert.ToInt32(res) + 1;
        }

        private DateTime GetLastDateForProcess()
        {
            var res = _db.ExecuteScalar("SELECT MAX(startdatetime) FROM BPASession WHERE processid = {0}".FormatSql(Main.ProcessId));
            if (res.IsNullOrDbNull()) return Simulator.StartDate;
            return Convert.ToDateTime(res).Date.AddDays(1);
        }
    }
}
