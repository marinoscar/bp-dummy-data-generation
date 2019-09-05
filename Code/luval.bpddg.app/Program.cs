using luval.bpddg.app.Entities;
using luval.bpddg.app.Loader;
using Luval.Data;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app
{
    class Program
    {
        static void Main(string[] args)
        {
            var processMaster = new ProcessMaster();
            processMaster.GetSims(@".\Resources\GenerationTemplate.xlsx");
            var processSim = new ProcessSim()
            {
                Region = "North America",
                ServiceLine = "Customer Service",
                BusinessUnit = "Order To Cash",
                Name = "Order Entry",
                Id = 1
            };
            var loader = new ProcessLoader(processSim);
            loader.LoadOrCreateProcessRoles();
        }
    }
}
