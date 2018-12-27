using System;
using System.Collections.Generic;

internal class SkipListCore<Key, Val>
{
    private class Node<K, V>
    {
        public Node<K, V>[] Next { get; private set; }
        public KeyValuePair<K, V> Value { get; set; }

        public Node(int level)
        {
            Next = new Node<K, V>[level];
        }
    }

    private Node<Key, Val> _head; // The max. number of levels is 33
    private Random _rand = new Random();
    private int _levels = 1;
    IComparer<Key> _comparer;
    private int _count = 0;
       

    public SkipListCore(IComparer<Key> comparer, int maxLevels = 33)
    {
        _head = new Node<Key, Val>(maxLevels);
        _comparer = comparer;
    }

    public int Count { get => _count; }

    /// <summary>
    /// Inserts a value into the skip list.
    /// </summary>
    public void Insert(Key key, Val value)
    {
        // Determine the level of the new node. Generate a random number R. The number of
        // 1-bits before we encounter the first 0-bit is the level of the node. Since R is
        // 32-bit, the level can be at most 32.
        int level = 0;
        for (int R = _rand.Next(); (R & 1) == 1; R >>= 1)
        {
            level++;
            if (level == _levels) { _levels++; break; }
        }

        // Insert this node into the skip list
        var newNode = new Node<Key, Val>(level + 1);
        newNode.Value = new KeyValuePair<Key, Val>(key, value);
               
        var cur = _head;
        for (int i = _levels - 1; i >= 0; i--)
        {
            for (; cur.Next[i] != null; cur = cur.Next[i])
            {
                var comparision = _comparer.Compare(cur.Next[i].Value.Key, key);
                if ( comparision > 0 ) break;
            }

            if (i <= level)
            {
                newNode.Next[i] = cur.Next[i];
                cur.Next[i] = newNode;
            }
        }
        _count++;
    }

    /// <summary>
    /// Inserts a value into the skip list.
    /// </summary>
    public void InsertOrUpdate(Key key, Val value)
    {
        // Determine the level of the new node. Generate a random number R. The number of
        // 1-bits before we encounter the first 0-bit is the level of the node. Since R is
        // 32-bit, the level can be at most 32.
        int level = 0;
        for (int R = _rand.Next(); (R & 1) == 1; R >>= 1)
        {
            level++;
            if (level == _levels) { _levels++; break; }
        }

        // Insert this node into the skip list
        var newNode = new Node<Key, Val>(level + 1);
        newNode.Value = new KeyValuePair<Key, Val>(key, value);

        var cur = _head;
        for (int i = _levels - 1; i >= 0; i--)
        {
            for (; cur.Next[i] != null; cur = cur.Next[i])
            {
                var comparision = _comparer.Compare(cur.Next[i].Value.Key, key);

                if (comparision == 0)
                    cur.Value = new KeyValuePair<Key, Val>(key, value);

                if (comparision > 0) break;
            }

            if (i <= level)
            {
                newNode.Next[i] = cur.Next[i];
                cur.Next[i] = newNode;
            }
        }
        _count++;
    }

    /// <summary>
    /// Returns whether a particular value already exists in the skip list
    /// </summary>
    public bool TryGet(Key key, out Val result)
    {
        var cur = _head;
        for (int i = _levels - 1; i >= 0; i--)
        {
            for (; cur.Next[i] != null; cur = cur.Next[i])
            {
                var comparision = _comparer.Compare(cur.Next[i].Value.Key, key);
                if (comparision > 0) break;
                if (comparision == 0)
                {
                    result = cur.Value.Value;
                    return true;
                } 
            }
        }
        result = default(Val);
        return false;
    }

    public IEnumerable<Val> Get(Key key)
    {
        var cur = _head;
        for (int i = _levels - 1; i >= 0; i--)
        {
            for (; cur.Next[i] != null; cur = cur.Next[i])
            {
                var comparision = _comparer.Compare(cur.Next[i].Value.Key, key);
                if (comparision > 0) break;
                if (comparision == 0)
                {
                    yield return cur.Value.Value;
                }
            }
        }        
    }
       
    public IEnumerable<Val> Get(Key key, int limit)
    {
        var cur = _head;
        var count = 0;
        bool done = false;
        for (int i = _levels - 1; i >= 0; i--)
        {
            for (; cur.Next[i] != null; cur = cur.Next[i])
            {
                var comparision = _comparer.Compare(cur.Next[i].Value.Key, key);
                if (comparision > 0) break;
                if (comparision == 0)
                {
                    yield return cur.Value.Value;
                    count++;
                    if (count > limit)
                    {
                        done = true;
                        break;
                    }
                }
            }
            if (done) break;
        }
    }

    /// <summary>
    /// Attempts to remove one occurence of a particular value from the skip list. Returns
    /// whether the value was found in the skip list.
    /// </summary>
    public bool Remove(Key key)
    {
        var cur = _head;

        bool found = false;
        for (int i = _levels - 1; i >= 0; i--)
        {
            for (; cur.Next[i] != null; cur = cur.Next[i])
            {
                var comparision = _comparer.Compare(cur.Next[i].Value.Key, key);
                if (comparision == 0)
                {
                    found = true;
                    cur.Next[i] = cur.Next[i].Next[i];
                    break;
                }

                if (comparision > 0) break;
            }
        }
        _count = found ? _count - 1 : _count;
        return found;
    }

    public void Clear()
    {
        _count = 0;
        for (int i = 0; i < _head.Next.Length; i++)
            _head.Next[i] = null;
    }

    /// <summary>
    /// Produces an enumerator that iterates over elements in the skip list in order.
    /// </summary>
    public IEnumerable<Val> OrderAscending()
    {
        var cur = _head.Next[0];
        while (cur != null)
        {
            yield return cur.Value.Value;
            cur = cur.Next[0];
        }
    }

    public IEnumerable<Val> OrderDescending()
    {
        Stack<Val> stack = new Stack<Val>();
        var cur = _head.Next[0];
        while (cur != null)
        {
            stack.Push(cur.Value.Value);
            cur = cur.Next[0];
        }

        while ((stack.Count > 0))
            yield return stack.Pop();
    }

    public IEnumerable<Val> OrderAscending(int limit)
    {
        var cur = _head.Next[0];
        var count = 0;
        while (cur != null || count < limit)
        {
            yield return cur.Value.Value;
            cur = cur.Next[0];
            count++;
        }
    }

    public IEnumerable<Val> OrderDescending(int limit)
    {
        Stack<Val> stack = new Stack<Val>();
        var cur = _head.Next[0];
        int count = 0;
        while (cur != null)
        {
            if(_count - count <= limit)
              stack.Push(cur.Value.Value);

            cur = cur.Next[0];
            count++;
        }

        while ((stack.Count > 0))        
            yield return stack.Pop();          
    }

}