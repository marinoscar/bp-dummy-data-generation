using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Resolvers
{
    public abstract class SingletonResolver<TResolver, TKey, TEntity> : Resolver<TKey, TEntity>
        where TResolver : SingletonResolver<TResolver, TKey, TEntity>, new()
    {
        private static TResolver _resolver;

        public static TResolver Instance {
            get
            {
                if (_resolver == null) _resolver = new TResolver();
                return _resolver;
            }
        }

        public abstract TEntity Get(TKey key);
        
    }
}
