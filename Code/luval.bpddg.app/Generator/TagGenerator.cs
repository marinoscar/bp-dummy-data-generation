using luval.bpddg.app.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luval.Data;

namespace luval.bpddg.app.Generator
{
    public class TagGenerator : GeneratorBase<TagEntity>
    {
        public TagEntity GetOrCreate(string name)
        {
            return Get(name) ?? Create(name);
        }

        private TagEntity Get(string name)
        {
            return Get(new TagEntity() { Tag = name });
        }

        private TagEntity Create(string name)
        {
            Create(new TagEntity() { Tag = name });
            return Get(new TagEntity() { Tag = name});
        }
    }
}
