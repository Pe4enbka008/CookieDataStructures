using System;
using System.Collections;

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



/*
CookieDataStructure: CookieBinNode contains
    GetValue - public function
    GetLeft - public function
    GetRight - public function
    SetValue - public function
    SetLeft - public function
    SetRight - public function 
    operator > - public static function (2 overloads: CookieBinNode<T>, T)
    operator < - public static function (2 overloads: CookieBinNode<T>, T)
    ToString - public function

CookieDataStructure: CookieHoldingLineHelper contains
    Print2LinkedList - private static function (2-arg helper) and public static function (1-arg, overloaded)
    Print2LinkedListRight - public static function
    Print2LinkedListLeftRecursive - public static function
    Print2LinkedListLeft - public static function
    AddRight - public static function (2 overloads: T, CookieBinNode<T>)
    AddLeft - public static function (2 overloads: T, CookieBinNode<T>)
    AddToHead - public static function (2 overloads: T, CookieBinNode<T>)
    AddToTail - public static function (2 overloads: T, CookieBinNode<T>)
    GetLeftest - public static function
    GetRightest - public static function

CookieDataStructure: CookieTree contains
    IsEmpty - public function
    Insert - public function
    InsertRecursive - private function
    Remove - public function
    RemoveRecursive - private function
    Search - public function
    SearchRecursive - private function
    Length - public function
    CountElements - private function
    Count - public function
    CountElement - private function
    CountLeaves - public function (and matching private overload)
    GetHight - public function (and matching private overload)
    IsFull - public function (and matching private overload)
    IsComplete - public function (and matching private overload)
    IsSpanningTree - public function (and matching private overload)
    GetEnumerator - public function (and IEnumerable.GetEnumerator implementation)
    InOrderYield - private function
    InOrderTraversal - public function
    InOrderRecursive - private function
    PreOrderTraversal - public function
    PreOrderRecursive - private function
    PostOrderTraversal - public function
    PostOrderRecursive - private function
*/


namespace smth
{
    /// <summary>
    /// Node for Tree or double-linked list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CookieBinNode<T>
    {
        private T value;
        private CookieBinNode<T>? left;
        private CookieBinNode<T>? right;

        /// <summary>
        /// CookieBinNode constructor
        /// </summary>
        /// <param name="value">value to save in the node</param>
        public CookieBinNode(T value)
        {
            this.value = value;
            this.left = null;
            this.right = null;
        } // __init__
        /// <summary>
        /// CookieBinNode constructor
        /// </summary>
        /// <param name="value">value to save in the node</param>
        /// <param name="left">left node</param>
        /// <param name="right">right node</param>
        public CookieBinNode(T value, CookieBinNode<T> left, CookieBinNode<T> right)
        {
            this.value = value;
            this.left = left;
            this.right = right;
        } // __init__


        // Getters/Setters

        /// <summary>Gets the value stored in this node</summary>
        public T Value { get { return this.value; } }
        /// <summary>Gets the left child node</summary>
        public CookieBinNode<T>? Left { get { return this.left; } }
        /// <summary>Gets the right child node</summary>
        public CookieBinNode<T>? Right { get { return this.right; } }

        /// <summary>Returns the value stored in this node</summary>
        /// <returns>The node's value</returns>
        public T GetValue() { return this.value; }
        /// <summary>Returns the left child node</summary>
        /// <returns>The left child, or null if there isn't one</returns>
        public CookieBinNode<T>? GetLeft() { return this.Left; }
        /// <summary>Returns the right child node</summary>
        /// <returns>The right child, or null if there isn't one</returns>
        public CookieBinNode<T>? GetRight() { return this.Right; }


        /// <summary>Sets the value stored in this node</summary>
        /// <param name="value">New value to store</param>
        public void SetValue(T value) { this.value = value; }

        /// <summary>Sets the left child to an existing node</summary>
        /// <param name="value">The node to use as the left child</param>
        public void SetLeft(CookieBinNode<T>? value) { this.left = value; }

        /// <summary>Sets the right child to an existing node</summary>
        /// <param name="value">The node to use as the right child</param>
        public void SetRight(CookieBinNode<T>? value) { this.right = value; }




        // For tree:
        /// <summary>Compares two nodes' values</summary>
        /// <returns>True if a's value is greater than b's value</returns>
        public static bool operator >(CookieBinNode<T> a, CookieBinNode<T> b)
        { return Comparer<T>.Default.Compare(a.Value, b.Value) > 0; }

        /// <summary>Compares two nodes' values</summary>
        /// <returns>True if a's value is less than b's value</returns>
        public static bool operator <(CookieBinNode<T> a, CookieBinNode<T> b)
        { return Comparer<T>.Default.Compare(a.Value, b.Value) < 0; }


        /// <summary>Compares a node's value against a raw value</summary>
        /// <returns>True if a's value is greater than b</returns>
        public static bool operator >(CookieBinNode<T> a, T b)
        { return Comparer<T>.Default.Compare(a.Value, b) > 0; }

        /// <summary>Compares a node's value against a raw value</summary>
        /// <returns>True if a's value is less than b</returns>
        public static bool operator <(CookieBinNode<T> a, T b)
        { return Comparer<T>.Default.Compare(a.Value, b) < 0; }



        // override:
        /// <summary>Returns the value stored in this node as a string</summary>
        /// <returns>Value as a string</returns>
        public override string ToString()
        { return $"{this.value}"; }

    } // CookieBinNode



    /// <summary>
    /// Static class for dealing with double-linked list
    /// </summary>
    public class CookieHoldingLineHelper
    {
        private static string className = "CookieHoldingLineHelper";

        // prints:

        /// <summary>Internal recursive helper for printing a binary node as a doubly-linked list</summary>
        /// <param name="bin_node">Node to print from</param>
        /// <param name="start_printing">Whether printing has reached the leftmost node yet</param>
        /// <returns>String fragment of the linked-list representation</returns>
        private static string Print2LinkedList<T>(CookieBinNode<T> bin_node, bool start_printing)
        {
            string return_string = "";
            if (bin_node == null)
                ;
            else if (start_printing)
                return_string = bin_node.ToString() + " -> " + Print2LinkedList(bin_node.GetRight(), true);
            else
                return_string = Print2LinkedList(bin_node.GetLeft(), (bin_node.GetLeft()).GetLeft() == null);
            return return_string;
        } // Print2LinkedList

        /// <summary>Prints the tree/list as a doubly-linked list, left to right, starting from null</summary>
        /// <param name="bin_node">Any node in the linked structure</param>
        /// <returns>String like "null -> a -> b -> c -> null"</returns>
        public static string Print2LinkedList<T>(CookieBinNode<T> bin_node)
        { return "null -> " + Print2LinkedList(bin_node, bin_node.GetLeft() == null) + "null"; }

        /// <summary>Prints the structure rightward starting from the given node</summary>
        /// <param name="bin_node">Node to start printing from</param>
        /// <returns>String like "a -> b -> c -> null"</returns>
        public static string Print2LinkedListRight<T>(CookieBinNode<T> bin_node)
        { return Print2LinkedList(bin_node, true) + "null"; }


        /// <summary>Recursively builds a leftward string representation ending at the given node</summary>
        /// <param name="bin_node">Node to print up to</param>
        /// <returns>String fragment like " <- a <- b"</returns>
        public static string Print2LinkedListLeftRecursive<T>(CookieBinNode<T> bin_node)
        {
            if (bin_node != null)
                return Print2LinkedListLeftRecursive(bin_node.GetLeft()) + " <- " + bin_node.ToString();
            return "";
        } // Print2LinkedListLeftRecursive

        /// <summary>Prints the structure leftward, starting from null, up to the given node</summary>
        /// <param name="bin_node">Node to print up to</param>
        /// <returns>String like "null <- a <- b <- c"</returns>
        public static string Print2LinkedListLeft<T>(CookieBinNode<T> bin_node)
        { return "null" + Print2LinkedListLeftRecursive(bin_node); }


        // Adds:

        /// <summary>Inserts a new node holding the given value to the right of bin_node, fixing up neighbouring links</summary>
        /// <param name="bin_node">Node to insert after</param>
        /// <param name="value">Value to wrap in a new node and insert</param>
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
        /// <summary>Inserts an existing node to the right of bin_node, fixing up neighbouring links</summary>
        /// <param name="bin_node">Node to insert after</param>
        /// <param name="value">Node to insert</param>
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

        /// <summary>Inserts a new node holding the given value to the left of bin_node, fixing up neighbouring links</summary>
        /// <param name="bin_node">Node to insert before</param>
        /// <param name="value">Value to wrap in a new node and insert</param>
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
        /// <summary>Inserts an existing node to the left of bin_node, fixing up neighbouring links</summary>
        /// <param name="bin_node">Node to insert before</param>
        /// <param name="value">Node to insert</param>
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

        /// <summary>Wraps a value in a new node and inserts it as the new leftmost (head) node</summary>
        /// <param name="bin_node">Any node in the structure</param>
        /// <param name="value">Value to wrap and insert at the head</param>
        public static void AddToHead<T>(CookieBinNode<T> bin_node, T value)
        {
            if (bin_node == null) return;
            CookieBinNode<T> left = GetLeftest(bin_node);
            CookieBinNode<T> newNode = new(value);
            left.SetLeft(newNode);
            newNode.SetRight(left);
        } // AddToHead
        /// <summary>Inserts an existing node as the new leftmost (head) node</summary>
        /// <param name="bin_node">Any node in the structure</param>
        /// <param name="value">Node to insert at the head</param>
        public static void AddToHead<T>(CookieBinNode<T> bin_node, CookieBinNode<T> value)
        {
            if (bin_node == null) return;
            CookieBinNode<T> left = GetLeftest(bin_node);
            left.SetLeft(value);
            value.SetRight(left);
        } // AddToHead

        /// <summary>Wraps a value in a new node and inserts it as the new rightmost (tail) node</summary>
        /// <param name="bin_node">Any node in the structure</param>
        /// <param name="value">Value to wrap and insert at the tail</param>
        public static void AddToTail<T>(CookieBinNode<T> bin_node, T value)
        {
            if (bin_node == null) return;
            CookieBinNode<T> right = GetRightest(bin_node);
            CookieBinNode<T> newNode = new(value);
            right.SetRight(newNode);
            newNode.SetLeft(right);
        } // AddToTail
        /// <summary>Inserts an existing node as the new rightmost (tail) node</summary>
        /// <param name="bin_node">Any node in the structure</param>
        /// <param name="value">Node to insert at the tail</param>
        public static void AddToTail<T>(CookieBinNode<T> bin_node, CookieBinNode<T> value)
        {
            if (bin_node == null) return;
            CookieBinNode<T> right = GetRightest(bin_node);
            right.SetRight(value);
            value.SetLeft(right);
        } // AddToTail



        /// <summary>Walks left from the given node until it finds the leftmost node</summary>
        /// <param name="bin_node">Node to start walking from</param>
        /// <returns>The leftmost node, or default if bin_node is null</returns>
        public static CookieBinNode<T> GetLeftest<T>(CookieBinNode<T> bin_node)
        {
            if (bin_node == null)
                throw new CookieEmptyStructureException(className);
            if (bin_node.Left == null)
                return bin_node;
            return GetLeftest(bin_node.Left);
        } // GetLeftest


        /// <summary>Walks right from the given node until it finds the rightmost node</summary>
        /// <param name="bin_node">Node to start walking from</param>
        /// <returns>The rightmost node, or default if bin_node is null</returns>
        public static CookieBinNode<T> GetRightest<T>(CookieBinNode<T> bin_node)
        {
            if (bin_node == null)
                throw new CookieEmptyStructureException(className);
            if (bin_node.Right == null)
                return bin_node;
            return GetRightest(bin_node.Right);
        } // GetRightest


    } // CookieHoldingLineHelper



    /// <summary>
    /// This CookieDataStructure doesn't require any extra file!
    /// Binary Tree created and better-ed by Cookie :]
    /// </summary>
    /// <typeparam name="TreeType">Type of the binary tree</typeparam>6
    public class CookieTree<TreeType> : IEnumerable<TreeType>
    { 
        private CookieBinNode<TreeType>? root;

        private static string className = "CookieTree";

        /// <summary>
        /// Class constructor that builds a tree on top of an existing root node
        /// </summary>
        /// <param name="root">The node to use as root</param>
        public CookieTree(CookieBinNode<TreeType> root)
        { this.root = root; }

        /// <summary>
        /// Class constructor for an empty tree
        /// </summary>
        public CookieTree() : this(null)
        { }


        /// <summary>
        /// Returns if the tree is empty
        /// </summary>
        /// <returns>True if the tree has no root</returns>
        public bool IsEmpty()
        { return this.root == null; }


        // insert:

        /// <summary>
        /// Inserts a value into the tree, keeping binary-search-tree order
        /// </summary>
        /// <param name="value">Value to insert</param>
        public void Insert(TreeType value)
        { this.root = InsertRecursive(this.root, value); }
        /// <summary>
        /// Recursively finds the correct spot for a value and inserts it there
        /// </summary>
        /// <param name="root">Subtree root to insert into</param>
        /// <param name="value">Value to insert</param>
        /// <returns>The (possibly new) subtree root</returns>
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

        /// <summary>
        /// Removes a value from the tree, keeping binary-search-tree order
        /// </summary>
        /// <param name="value">Value to remove</param>
        public void Remove(TreeType value)
        { this.root = RemoveRecursive(this.root, value); }
        /// <summary>
        /// Recursively finds and removes a value, re-attaching the remaining subtrees
        /// </summary>
        /// <param name="root">Subtree root to remove from</param>
        /// <param name="value">Value to remove</param>
        /// <returns>The (possibly new) subtree root</returns>
        private CookieBinNode<TreeType> RemoveRecursive(CookieBinNode<TreeType> root, TreeType value)
        {
            if (root == null)
                throw new CookieEmptyStructureException(className);

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

        /// <summary>
        /// Searches the tree for a value
        /// </summary>
        /// <param name="value">Value to look for</param>
        /// <returns>True if the value is found</returns>
        public bool Search(TreeType value)
        { return SearchRecursive(this.root, value); }
        /// <summary>
        /// Recursively searches a subtree for a value
        /// </summary>
        /// <param name="root">Subtree root to search</param>
        /// <param name="value">Value to look for</param>
        /// <returns>True if the value is found</returns>
        private bool SearchRecursive(CookieBinNode<TreeType> root, TreeType value)
        {
            if (root == null)
                throw new CookieEmptyStructureException(className);

            if (root.Value.Equals(value))
                return true;

            bool found = SearchRecursive(root.Right, value);
            if (root > value)
                found = SearchRecursive(root.Left, value);
            return found;
        } // SearchRecursive


        /// <summary>
        /// Returns the total number of nodes in the tree
        /// </summary>
        /// <returns>Number of nodes</returns>
        public int Length()
        { return CountElements(this.root); }
        /// <summary>
        /// Recursively counts the nodes in a subtree
        /// </summary>
        /// <param name="root">Subtree root to count from</param>
        /// <returns>Number of nodes in the subtree</returns>
        private int CountElements(CookieBinNode<TreeType> root)
        {
            if (root == null)
                return 0;
            return 1 + CountElements(root.Left) + CountElements(root.Right);
        } // CountElements


        /// <summary>
        /// Counts how many times a value appears in the tree
        /// </summary>
        /// <param name="value">Value to count</param>
        /// <returns>Number of matching nodes</returns>
        public int Count(TreeType value)
        { return CountElement(this.root, value); }
        /// <summary>
        /// Recursively counts how many times a value appears in a subtree
        /// </summary>
        /// <param name="root">Subtree root to count from</param>
        /// <param name="value">Value to count</param>
        /// <returns>Number of matching nodes</returns>
        private int CountElement(CookieBinNode<TreeType> root, TreeType value)
        {
            if (root == null)
                return 0;
            int add = 0;
            if (root.Value.Equals(value))
                add = 1;
            return add + CountElement(root.Left, value) + CountElement(root.Right, value);
        } // CountElement


        /// <summary>
        /// Counts the number of leaf nodes (nodes with no children) in the tree
        /// </summary>
        /// <returns>Number of leaf nodes</returns>
        public int CountLeaves()
        { return CountLeaves(this.root); }
        /// <summary>
        /// Recursively counts leaf nodes in a subtree
        /// </summary>
        /// <param name="root">Subtree root to count from</param>
        /// <returns>Number of leaf nodes in the subtree</returns>
        private int CountLeaves(CookieBinNode<TreeType> root)
        {
            if (root == null)
                return 0;
            int add = 0;
            if (root.Left == root.Right && root.Left == null)
                add = 1;
            return add + CountLeaves(root.Left) + CountLeaves(root.Right);
        } // CountLeaves


        /// <summary>
        /// Returns the height of the tree
        /// </summary>
        /// <returns>The tree's height</returns>
        public int GetHight()
        { return GetHight(this.root); }
        /// <summary>
        /// Recursively computes the height of a subtree
        /// </summary>
        /// <param name="root">Subtree root to measure</param>
        /// <returns>Height of the subtree</returns>
        private int GetHight(CookieBinNode<TreeType> root) 
        {
            if (root == null) return 0;
            int count_left = 1 + GetHight(root.Left);
            int count_right = 1 + GetHight(root.Right);
            return Math.Max(count_left, count_right);
        } // GetHight


        // type of trees:

        /// <summary>
        /// Checks if every node has either 0 or 2 children (a "full" binary tree)
        /// </summary>
        /// <returns>True if the tree is full</returns>
        public bool IsFull()
        { return this.root != null && IsFull(this.root); }
        /// <summary>
        /// Recursively checks if every node in a subtree has either 0 or 2 children
        /// </summary>
        /// <param name="bin_tree">Subtree root to check</param>
        /// <returns>True if the subtree is full</returns>
        private bool IsFull(CookieBinNode<TreeType>? bin_tree) // 17 
        {
            if (bin_tree == null)
                return true;
            bool check = (bin_tree.Right == null || bin_tree.Left == null) && !(bin_tree.Right == null && bin_tree.Left == null);
            return !check && IsFull(bin_tree.Left) && IsFull(bin_tree.Right);
        } // IsFull


        /// <summary>
        /// Checks if the tree is complete (every level is filled left to right, with no gaps)
        /// </summary>
        /// <returns>True if the tree is complete</returns>
        public bool IsComplete()
        { return this.root != null && IsComplete(this.root); }
        /// <summary>
        /// Performs a breadth-first walk checking that no node has a right child without
        /// a left child, and that no children appear after an empty slot was seen
        /// </summary>
        /// <param name="bin_tree">Subtree root to check</param>
        /// <returns>True if the subtree is complete</returns>
        private bool IsComplete(CookieBinNode<TreeType>? bin_tree)
        {
            if (bin_tree == null)
                return false;

            CookieQueue<CookieBinNode<TreeType>> check_list = new(bin_tree);
            bool reached_empty = false;
            while (!check_list.IsEmpty())
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


        /// <summary>
        /// Checks if the tree satisfies binary-search-tree ordering at every node
        /// </summary>
        /// <returns>True if the tree is correctly ordered as a BST</returns>
        public bool IsSpanningTree()
        { return this.root != null && IsSpanningTree(this.root); }

        /// <summary>
        /// Recursively checks BST ordering: every left child must be smaller, every right child larger
        /// </summary>
        /// <param name="nodes">Subtree root to check</param>
        /// <returns>True if the subtree satisfies BST ordering</returns>
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
        /// <summary>
        /// Returns an enumerator that walks the tree in-order (enables foreach)
        /// </summary>
        /// <returns>An in-order enumerator over the tree's values</returns>
        public IEnumerator<TreeType> GetEnumerator()
        // FIX: was "InOrderYeald(root)" (typo, undefined method) - corrected to InOrderYield and
        // added ".GetEnumerator()" since InOrderYield returns IEnumerable<TreeType>, not IEnumerator<TreeType>
        { if (root != null) return InOrderYield(root).GetEnumerator(); return Enumerable.Empty<TreeType>().GetEnumerator(); }
        /// <summary>
        /// Recursively yields values in in-order (left, node, right)
        /// </summary>
        /// <param name="node">Subtree root to yield from</param>
        /// <returns>Values in in-order sequence</returns>
        private IEnumerable<TreeType> InOrderYield(CookieBinNode<TreeType>? node)
        {
            if (node == null)
                yield break;

            foreach (var v in InOrderYield(node.Left))
                yield return v;

            yield return node.Value;

            foreach (var v in InOrderYield(node.Right))
                yield return v;
        } // InOrderYield
        /// <summary>
        /// Non-generic IEnumerable.GetEnumerator implementation, forwards to the generic version
        /// </summary>
        /// <returns>An in-order enumerator over the tree's values</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();



        // prints:

        /// <summary>
        /// Returns the tree's values as a space-separated string in in-order (left, node, right)
        /// </summary>
        /// <returns>In-order traversal as a string</returns>
        public string InOrderTraversal()
        { return InOrderRecursive(this.root); }
        /// <summary>
        /// Recursively builds an in-order traversal string
        /// </summary>
        /// <param name="root">Subtree root to traverse</param>
        /// <returns>String fragment of values in in-order sequence</returns>
        private string InOrderRecursive(CookieBinNode<TreeType> root)
        {
            string return_value = "";
            if (root != null)
            {
                return_value += InOrderRecursive(root.Left);
                return_value += root.Value + " ";
                return_value += InOrderRecursive(root.Right);
            } // if
            return return_value;
        } // InOrderRecursive


        /// <summary>
        /// Returns the tree's values as a space-separated string in pre-order (node, left, right)
        /// </summary>
        /// <returns>Pre-order traversal as a string</returns>
        public string PreOrderTraversal()
        { return PreOrderRecursive(this.root); }
        /// <summary>
        /// Recursively builds a pre-order traversal string
        /// </summary>
        /// <param name="root">Subtree root to traverse</param>
        /// <returns>String fragment of values in pre-order sequence</returns>
        private string PreOrderRecursive(CookieBinNode<TreeType> root)
        {
            string return_value = "";
            if (root != null)
            {
                return_value += root.Value + " ";
                return_value += PreOrderRecursive(root.Left);
                return_value += PreOrderRecursive(root.Right);
            } // if 
            return return_value;
        } // PreOrderRecursive


        /// <summary>
        /// Returns the tree's values as a space-separated string in post-order (left, right, node)
        /// </summary>
        /// <returns>Post-order traversal as a string</returns>
        public string PostOrderTraversal()
        { return PostOrderRecursive(this.root); }
        /// <summary>
        /// Recursively builds a post-order traversal string
        /// </summary>
        /// <param name="root">Subtree root to traverse</param>
        /// <returns>String fragment of values in post-order sequence</returns>
        private string PostOrderRecursive(CookieBinNode<TreeType> root)
        {
            string return_value = "";
            if (root != null)
            {
                return_value += PostOrderRecursive(root.Left);
                return_value += PostOrderRecursive(root.Right);
                return_value += root.Value + " ";
            } // if
            return return_value;
        } // PostOrderRecursive

    } // class CookieTree


} // namespace first_smth

