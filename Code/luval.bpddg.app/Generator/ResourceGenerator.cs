using luval.bpddg.app.Entities;
using Luval.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Generator
{
    public class ResourceGenerator
    {
        private Database _db;
        private EntityAdapter _resourceAdapter;
        public ResourceGenerator()
        {
            _db = DbHelper.CreateDb();
            _resourceAdapter = DbHelper.CreateAdapter<Resource>();
        }

        public Resource GetOrCreate(string name)
        {
            return Get(name) ?? Create(name);
        }

        private Resource Get(string name)
        {
            return _resourceAdapter.Read<Resource>(new Resource() { Name = name });
        }

        private Resource Create(string name)
        {
            var res = new Resource() { Name = name };
            _resourceAdapter.Insert(res);
            return res;
        }
    }
}
