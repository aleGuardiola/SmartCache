using AVLTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmartCache
{
    public class CachedModel
    {
        public static CachedModelBuilder<Tp> Create<Tp>()
        {
            return new CachedModelBuilder<Tp>();
        }
    }


    public class CachedModel<T, Key> : CachedType<T>
    {
        protected PropertyInfo PrimaryKeyInfo;
        protected AVLTree<Key, T> PrimaryKeyTree;

        Dictionary<PropertyInfo, object> uniqueIndexes;

        internal CachedModel(string primaryKey)
        {
            PrimaryKeyInfo = Type.GetProperty(primaryKey);
            PrimaryKeyTree = new AVLTree<Key, T>();
            uniqueIndexes = new Dictionary<PropertyInfo, object>();
        }
        
        internal void AddUniqueIndex<IndexType>(string propertyName)
        {
            var propInfo = Type.GetProperty(propertyName);
            var tree = new AVLTree<IndexType, T>();
            uniqueIndexes[propInfo] = tree;
        }

        public T this[Key index]
        {
            get
            {
                T result;
                var exist = TryToGet(index, out result);
                if (!exist)
                    throw new KeyNotFoundException($"No object found with primary key{result}");

                return result;
            }
        }

        public bool TryToGet(Key key, out T result)
        {
            return PrimaryKeyTree.TryGetValue(key, out result);
        }

        public override void Add(T item)
        {
            base.Add(item);
            if (item == null)
                return;

            var value = (Key)PrimaryKeyInfo.GetValue(item);
            PrimaryKeyTree[value] = item;

            foreach (var cached in TypesCached)
            {
                var props = Type.GetProperties();
                var properties = props.Where(p => p.PropertyType == cached);

                foreach (var p in properties)
                {
                    var val = p.GetValue(item);
                    Db.UpdateCachedType(cached, val);
                }

                properties = props.Where(p => implementsIEnumerable(p.PropertyType, cached));
                foreach (var p in properties)
                {
                    var val = p.GetValue(item);

                    var enumerable = val as IEnumerable;

                    foreach (var val2 in enumerable)
                        Db.UpdateCachedType(cached, val2);
                }

            }
        }

        public override IEnumerable<T> GetItems()
        {
            return PrimaryKeyTree.GetSortedArrayAscendent();
        }         
    }
}
