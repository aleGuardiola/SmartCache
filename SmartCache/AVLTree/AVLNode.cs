using System;
using System.Collections.Generic;
using System.Text;

namespace AVLTree
{
    internal class AVLNode<TKey, TValue>
    {
        public AVLNode<TKey, TValue> Parent;
        public AVLNode<TKey, TValue> Left;
        public AVLNode<TKey, TValue> Right;
        public TKey Key;
        public TValue Value;
        public int Balance;
    }
}
