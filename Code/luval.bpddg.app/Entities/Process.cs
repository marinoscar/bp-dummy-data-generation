using Luval.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Entities
{
    [TableName("BPAProcess")]
    public class Process
    {
        public Process()
        {
            ProcessId = Guid.NewGuid().ToString();
            ProcessType = "P";
            Version = "1.0";
            CreateDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
            RunMode = 1;
            UseLegacyNamespace = true;
            Description = string.Empty;
            ProcessXml = @"<process name=""ProcessTemplate2019"" version=""1.0"" bpversion=""6.5.0.12573"" narrative="""" byrefcollection=""true"">    <view>      <camerax>0</camerax>      <cameray>0</cameray>      <zoom version=""2"">1.25</zoom>    </view>    <preconditions />    <endpoint narrative="""" />    <stage stageid=""93fceb79-54ab-4ef4-889e-7c3d772eea90"" name=""Start"" type=""Start"">      <narrative></narrative>      <displayx>15</displayx>      <displayy>-105</displayy>      <displaywidth>60</displaywidth>      <displayheight>30</displayheight>      <font family=""Segoe UI"" size=""10"" style=""Regular"" color=""000000"" />      <onsuccess>52ddc9e4-21d8-4c05-aca2-406b5314ba0f</onsuccess>    </stage>    <stage stageid=""3c0ba2da-7378-4c1d-b31b-e6569d05b095"" name=""End"" type=""End"">      <narrative></narrative>      <displayx>15</displayx>      <displayy>90</displayy>      <displaywidth>60</displaywidth>      <displayheight>30</displayheight>      <font family=""Segoe UI"" size=""10"" style=""Regular"" color=""000000"" />    </stage>    <stage stageid=""0dd54608-92a5-4f3c-bcea-a7717e2a7d03"" name=""Stage1"" type=""ProcessInfo"">      <narrative></narrative>      <displayx>-195</displayx>      <displayy>-105</displayy>      <displaywidth>150</displaywidth>      <displayheight>90</displayheight>      <font family=""Segoe UI"" size=""10"" style=""Regular"" color=""000000"" />    </stage>    <stage stageid=""52ddc9e4-21d8-4c05-aca2-406b5314ba0f"" name=""MainNote"" type=""Note"">      <loginhibit />      <narrative>This is a template XML</narrative>      <displayx>15</displayx>      <displayy>-30</displayy>      <displaywidth>60</displaywidth>      <displayheight>30</displayheight>      <font family=""Segoe UI"" size=""10"" style=""Regular"" color=""000000"" />      <onsuccess>3c0ba2da-7378-4c1d-b31b-e6569d05b095</onsuccess>    </stage>  </process>";
        }
        [ColumnName("processid")]
        public string ProcessId { get; set; }
        [ColumnName("ProcessType"), PrimaryKey]
        public string ProcessType { get; set; }
        [ColumnName("description")]
        public string Description { get; set; }
        [ColumnName("name"), PrimaryKey]
        public string Name { get; set; }
        [ColumnName("version")]
        public string Version { get; set; }
        [ColumnName("createdate")]
        public DateTime CreateDate { get; set; }
        [ColumnName("createdby")]
        public string CreatedBy { get; set; }
        [ColumnName("lastmodifieddate")]
        public DateTime LastModifiedDate { get; set; }
        [ColumnName("lastmodifiedby")]
        public string LastModifiedBy { get; set; }
        [ColumnName("AttributeID")]
        public int AttributeID { get; set; }
        [ColumnName("processxml")]
        public string ProcessXml { get; set; }
        [ColumnName("runmode")]
        public int RunMode { get; set; }
        [ColumnName("sharedObject")]
        public bool SharedObject { get; set; }
        [ColumnName("forceLiteralForm")]
        public bool ForceLiteralForm { get; set; }
        [ColumnName("useLegacyNamespace")]
        public bool UseLegacyNamespace { get; set; }
        [NotMapped]
        public GroupHierarchy Groups { get; set; }
        [NotMapped]
        public WorkQueue Queue { get; set; }

        public void UpdateXml()
        {
            UpdateXml("ProcessTemplate2019");
        }

        public void UpdateXml(string oldName)
        {
            ProcessXml = ProcessXml.Replace(oldName, Name);
        }

    }
}
