using luval.bpddg.app.Entities;
using luval.bpddg.app.Loader;
using Luval.Data;
using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization.Configuration;

namespace luval.bpddg.app
{
    class Program
    {
        static DateTime _start = DateTime.Now;
        static void Main(string[] args)
        {
            var consoleColor = Console.ForegroundColor;
            try
            {
                DoWork();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = consoleColor;
                Console.WriteLine();
                Console.WriteLine("Press any key");
                Console.ReadKey();
            }
        }

        static void DoWork()
        {
            var processMaster = new ProcessMaster();
            var sims = processMaster.GetSims(@".\Resources\GenerationTemplate.xlsx").ToList();
            var loaders = new ConcurrentDictionary<ProcessSim, ProcessLoader>();
            foreach (var sim in sims)
            {
                loaders[sim] = new ProcessLoader(sim);
                loaders[sim].ProgressMessage += RecordProgress;
                loaders[sim].LoadOrCreateProcessRoles();
            }
            var tasks = new List<Task>();
            foreach (var sim in sims)
            {
                tasks.Add(new Task(() => {
                    var loader = loaders[sim];
                    loader.GenerateSessions();
                    Console.WriteLine("Completed {0} in {1}", loader.Simulator.Name, DateTime.Now.Subtract(_start));
                }));
            }
            var runCount = 0;
            while (!tasks.Any(i => i.IsCompleted))
            {
                var toExecute = new List<Task>();
                for (int i = 0; i < 5; i++)
                {
                    if (runCount > tasks.Count) break;
                    toExecute.Add(tasks[runCount]);
                    runCount++;
                }
                toExecute.ForEach(i => i.Start());
                Task.WaitAll(toExecute.ToArray());
            }
            Console.WriteLine("Completed on {0}", DateTime.Now.Subtract(_start));
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }

        static void RecordProgress(object sender, string message)
        {
            Console.WriteLine("{0}", message);
        }
    }
}
