using System;

namespace SkipList
{
    internal class SkipListNode
    {
        public object Value;

        SkipListNode Next;
        SkipListNode Previous;

        SkipListNode Down;
    }
}
