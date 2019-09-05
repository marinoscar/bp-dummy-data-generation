using Luval.Data;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Entities
{
    public class Session
    {
        public Session()
        {
            SessionId = Guid.NewGuid().ToString();
            StatusId = 4;
            StartParamXml = "<inputs />";
            LastStage = "Wait1";
            WarningThreshold = 300;
            StartTimeZoneOffset = -1800;
            EndTimeZoneOffset = -1800;
            LastUpdateTimeZoneOffset = -1800;

        }
        [PrimaryKey]
        public string SessionId { get; set; }
        public int QueueId { get; set; }
        public int SessionNumber { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string ProcessId { get; set; }
        public string StartResourceId { get; set; }
        public string StartUserId { get; set; }
        public string RunningResourceId { get; set; }
        public int StatusId { get; set; }
        public string StartParamXml { get; set; }
        public DateTime LastUpdated { get; set; }
        public string LastStage { get; set; }
        public int WarningThreshold { get; set; }
        public int StartTimeZoneOffset { get; set; }
        public int EndTimeZoneOffset { get; set; }
        public int LastUpdateTimeZoneOffset { get; set; }
    }
}
