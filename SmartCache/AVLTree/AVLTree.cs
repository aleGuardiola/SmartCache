using System;
using System.Collections;
using System.Collections.Generic;

namespace AVLTree
{
    public class AVLTree<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>, ICollection, IDictionary
    {
        BinaryTree<TKey, TValue> _binaryTree;

        public AVLTree() : this(Comparer<TKey>.Default) { }
        
        public AVLTree(IComparer<TKey> comparer)
        {
            _binaryTree = new BinaryTree<TKey, TValue>(comparer);
        }

        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                var exist = TryGetValue(key, out value);
                if (!exist)
                    throw new KeyNotFoundException();
                return value;
            }
            set
            {
                Add(key, value);
            }
        }

        public object this[object key] { get => this[(TKey)key]; set => this[(TKey)key] = (TValue)value; }
        TValue IDictionary<TKey, TValue>.this[TKey key] { get => this[key]; set => this[key] = value; }

        public int Count
        {
            get
            {
                lock(_binaryTree)
                {
                    return _binaryTree.Count;
                }
            }
        }

        public bool IsReadOnly => false;

        public IEnumerable<TKey> Keys => _binaryTree.GetKeysSorted();

        public IEnumerable<TValue> Values => _binaryTree.GetValuesSorted();

        public bool IsSynchronized => true;

        public object SyncRoot => _binaryTree;

        public bool IsFixedSize => false;

        ICollection IDictionary.Keys => throw new NotImplementedException();

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => throw new NotImplementedException();

        ICollection IDictionary.Values => throw new NotImplementedException();

        ICollection<TValue> IDictionary<TKey, TValue>.Values => throw new NotImplementedException();

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(object key, object value)
        {
            Add((TKey)key, (TValue)value);
        }

        public void Add(TKey key, TValue value)
        {
            lock(_binaryTree)
            {
                _binaryTree.Insert(key, value);
            }            
        }

        public void Clear()
        {
            lock(_binaryTree)
            {
                _binaryTree.Clear();
            }            
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ContainsKey(item.Key);
        }

        public bool Contains(object key)
        {
            return ContainsKey((TKey)key);
        }

        public bool ContainsKey(TKey key)
        {
            TValue value;

            lock (_binaryTree)
            {
                return _binaryTree.Search(key, out value);
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            lock (_binaryTree)
            {
                for (int i = arrayIndex; i < array.Length; i++)
                {
                    var keyValue = array[i];
                    _binaryTree.Insert(keyValue.Key, keyValue.Value);
                }
            }
        }

        public void CopyTo(Array array, int index)
        {
            CopyTo((KeyValuePair<TKey, TValue>[])array, index);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            lock (_binaryTree)
            {
                return (IEnumerator<KeyValuePair<TKey, TValue>>)_binaryTree.GetKeyValuesSorted().GetEnumerator();
            }           
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
           return Remove(item.Key);           
        }

        public void Remove(object key)
        {
            Remove((TKey)key);
        }

        public bool Remove(TKey key)
        {
            lock (_binaryTree)
            {
                return _binaryTree.Delete(key);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (_binaryTree)
            {
                var result = _binaryTree.Search(key, out value);
                return result;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (_binaryTree)
            {
                return _binaryTree.GetKeyValuesSorted().GetEnumerator();
            }
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            lock (_binaryTree)
            {
                return new DictionaryEnumarator( _binaryTree.GetKeyValuesSorted() );
            }
        }

        public TValue[] GetSortedArrayAscendent()
        {
            return _binaryTree.GetValuesSorted(true);
        }

        public TValue[] GetSortedArrayDescendent()
        {
            return _binaryTree.GetValuesSorted(false);
        }

        public TValue[] GetSortedArrayAscendent(int count)
        {
            return _binaryTree.GetValuesSorted(true, count);
        }

        public TValue[] GetSortedArrayDescendent(int count)
        {
            return _binaryTree.GetValuesSorted(false, count);
        }

        class DictionaryEnumarator : IDictionaryEnumerator
        {
            KeyValuePair<TKey, TValue>[] items;
            int index = -1;
            DictionaryEntry current;

            public DictionaryEnumarator(KeyValuePair<TKey, TValue>[] items)
            {
                this.items = items ?? throw new ArgumentNullException(nameof(items));
                MoveNext();
            }

            public DictionaryEntry Entry => items.Length > 0 ? new DictionaryEntry(items[0].Key, items[0].Value) : default(DictionaryEntry);

            public object Key => current.Key;

            public object Value => current.Value;

            public object Current => current;

            public bool MoveNext()
            {
                index++;
                if (index >= items.Length)
                    return false;

                current = new DictionaryEntry(items[index].Key, items[index].Value);
                return true;
            }

            public void Reset()
            {
                index = -1;
                MoveNext();
            }
        }

    }
}
