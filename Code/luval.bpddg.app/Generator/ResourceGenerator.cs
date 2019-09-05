using luval.bpddg.app.Entities;
using Luval.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Generator
{
    public class ResourceGenerator : GeneratorBase<Resource>
    {

        public Resource GetOrCreate(string name)
        {
            return Get(name) ?? Create(name);
        }

        private Resource Get(string name)
        {
            return Get(new Resource() { Name = name });
        }

        private Resource Create(string name)
        {
            return Create(new Resource() { Name = name });
        }
    }
}
