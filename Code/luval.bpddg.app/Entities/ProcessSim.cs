using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Entities
{

    public class ProcessSim
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string ServiceLine { get; set; }
        public string BusinessUnit { get; set; }
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public int StartMinDate { get; set; }
        public int EndMinDate { get; set; }
        public int Interval { get; set; }
        public int MinTransaction { get; set; }
        public int MaxTransaction { get; set; }
        public double FPY { get; set; }
        public double BusinessException { get; set; }
        public double SystemException { get; set; }

        public string GetCodeFromWord(string name)
        {
            var words = name.Split(" ".ToCharArray()).Take(3);
            return words.Count() > 1 ? string.Join("", words.Select(i => i.ToUpperInvariant())) : words.First().Substring(0,2).ToUpperInvariant();
        }


        public string GetProcessId()
        {
            return Id.ToString().PadLeft(4, '0');
        }

        public string GetProcessGroup()
        {
            return string.Format("{0} - {1}", GetProcessId(), Name);
        }

        public string GetProcessName(string role)
        {
            return string.Format("{0}_{1}",
                GetRootName(), role);
        }

        public string GetQueueName()
        {
            return GetRootName() + "_Main";
        }

        private string GetRootName()
        {
            return string.Format("{0}_{1}_{2}_{3}_{4}",
                GetCodeFromWord(Region), GetCodeFromWord(ServiceLine), GetCodeFromWord(BusinessUnit), GetProcessId(), Name.Replace(" ", "_"));
        }

    }
}
