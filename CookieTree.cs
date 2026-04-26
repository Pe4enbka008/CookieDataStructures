using smth;
using System;

/*
    CookieDataStructure
    Copyright (C) 2026 Pe4enbka008 (Helen Ivanova) 

    This code is free software: you can redistribute it and/or modify 
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or any later version. 

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

    See the GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program. If not, see <https://www.gnu.org/licenses/>.
*/


namespace smth
{
    public class CookieBinNode<T>
    {
        private T value;
        private CookieBinNode<T>? left;
        private CookieBinNode<T>? right;

        public CookieBinNode(T value)
        {
            this.value = value;
            this.left = null;
            this.right = null;
        } // __init__
        public CookieBinNode() : this(default)   // for setters
        { }

        // Getters/Setters

        public T Value { get { return this.value; } }
        public CookieBinNode<T>? Left { get { return this.left; } }
        public CookieBinNode<T>? Right { get { return this.right; } }

        public T GetValue() { return this.value; }
        public CookieBinNode<T>? GetLeft() { return this.Left; }
        public CookieBinNode<T>? GetRight() { return this.Right; }


        public void SetValue(T value) { this.value = value; }
        public void SetLeft(CookieBinNode<T>? value) { this.left = value; }
        public void SetLeft(T? value) { this.left = new(value); }
        public void SetRight(CookieBinNode<T>? value) { this.right = value; }
        public void SetRight(T? value) { this.right = new(value); }



        // For tree:
        public static bool operator >(CookieBinNode<T> a, CookieBinNode<T> b)
        { return Comparer<T>.Default.Compare(a.Value, b.Value) > 0; }

        public static bool operator <(CookieBinNode<T> a, CookieBinNode<T> b)
        { return Comparer<T>.Default.Compare(a.Value, b.Value) < 0; }


        public static bool operator >(CookieBinNode<T> a, T b)
        { return Comparer<T>.Default.Compare(a.Value, b) > 0; }

        public static bool operator <(CookieBinNode<T> a, T b)
        { return Comparer<T>.Default.Compare(a.Value, b) < 0; }



        // override:
        public override string ToString()
        { return $"{this.value}"; }

    } // CookieBinNode


    public class CookieHoldingLineHelper
    {
        // prints:

        public static void Print2LinkedList<T>(CookieBinNode<T> bin_node)
        { Print2LinkedList(bin_node, bin_node.GetLeft() == null); Console.Write("null\n"); }

        private static void Print2LinkedList<T>(CookieBinNode<T> bin_node, bool start_printing)
        {
            if (bin_node == null)
                return;

            if (start_printing)
            {
                Console.Write(bin_node.ToString() + " -> ");
                Print2LinkedList(bin_node.GetRight(), true);
            } // if
            else
                Print2LinkedList(bin_node.GetLeft(), (bin_node.GetLeft()).GetLeft() == null);
        } // Print2LinkedList


        public static void Print2LinkedListRight<T>(CookieBinNode<T> bin_node)
        { Print2LinkedList(bin_node, true); Console.Write("null\n"); }


        public static void Print2LinkedListLeft<T>(CookieBinNode<T> bin_node)
        { Console.Write("null"); Print2LinkedListLeftRecursive(bin_node); }

        public static void Print2LinkedListLeftRecursive<T>(CookieBinNode<T> bin_node)
        {
            if (bin_node != null)
            {
                Print2LinkedListLeftRecursive(bin_node.GetLeft());
                Console.Write(" <- " + bin_node.ToString());
            } // if
        } // Print2LinkedListLeftRecursive



        // Adds:

        public static void AddRight<T>(CookieBinNode<T> bin_node, T value)
        {
            if (bin_node != null)
            {
                CookieBinNode<T> temp = new CookieBinNode<T>(value);

                temp.SetRight(bin_node.GetRight());
                bin_node.SetRight(temp);
                temp.SetLeft(bin_node);
                if (temp.GetRight() != null)
                    temp.GetRight().SetLeft(temp);
            } // if
        } // AddRight
        public static void AddRight<T>(CookieBinNode<T> bin_node, CookieBinNode<T> value)
        {
            if (bin_node != null)
            {
                value.SetRight(bin_node.GetRight());
                bin_node.SetRight(value);
                value.SetLeft(bin_node);
                if (value.GetRight() != null)
                    value.GetRight().SetLeft(value);
            } // if
        } // AddRight

        public static void AddLeft<T>(CookieBinNode<T> bin_node, T value)
        {
            if (bin_node != null)
            {
                CookieBinNode<T> temp = new CookieBinNode<T>(value);

                temp.SetLeft(bin_node.GetLeft());
                bin_node.SetLeft(temp);
                temp.SetRight(bin_node);
                if (temp.GetLeft() != null)
                    temp.GetLeft().SetRight(temp);
            } // if
        } // AddLeft
        public static void AddLeft<T>(CookieBinNode<T> bin_node, CookieBinNode<T> value)
        {
            if (bin_node != null)
            {
                value.SetLeft(bin_node.GetLeft());
                bin_node.SetLeft(value);
                value.SetRight(bin_node);
                if (value.GetLeft() != null)
                    value.GetLeft().SetRight(value);
            } // if
        } // AddLeft

        public static void AddToHead<T>(CookieBinNode<T> bin_node)
        {
            if (bin_node == null) return;
            CookieBinNode<T> left = GetLeftest(bin_node);
            left.SetLeft(bin_node);
            bin_node.SetRight(left);
        } // AddToHead
        public static void AddToTail<T>(CookieBinNode<T> bin_node)
        {
            if (bin_node == null) return;
            CookieBinNode<T> right = GetRightest(bin_node);
            right.SetRight(bin_node);
            bin_node.SetLeft(right);
        } // AddToTail



        public static CookieBinNode<T> GetLeftest<T>(CookieBinNode<T> bin_node)
        {
            if (bin_node == null)
                return default;
            if (bin_node.Left == null)
                return bin_node;
            return GetLeftest(bin_node.Left);
        } // GetLeftest
        public static T GetLeftestValue<T>(CookieBinNode<T> bin_node)
        { return GetLeftest(bin_node).Value; } 


        public static CookieBinNode<T> GetRightest<T>(CookieBinNode<T> bin_node)
        {
            if (bin_node == null)
                return default;
            if (bin_node.Right == null)
                return bin_node;
            return GetRightest(bin_node.Right);
        } // GetRightest
        public static T GetRightestValue<T>(CookieBinNode<T> bin_node)
        { return GetRightest(bin_node).Value; }



    } // CookieHoldingLineHelper

    public class CookieTree<TreeType> : IEnumerable<TreeType>
    {
        private CookieBinNode<TreeType>? root;

        public CookieTree(CookieBinNode<TreeType> root)
        { this.root = root; }

        public CookieTree() : this(null)
        { }

        /// <summary>
        /// Is the list ReadOnly
        /// </summary>
        public bool IsReadOnly { get; }


        // insert:

        public void Insert(TreeType value)
        { this.root = InsertRecursive(this.root, value); }
        private CookieBinNode<TreeType> InsertRecursive(CookieBinNode<TreeType> root, TreeType value)
        {
            if (root == null)
            {
                root = new CookieBinNode<TreeType>(value);
                return root;
            } // if

            if (root > value)
                root.SetLeft(InsertRecursive(root.Left, value));
            else if (root < value)
                root.SetRight(InsertRecursive(root.Right, value));
            return root;
        } // InsertRecursive


        // remove:

        public void Remove(TreeType value)
        { this.root = RemoveRecursive(this.root, value); }
        private CookieBinNode<TreeType> RemoveRecursive(CookieBinNode<TreeType> root, TreeType value)
        {
            if (root == null)
                return root;

            if (root > value)
                root.SetLeft(RemoveRecursive(root.Left, value));
            else if (root < value)
                root.SetRight(RemoveRecursive(root.Right, value));
            else
            {
                CookieTree<TreeType> right_branch = new(root.Right);
                CookieTree<TreeType> left_branch = new(root.Left);
                foreach (TreeType tree_value in right_branch)
                    left_branch.Insert(tree_value);
                root = left_branch.root;
            } // else

            return root;
        } // RemoveRecursive



        // funzies:

        public bool Search(TreeType value)
        { return SearchRecursive(this.root, value); }
        private bool SearchRecursive(CookieBinNode<TreeType> root, TreeType value)
        {
            if (root == null)
                return false;

            if (root.Value.Equals(value))
                return true;

            bool found = SearchRecursive(root.Right, value);
            if (root > value)
                found = SearchRecursive(root.Left, value);
            return found;
        } // SearchRecursive


        public int Length()
        { return CountElements(this.root); }
        private int CountElements(CookieBinNode<TreeType> root)
        {
            if (root == null)
                return 0;
            return 1 + CountElements(root.Left) + CountElements(root.Right);
        } // CountElements


        public int Count(TreeType value)
        { return CountElement(this.root, value); }
        private int CountElement(CookieBinNode<TreeType> root, TreeType value)
        {
            if (root == null)
                return 0;
            int add = 0;
            if (root.Value.Equals(value))
                add = 1;
            return add + CountElement(root.Left, value) + CountElement(root.Right, value);
        } // CountElement


        public int CountLeaves()
        { return CountLeaves(this.root); }
        private int CountLeaves(CookieBinNode<TreeType> root)
        {
            if (root == null)
                return 0;
            int add = 0;
            if (root.Left == root.Right && root.Left == null)
                add = 1;
            return add + CountLeaves(root.Left) + CountLeaves(root.Right);
        } // CountLeaves


        public int GetHight()
        { return GetHight(this.root); }
        private int GetHight(CookieBinNode<TreeType> root) 
        {
            if (root == null) return 0;
            int count_left = 1 + GetHight(root.Left);
            int count_right = 1 + GetHight(root.Right);
            return Math.Max(count_left, count_right);
        } // GetHight


        // type of trees:

        public bool IsFull()
        { return IsFull(this.root); }
        private bool IsFull(CookieBinNode<TreeType>? bin_tree) // 17 
        {
            if (bin_tree == null)
                return true;
            bool check = (bin_tree.Right == null || bin_tree.Left == null) && !(bin_tree.Right == null && bin_tree.Left == null);
            return !check && IsFull(bin_tree.Left) && IsFull(bin_tree.Right);
        } // IsFull


        public bool IsComplete()
        { return IsComplete(this.root); }
        private bool IsComplete(CookieBinNode<TreeType>? bin_tree)
        {
            if (bin_tree == null)
                return false;

            CookieQueue<CookieBinNode<TreeType>> check_list = new(bin_tree);
            bool reached_empty = false;

            while (check_list.IsEmpty())
            {
                CookieBinNode<TreeType> current = check_list.RemoveValue();
                if (current.Left != null)
                {
                    if (reached_empty) return false;
                    check_list.Insert(current.Left);
                } // if
                else reached_empty = true;

                if (current.Right != null)
                {
                    if (reached_empty) return false;
                    check_list.Insert(current.Right);
                } // if
                else reached_empty = true;
            } // while

            return true;
        } // IsComplete


        public bool IsSpanningTree()
        { return IsComplete(this.root); }

        private bool IsSpanningTree(CookieBinNode<TreeType>? nodes)
        {
            if (nodes == null) return true;

            if (nodes.Left != null)
                if (nodes < nodes.Left)
                    return false;
            if (nodes.Right != null)
                if (nodes > nodes.Right)
                    return false;
            return IsSpanningTree(nodes.Left) && IsSpanningTree(nodes.Right);
        } // IsSpanningTree


        // IEnumerable
        public System.Collections.Generic.IEnumerator<TreeType> GetEnumerator()
        { return InOrderYeald(root).GetEnumerator(); }

        System.Collections.IEnumerator
        System.Collections.IEnumerable.GetEnumerator()
        { return GetEnumerator(); }

        private IEnumerable<TreeType> InOrderYeald(CookieBinNode<TreeType>? node)
        {
            if (node == null)
                yield break;

            foreach (var v in InOrderYeald(node.Left))
                yield return v;

            yield return node.Value;

            foreach (var v in InOrderYeald(node.Right))
                yield return v;
        } // InOrderYeald



        // prints:

        public void InOrderTraversal()
        { InOrderRecursive(this.root); Console.WriteLine(" "); }
        private void InOrderRecursive(CookieBinNode<TreeType> root)
        {
            if (root != null)
            {
                InOrderRecursive(root.Left);
                Console.Write(root.Value + " ");
                InOrderRecursive(root.Right);
            } // if
        } // InOrderRecursive


        public void PreOrderTraversal()
        { PreOrderRecursive(this.root); Console.WriteLine(" "); }
        private void PreOrderRecursive(CookieBinNode<TreeType> root)
        {
            if (root != null)
            {
                Console.Write(root.Value + " ");
                PreOrderRecursive(root.Left);
                PreOrderRecursive(root.Right);
            } // if 
        } // PreOrderRecursive


        public void PostOrderTraversal()
        { PostOrderRecursive(this.root); Console.WriteLine(" "); }
        private void PostOrderRecursive(CookieBinNode<TreeType> root)
        {
            if (root != null)
            {
                PostOrderRecursive(root.Left);
                PostOrderRecursive(root.Right);
                Console.Write(root.Value + " ");
            } // if
        } // PostOrderRecursive

    } // class CookieTree


} // namespace first_smth

