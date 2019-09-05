using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luval.bpddg.app.Resolvers
{
    public abstract class Resolver<TKey, TEntity>
    {

        protected Resolver()
        {

        }

        private static Dictionary<TKey, TEntity> _items;
        public Dictionary<TKey, TEntity> Items {
            get {
                if (_items == null) _items = new Dictionary<TKey, TEntity>();
                return _items;
            }
        }

        public virtual TEntity Get(TKey key, Func<TKey, TEntity> getEntity)
        {
            if (Items.ContainsKey(key)) return Items[key];
            var entity = getEntity(key);
            Items[key] = entity;
            return entity;
        }
    }
}
