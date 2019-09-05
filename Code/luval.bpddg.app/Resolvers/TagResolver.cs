using luval.bpddg.app.Entities;
using luval.bpddg.app.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Resolvers
{
    public class TagResolver : SingletonResolver<TagResolver, string, TagEntity>
    {
        public override TagEntity Get(string key)
        {
            return Get(key, (tag) => {
                var gen = new TagGenerator();
                return gen.GetOrCreate(tag);
            });
        }
    }
}
