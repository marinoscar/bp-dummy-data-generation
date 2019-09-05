using Luval.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Generator
{
    public abstract class GeneratorBase<T>
    {
        public EntityAdapter Adapter { get; private set; }

        public GeneratorBase()
        {
            Adapter = DbHelper.CreateAdapter<T>();
        }

        public virtual T GetOrCreate(T entity)
        {
            var res = Get(entity);
            if (res == null) res = Create(entity);
            return res;
        }

        protected virtual T Get(T entity)
        {
            return Adapter.Read<T>(entity);
        }

        protected virtual T Create(T entity)
        {
            Adapter.Insert(entity);
            return entity;
        }
    }
}
