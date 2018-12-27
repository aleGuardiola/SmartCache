using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SkipList
{
    public class SkipList<Key, Value> : IDictionary<Key, Value>
    {
        SkipListCore<Key, Value> skipListCore;

        public SkipList() : this( Comparer<Key>.Default, 33 )
        {
            
        }

        public SkipList(IComparer<Key> comparer) : this(comparer, 33)
        {

        }

        public SkipList(int maxLevels) : this(Comparer<Key>.Default, maxLevels)
        {

        }

        public SkipList(IComparer<Key> comparer, int maxLevels)
        {
            skipListCore = new SkipListCore<Key, Value>(comparer, maxLevels);
        }

        public Value this[Key key] {
            get {

                Value result;

                if (!skipListCore.TryGet(key, out result))
                    throw new KeyNotFoundException($"item with key {key} cannot be found in the list.");

                return result;
            }
        set => skipListCore.InsertOrUpdate(key, value); }

        public ICollection<Key> Keys => throw new NotImplementedException();

        public ICollection<Value> Values => throw new NotImplementedException();

        public int Count => skipListCore.Count;

        public bool IsReadOnly => false;

        public void Add(Key key, Value value)
        {
            skipListCore.InsertOrUpdate(key, value);
        }

        public void Add(KeyValuePair<Key, Value> item)
        {
            skipListCore.InsertOrUpdate(item.Key, item.Value);
        }

        public void Clear()
        {
            skipListCore.Clear();
        }

        public bool Contains(KeyValuePair<Key, Value> item)
        {
            Value tmp;
            return TryGetValue(item.Key, out tmp);
        }

        public bool ContainsKey(Key key)
        {
            Value tmp;
            return TryGetValue(key, out tmp);
        }

        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            throw new NotImplementedException(); 
        }

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Key key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<Key, Value> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(Key key, out Value value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
