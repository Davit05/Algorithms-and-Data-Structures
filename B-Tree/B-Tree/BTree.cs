using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    class BTree<T> where T : IComparable
    {
        public Node<T> Root { get; set; }
        public int BFact { get; }

        public BTree(int branchingFactor)
        {
            BFact = branchingFactor;
            Root = new Node<T>(null);
        }



        public bool Contains(T key)
        {
            return Contains(Root, key);
        }
        private bool Contains(Node<T> cur, T key)
        {
            int i = 0;
            for (; i < cur.Key.Count; i++)
            {
                if (key.CompareTo(cur.Key[i]) == 0)
                    return true;

                if (key.CompareTo(cur.Key[i]) < 0)
                    break;
            }

            if (cur.Child.Count != 0)
                return Contains(cur.Child[i], key);
            else
                return false;
        }



        public Node<T> Search(T key, out int kThKey)
        {
            kThKey = 0;
            Node<T> found = Search(Root, key);
            if (found == null)
                return null;

            for (int i = 0; i < found.Key.Count; i++)
            {
                if (key.CompareTo(found.Key[i]) == 0)
                {
                    kThKey = i;
                    break;
                }
            }
            return found;
        }
        private Node<T> Search(Node<T> cur, T key)
        {
            int i = 0;
            for (; i < cur.Key.Count; i++)
            {
                if (key.CompareTo(cur.Key[i]) == 0)
                    return cur;

                if (key.CompareTo(cur.Key[i]) < 0)
                    break;
            }

            if (cur.Child.Count != 0)
                return Search(cur.Child[i], key);
            else
                return null;
        }



        public void Add(T key)
        {
            if (Contains(key))
                return;

            Node<T> added = Add(Root, key);

            CheckupAdd(added);
        }
        private Node<T> Add(Node<T> cur, T key)
        {
            int i = 0;
            for (; i < cur.Key.Count; i++)
            {
                if (key.CompareTo(cur.Key[i]) <= 0)
                    break;
            }

            if (cur.Child.Count != 0)
                return Add(cur.Child[i], key);
            else
                cur.Key.Insert(i, key);

            return cur;
        }

        private void CheckupAdd(Node<T> cur)
        {
            if (!(cur.Key.Count <= 2 * BFact - 2))
            {
                Split(cur);

                if (cur.Parent != null)
                {
                    int i = 0;
                    cur.Child[0].Parent = cur.Parent;
                    cur.Child[1].Parent = cur.Parent;

                    for (; i < cur.Parent.Key.Count; i++)
                    {
                        if (cur.Key[0].CompareTo(cur.Parent.Key[i]) <= 0)
                            break;
                    }

                    cur.Parent.Key.Insert(i, cur.Key[0]);

                    cur.Parent.Child.RemoveAt(i);
                    cur.Parent.Child.Insert(i, cur.Child[1]);
                    cur.Parent.Child.Insert(i, cur.Child[0]);

                    CheckupAdd(cur.Parent);
                }
            }
        }

        private void Split(Node<T> cur)
        {
            Node<T> child1 = new Node<T>(cur);
            Node<T> child2 = new Node<T>(cur);

            int index1 = 0;
            int index2 = BFact;
            for (int i = 0; i < BFact - 1; i++)
            {
                child1.Key.Add(cur.Key[index1++]);
                child2.Key.Add(cur.Key[index2++]);
            }

            if (cur.Child.Count != 0)
            {
                index1 = 0;
                index2 = BFact;
                for (int i = 0; i < BFact; i++)
                {
                    child1.Child.Add(cur.Child[index1++]);
                    child2.Child.Add(cur.Child[index2++]);
                }
            }
            T t = cur.Key[BFact - 1];
            cur.Key = new List<T>();
            cur.Key.Add(t);

            if (cur.Child.Count != 0)
            {
                for (int i = 0; i < BFact; i++)
                {
                    child1.Child[i].Parent = child1;
                    child2.Child[i].Parent = child2;
                }
            }
            cur.Child = new List<Node<T>>();
            cur.Child.Add(child1);
            cur.Child.Add(child2);
            //cur.Child.AddRange(new Node<T>[] { child1, child2 });
        }



        public void Remove(T key)
        {
            Node<T> found = Search(key, out int kTh);

            if (found == null)
                return;

            found.Key.RemoveAt(kTh);

            Node<T> leaf;
            if (found.Child.Count == 0)
            {
                leaf = found;
            }
            else
            {
                leaf = GetRightSmallestLeaf(found.Child[kTh + 1]);
                found.Key.Insert(kTh, leaf.Key[0]);
                leaf.Key.RemoveAt(0);
            }

            CheckupRemove(leaf);
        }
        private Node<T> GetRightSmallestLeaf(Node<T> cur)
        {

            if (cur.Child.Count == 0)
            {
                return cur;
            }

            return GetRightSmallestLeaf(cur.Child[0]);
        }

        private void CheckupRemove(Node<T> cur)
        {
            if (cur.Parent == null || (BFact - 1 <= cur.Key.Count))
                return;

            int kThChild = 0;
            for (; kThChild < cur.Parent.Key.Count; kThChild++)
            {
                if (cur.Key[0].CompareTo(cur.Parent.Key[kThChild]) < 0)
                    break;
            }

            if (kThChild > 0 && cur.Parent.Child[kThChild - 1].Key.Count >= BFact)
            {
                RightRotate(cur, kThChild);
            }
            else if (kThChild < cur.Parent.Child.Count - 1 && cur.Parent.Child[kThChild + 1].Key.Count >= BFact)
            {
                LeftRotate(cur, kThChild);
            }
            else
            {
                if (kThChild > 0)
                    kThChild--;

                if (cur.Parent.Key.Count == 1)
                    MergeWithRoot(cur.Parent);
                else
                    Merge(cur.Parent, kThChild);

                CheckupRemove(cur.Parent);
            }
        }

        private void LeftRotate(Node<T> cur, int kTh)
        {
            cur.Key.Add(cur.Parent.Key[kTh]);
            cur.Parent.Key.RemoveAt(kTh);
            cur.Parent.Key.Insert(kTh, cur.Parent.Child[kTh + 1].Key[0]);
            cur.Parent.Child[kTh + 1].Key.RemoveAt(0);

            Console.WriteLine("\nLeft rotate");

            if (cur.Child.Count != 0)
            {
                cur.Child.Add(cur.Parent.Child[kTh + 1].Child[0]);
                cur.Parent.Child[kTh + 1].Child.RemoveAt(0);
                cur.Child[cur.Child.Count - 1].Parent = cur;
            }
        }
        private void RightRotate(Node<T> cur, int kTh)
        {
            cur.Key.Insert(0, cur.Parent.Key[kTh - 1]);
            cur.Parent.Key.RemoveAt(kTh - 1);
            cur.Parent.Key.Insert(kTh - 1, cur.Parent.Child[kTh - 1].Key[cur.Parent.Child[kTh - 1].Key.Count - 1]);
            cur.Parent.Child[kTh - 1].Key.RemoveAt(cur.Parent.Child[kTh - 1].Key.Count - 1);

            Console.WriteLine("\nRight rotate");

            if (cur.Child.Count != 0)
            {
                cur.Child.Insert(0, cur.Parent.Child[kTh - 1].Child[cur.Parent.Child[kTh - 1].Child.Count - 1]);
                cur.Parent.Child[kTh - 1].Child.RemoveAt(cur.Parent.Child[kTh - 1].Child.Count - 1);
                cur.Child[0].Parent = cur;
            }
        }

        private void MergeWithRoot(Node<T> root)
        {
            Console.WriteLine("\nMerge with root");

            for (int i = root.Child[0].Key.Count - 1; i >= 0; i--)
            {
                root.Key.Insert(0, root.Child[0].Key[i]);
            }
            foreach (var item in root.Child[1].Key)
            {
                root.Key.Add(item);
            }

            List<Node<T>> child = new List<Node<T>>();
            foreach (var item in root.Child[0].Child)
            {
                child.Add(item);
                item.Parent = root;
            }
            foreach (var item in root.Child[1].Child)
            {
                child.Add(item);
                item.Parent = root;
            }
            root.Child = child;
        }
        private void Merge(Node<T> cur, int kThKey)
        {
            Console.WriteLine("\nMerge");

            cur.Child[kThKey].Key.Add(cur.Key[kThKey]);

            foreach (var item in cur.Child[kThKey + 1].Key)
            {
                cur.Child[kThKey].Key.Add(item);
            }
            foreach (var item in cur.Child[kThKey + 1].Child)
            {
                cur.Child[kThKey].Child.Add(item);
            }

            cur.Key.RemoveAt(kThKey);
            cur.Child.RemoveAt(kThKey + 1);
        }



        public void Print()
        {
            foreach (var key in Root.Key)
            {
                Console.Write(key + " ");
            }
            Console.WriteLine("\n");

            Print(Root);
        }
        private void Print(Node<T> root)
        {
            if (root.Child.Count != 0)
            {
                foreach (var item in root.Child)
                {
                    foreach (var key in item.Key)
                    {
                        Console.Write(key + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                foreach (var item in root.Child)
                {
                    Print(item);
                }
            }
        }
    }
}
