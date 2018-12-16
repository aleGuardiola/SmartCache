using System;
using System.Collections.Generic;
using System.Text;

namespace AVLTree
{
    internal class BinaryTree<TKey, TValue>
    {
        IComparer<TKey> _comparer;
        AVLNode<TKey, TValue> _root = null;
        int count;

        public BinaryTree(IComparer<TKey> comparer)
        {
            _comparer = comparer;
            count = 0;
        }

        public int Count
        {
            get => count;
        }

        public void Insert(TKey key, TValue value)
        {
            if (_root == null)
            {
                _root = new AVLNode<TKey, TValue> { Key = key, Value = value };
                count++;
            }
            else
            {
                AVLNode<TKey, TValue> node = _root;

                while (node != null)
                {
                    int compare = _comparer.Compare(key, node.Key);

                    if (compare < 0)
                    {
                        AVLNode<TKey, TValue> left = node.Left;

                        if (left == null)
                        {
                            node.Left = new AVLNode<TKey, TValue> { Key = key, Value = value, Parent = node };

                            InsertBalance(node, 1);
                            count++;

                            return;
                        }
                        else
                        {
                            node = left;
                        }
                    }
                    else if (compare > 0)
                    {
                        AVLNode<TKey, TValue> right = node.Right;

                        if (right == null)
                        {
                            node.Right = new AVLNode<TKey, TValue> { Key = key, Value = value, Parent = node };

                            InsertBalance(node, -1);
                            count++;

                            return;
                        }
                        else
                        {
                            node = right;
                        }
                    }
                    else
                    {
                        node.Value = value;

                        return;
                    }
                }
            }
        }

        private void InsertBalance(AVLNode<TKey, TValue> node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 0)
                {
                    return;
                }
                else if (balance == 2)
                {
                    if (node.Left.Balance == 1)
                    {
                        RotateRight(node);
                    }
                    else
                    {
                        RotateLeftRight(node);
                    }

                    return;
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance == -1)
                    {
                        RotateLeft(node);
                    }
                    else
                    {
                        RotateRightLeft(node);
                    }

                    return;
                }

                AVLNode<TKey, TValue> parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.Left == node ? 1 : -1;
                }

                node = parent;
            }
        }

        private AVLNode<TKey, TValue> RotateLeft(AVLNode<TKey, TValue> node)
        {
            AVLNode<TKey, TValue> right = node.Right;
            AVLNode<TKey, TValue> rightLeft = right.Left;
            AVLNode<TKey, TValue> parent = node.Parent;

            right.Parent = parent;
            right.Left = node;
            node.Right = rightLeft;
            node.Parent = right;

            if (rightLeft != null)
            {
                rightLeft.Parent = node;
            }

            if (node == _root)
            {
                _root = right;
            }
            else if (parent.Right == node)
            {
                parent.Right = right;
            }
            else
            {
                parent.Left = right;
            }

            right.Balance++;
            node.Balance = -right.Balance;

            return right;
        }

        private AVLNode<TKey, TValue> RotateRight(AVLNode<TKey, TValue> node)
        {
            AVLNode<TKey, TValue> left = node.Left;
            AVLNode<TKey, TValue> leftRight = left.Right;
            AVLNode<TKey, TValue> parent = node.Parent;

            left.Parent = parent;
            left.Right = node;
            node.Left = leftRight;
            node.Parent = left;

            if (leftRight != null)
            {
                leftRight.Parent = node;
            }

            if (node == _root)
            {
                _root = left;
            }
            else if (parent.Left == node)
            {
                parent.Left = left;
            }
            else
            {
                parent.Right = left;
            }

            left.Balance--;
            node.Balance = -left.Balance;

            return left;
        }

        private AVLNode<TKey, TValue> RotateLeftRight(AVLNode<TKey, TValue> node)
        {
            AVLNode<TKey, TValue> left = node.Left;
            AVLNode<TKey, TValue> leftRight = left.Right;
            AVLNode<TKey, TValue> parent = node.Parent;
            AVLNode<TKey, TValue> leftRightRight = leftRight.Right;
            AVLNode<TKey, TValue> leftRightLeft = leftRight.Left;

            leftRight.Parent = parent;
            node.Left = leftRightRight;
            left.Right = leftRightLeft;
            leftRight.Left = left;
            leftRight.Right = node;
            left.Parent = leftRight;
            node.Parent = leftRight;

            if (leftRightRight != null)
            {
                leftRightRight.Parent = node;
            }

            if (leftRightLeft != null)
            {
                leftRightLeft.Parent = left;
            }

            if (node == _root)
            {
                _root = leftRight;
            }
            else if (parent.Left == node)
            {
                parent.Left = leftRight;
            }
            else
            {
                parent.Right = leftRight;
            }

            if (leftRight.Balance == -1)
            {
                node.Balance = 0;
                left.Balance = 1;
            }
            else if (leftRight.Balance == 0)
            {
                node.Balance = 0;
                left.Balance = 0;
            }
            else
            {
                node.Balance = -1;
                left.Balance = 0;
            }

            leftRight.Balance = 0;

            return leftRight;
        }

        private AVLNode<TKey, TValue> RotateRightLeft(AVLNode<TKey, TValue> node)
        {
            AVLNode<TKey, TValue> right = node.Right;
            AVLNode<TKey, TValue> rightLeft = right.Left;
            AVLNode<TKey, TValue> parent = node.Parent;
            AVLNode<TKey, TValue> rightLeftLeft = rightLeft.Left;
            AVLNode<TKey, TValue> rightLeftRight = rightLeft.Right;

            rightLeft.Parent = parent;
            node.Right = rightLeftLeft;
            right.Left = rightLeftRight;
            rightLeft.Right = right;
            rightLeft.Left = node;
            right.Parent = rightLeft;
            node.Parent = rightLeft;

            if (rightLeftLeft != null)
            {
                rightLeftLeft.Parent = node;
            }

            if (rightLeftRight != null)
            {
                rightLeftRight.Parent = right;
            }

            if (node == _root)
            {
                _root = rightLeft;
            }
            else if (parent.Right == node)
            {
                parent.Right = rightLeft;
            }
            else
            {
                parent.Left = rightLeft;
            }

            if (rightLeft.Balance == 1)
            {
                node.Balance = 0;
                right.Balance = -1;
            }
            else if (rightLeft.Balance == 0)
            {
                node.Balance = 0;
                right.Balance = 0;
            }
            else
            {
                node.Balance = 1;
                right.Balance = 0;
            }

            rightLeft.Balance = 0;

            return rightLeft;
        }


        public bool Delete(TKey key)
        {
            AVLNode<TKey, TValue> node = _root;

            while (node != null)
            {
                if (_comparer.Compare(key, node.Key) < 0)
                {
                    node = node.Left;
                }
                else if (_comparer.Compare(key, node.Key) > 0)
                {
                    node = node.Right;
                }
                else
                {
                    AVLNode<TKey, TValue> left = node.Left;
                    AVLNode<TKey, TValue> right = node.Right;

                    if (left == null)
                    {
                        if (right == null)
                        {
                            if (node == _root)
                            {
                                _root = null;
                            }
                            else
                            {
                                AVLNode<TKey, TValue> parent = node.Parent;

                                if (parent.Left == node)
                                {
                                    parent.Left = null;

                                    DeleteBalance(parent, -1);
                                }
                                else
                                {
                                    parent.Right = null;

                                    DeleteBalance(parent, 1);
                                }
                            }
                        }
                        else
                        {
                            Replace(node, right);

                            DeleteBalance(node, 0);
                        }
                    }
                    else if (right == null)
                    {
                        Replace(node, left);

                        DeleteBalance(node, 0);
                    }
                    else
                    {
                        AVLNode<TKey, TValue> successor = right;

                        if (successor.Left == null)
                        {
                            AVLNode<TKey, TValue> parent = node.Parent;

                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;

                            if (left != null)
                            {
                                left.Parent = successor;
                            }

                            if (node == _root)
                            {
                                _root = successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }

                            DeleteBalance(successor, 1);
                        }
                        else
                        {
                            while (successor.Left != null)
                            {
                                successor = successor.Left;
                            }

                            AVLNode<TKey, TValue> parent = node.Parent;
                            AVLNode<TKey, TValue> successorParent = successor.Parent;
                            AVLNode<TKey, TValue> successorRight = successor.Right;

                            if (successorParent.Left == successor)
                            {
                                successorParent.Left = successorRight;
                            }
                            else
                            {
                                successorParent.Right = successorRight;
                            }

                            if (successorRight != null)
                            {
                                successorRight.Parent = successorParent;
                            }

                            successor.Parent = parent;
                            successor.Left = left;
                            successor.Balance = node.Balance;
                            successor.Right = right;
                            right.Parent = successor;

                            if (left != null)
                            {
                                left.Parent = successor;
                            }

                            if (node == _root)
                            {
                                _root = successor;
                            }
                            else
                            {
                                if (parent.Left == node)
                                {
                                    parent.Left = successor;
                                }
                                else
                                {
                                    parent.Right = successor;
                                }
                            }

                            DeleteBalance(successorParent, -1);
                        }
                    }
                    count--;
                    return true;
                }
            }

            return false;
        }

        private void DeleteBalance(AVLNode<TKey, TValue> node, int balance)
        {
            while (node != null)
            {
                balance = (node.Balance += balance);

                if (balance == 2)
                {
                    if (node.Left.Balance >= 0)
                    {
                        node = RotateRight(node);

                        if (node.Balance == -1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = RotateLeftRight(node);
                    }
                }
                else if (balance == -2)
                {
                    if (node.Right.Balance <= 0)
                    {
                        node = RotateLeft(node);

                        if (node.Balance == 1)
                        {
                            return;
                        }
                    }
                    else
                    {
                        node = RotateRightLeft(node);
                    }
                }
                else if (balance != 0)
                {
                    return;
                }

                AVLNode<TKey, TValue> parent = node.Parent;

                if (parent != null)
                {
                    balance = parent.Left == node ? -1 : 1;
                }

                node = parent;
            }
        }

        private static void Replace(AVLNode<TKey, TValue> target, AVLNode<TKey, TValue> source)
        {
            AVLNode<TKey, TValue> left = source.Left;
            AVLNode<TKey, TValue> right = source.Right;

            target.Balance = source.Balance;
            target.Key = source.Key;
            target.Value = source.Value;
            target.Left = left;
            target.Right = right;

            if (left != null)
            {
                left.Parent = target;
            }

            if (right != null)
            {
                right.Parent = target;
            }
        }

        public void Clear()
        {
            _root = null;
            count = 0;
        }

        public bool Search(TKey key, out TValue value)
        {
            AVLNode<TKey, TValue> node = _root;

            while (node != null)
            {
                if (_comparer.Compare(key, node.Key) < 0)
                {
                    node = node.Left;
                }
                else if (_comparer.Compare(key, node.Key) > 0)
                {
                    node = node.Right;
                }
                else
                {
                    value = node.Value;

                    return true;
                }
            }

            value = default(TValue);

            return false;
        }

        public KeyValuePair<TKey, TValue>[] GetKeyValuesSorted(bool ascendant  = true)
        {
            var result = new KeyValuePair<TKey, TValue>[count];
            if (ascendant)
            {
                GetKeyValuesSortedAscendant(_root, result, 0);
            }
            else
            {
                GetKeyValuesSortedDescendant(_root, result, 0);
            }
            return result;
        }

        void GetKeyValuesSortedAscendant(AVLNode<TKey, TValue> node, KeyValuePair<TKey, TValue>[] array, int? pos)
        {
            if (node == null)
                return;

            GetKeyValuesSortedAscendant(node.Left, array, pos);
            GetKeyValuesSortedAscendant(node.Right, array, pos);

            array[(int)pos++] = new KeyValuePair<TKey, TValue>(node.Key, node.Value);
        }


        void GetKeyValuesSortedDescendant(AVLNode<TKey, TValue> node, KeyValuePair<TKey, TValue>[] array, int? pos)
        {
            if (node == null)
                return;

            GetKeyValuesSortedDescendant(node.Right, array, pos);
            GetKeyValuesSortedDescendant(node.Left, array, pos);

            array[(int)pos++] = new KeyValuePair<TKey, TValue>(node.Key, node.Value);
        }


        public TKey[] GetKeysSorted(bool ascendant = true)
        {
            var result = new TKey[count];
            if (ascendant)
            {
                GetKeysSortedAscendant(_root, result, 0);
            }
            else
            {
                GetKeysSortedDescendant(_root, result, 0);
            }
            return result;
        }

        void GetKeysSortedAscendant(AVLNode<TKey, TValue> node, TKey[] array, int? pos)
        {
            if (node == null)
                return;

            GetKeysSortedAscendant(node.Left, array, pos);
            GetKeysSortedAscendant(node.Right, array, pos);

            array[(int)pos++] = node.Key;
        }
        
        void GetKeysSortedDescendant(AVLNode<TKey, TValue> node, TKey[] array, int? pos)
        {
            if (node == null)
                return;

            GetKeysSortedDescendant(node.Right, array, pos);
            GetKeysSortedDescendant(node.Left, array, pos);

            array[(int)pos++] = node.Key;
        }


        public TValue[] GetValuesSorted(bool ascendant = true)
        {
            var result = new TValue[count];
            if (ascendant)
            {
                GetValuesSortedAscendant(_root, result, 0);
            }
            else
            {
                GetValuesSortedDescendant(_root, result, 0);
            }
            return result;
        }

        void GetValuesSortedAscendant(AVLNode<TKey, TValue> node, TValue[] array, int? pos)
        {
            if (node == null)
                return;

            GetValuesSortedAscendant(node.Left, array, pos);
            GetValuesSortedAscendant(node.Right, array, pos);

            array[(int)pos++] = node.Value;
        }

        void GetValuesSortedDescendant(AVLNode<TKey, TValue> node, TValue[] array, int? pos)
        {
            if (node == null)
                return;

            GetValuesSortedDescendant(node.Right, array, pos);
            GetValuesSortedDescendant(node.Left, array, pos);

            array[(int)pos++] = node.Value;
        }
    }
}
