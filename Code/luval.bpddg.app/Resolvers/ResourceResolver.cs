using luval.bpddg.app.Entities;
using luval.bpddg.app.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Resolvers
{
    public class ResourceResolver: SingletonResolver<ResourceResolver, string, Resource>
    {
        private ResourceGenerator _gen;
        public ResourceResolver()
        {
            _gen = new ResourceGenerator();
        }
        public override Resource Get(string name)
        {
            return Get(name, (r) => {
                return _gen.GetOrCreate(name);
            });    
        }
    }
}
