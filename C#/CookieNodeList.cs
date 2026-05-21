using NHunspell;
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


namespace smth
{
    /// <summary>
    /// This CookieDataStructure requires CookieNode.cs file!
    /// List created and better-ed by Cookie :]
    /// </summary>
    /// <typeparam name="ListType">Type of the node list</typeparam>
    public class CookieNodeList<ListType> : IEnumerable<ListType>, ICollection<ListType>
    {
        /// <summary>
        /// The list itself
        /// </summary>
        private CookieNode<ListType>? head_node;
        /// <summary>
        /// Is the list ReadOnly
        /// </summary>
        public bool IsReadOnly { get; }


        // Builders:
        /// <summary>
        /// Class setter
        /// </summary>
        public CookieNodeList() { this.head_node = null; }
        /// <summary>
        /// Class setter with valuables
        /// </summary>
        public CookieNodeList(ListType[] arr)
        {
            CookieNode<ListType> prev_node = null;

            foreach (ListType value in arr)
            {
                CookieNode<ListType> some_node = new(value);

                if (this.count == 0)
                    this.head_node = some_node;
                else
                    prev_node.SetNext(some_node);

                prev_node = some_node;
                this.count++;
            } // foreach
        } // __init__
        /// <summary>
        /// Class setter with valuables
        /// </summary>
        private CookieNodeList(CookieNodeList<ListType> nodes)
        {
            CookieNode<ListType> prev_node = null;

            for (int i = 0; i < nodes.Length; i++)
            {
                CookieNode<ListType> some_node = new(nodes.Get(i));

                if (this.count == 0)
                    this.head_node = some_node;
                else
                    prev_node.SetNext(some_node);

                prev_node = some_node;
                this.count++;
            } // foreach
        } // __init__



        // Length-connected
        /// <summary>
        /// Return current length of the list
        /// </summary>
        /// <returns>Int represented length</returns>
        public int Length { get { return this.RecursionCount(this.head_node); } }
        /// <summary>
        /// Return current length of the list
        /// </summary>
        /// <returns>Int represented length</returns>
        public int Count { get { return this.RecursionCount(this.head_node); } }


        /// <summary>
        /// Counts number of nodes in the node list! Recursively
        /// </summary>
        /// <param name="node">head node</param>
        /// <returns>Number of nodes in the given list</returns>
        private static int RecursionCount(CookieNode<ListType> node)
        {
            if (node == null) return 0;
            return 1 + RecursionCount(node.GetNext());
        } // RecursionCount


        // Counters:
        /// <summary>
        /// Counts number of duplicates of item given
        /// </summary>
        /// <param name="item">An item to check for</param>
        /// <returns>Number of items found</returns>
        public int CountElementDuplicates(ListType item)
        {
            if (this.head_node == null || !this.Contains(item)) return 0;
            return CountElementDuplicatesLoop(item, this.head_node);
        } // CountElementDuplicates

        /// <summary>
        /// Checks if the list has an item specified
        /// </summary>
        /// <param name="item">An item to check for</param>
        /// <returns>Number of items found</returns>
        private int CountElementDuplicatesLoop(ListType item, CookieNode<ListType> node)
        {
            int dupe = 0;
            if (node == null) return dupe;
            if (item.Equals(node.Value)) dupe++;
            return dupe + CountElementDuplicatesLoop(item, node.GetNext());
        } // CountElementDuplicatesLoop



        // Getters
        /// <summary>
        /// Get element at index
        /// </summary>
        /// <param name="index">Index to get value from</param>
        /// <returns>Element at specified index</returns>
        public ListType this[int index] // INDEXES!
        {
            get => this.Get(index);
            set => this.AddAt(value, index);
        } // ListType

        /// <summary>
        /// Finds index of the item inputted. If the item is not found, returns -1
        /// </summary>
        /// <param name="item">an item that needs to be found</param>
        /// <returns>Int represented index</returns>
        public int GetIndex(ListType item)
        {
            this.CheckCount();
            if (this.head_node == null) return -1;

            CookieNode<ListType> some_node = this.head_node;
            int counter = 0;

            while (some_node != null)
            {
                if (some_node.Value.Equals(item))
                    return counter;
                counter++;
                some_node = some_node.GetNext();
            } // while
            return -1;
        } // GetIndex
        /// <summary>
        /// Finds index of the item inputted. If the item is not found, returns -1
        /// </summary>
        /// <param name="item">an item that needs to be found</param>
        /// <returns>Int represented index</returns>
        public int Find(ListType item)
        { return this.GetIndex(item); }

        /// <summary>
        /// Finds item by the inputted index
        /// </summary>
        /// <param name="index">An index type Int</param>
        /// <returns>Found element</returns>
        /// <exception cref="IndexOutOfRangeException">If the index is out-of-range</exception>
        public ListType Get(int index)
        {
            if (index < 0 || index >= this.RecursionCount(this.head_node)) throw new IndexOutOfRangeException(nameof(index));

            CookieNode<ListType>? some_node = this.head_node;
            for (; 0 < index; index--)
                some_node = some_node.GetNext();
            return some_node.Value;
        } // Get

        /// <summary>
        /// Returns first value of the list
        /// </summary>
        /// <returns>Found element, null if no elements are in the list</returns>
        public ListType GetFirst()
        {
            if (this.head_node == null)
                return (ListType)(object)null;
            return this.Get(0);
        } // GetFirst


        // Special case : contains value
        /// <summary>
        /// Checks if the list has an item specified
        /// </summary>
        /// <param name="item">An item to check for</param>
        /// <returns>Boolean if the the item found</returns>
        public bool Contains(ListType item)
        { if (this.head_node == null) return false; return this.GetIndex(item) != -1; }

        // Special case : copy value
        /// <summary>
        /// The function return a copy of this object
        /// </summary>
        /// <returns></returns>
        public CookieNodeList<ListType> Copy()
        { return new CookieNodeList<ListType>(this); }


        // Special case : change type
        /// <summary>
        /// Changes the type of the list to the requested - be sure it's convertable!
        /// </summary>
        /// <typeparam name="RequestedType">Type to change to</typeparam>
        /// <returns>If possible, the list, if not null list</returns>
        public CookieNodeList<RequestedType>? ChangeType<RequestedType>()
        {
            if (this.head_node == null) return null;
            CookieNodeList<RequestedType> new_list = new CookieNodeList<RequestedType>();

            CookieNode<ListType>? nodes = this.head_node;
            while (nodes != null)
            {
                try
                {
                    RequestedType value = (RequestedType)Convert.ChangeType(nodes.Value, typeof(RequestedType));
                    new_list.Append(value);
                } // try
                catch
                { return null; } // conversion failed - break

                nodes = nodes.Next;
            } // while

            return new_list;
        } // ChangeType

        /// <summary>
        /// Changes the type of the list to the requested
        /// </summary>
        /// <typeparam name="RequestedType">Type to change to</typeparam>
        /// <returns>A list of possible values</returns>
        public CookieNodeList<RequestedType>? PartlyChangeType<RequestedType>()
        {
            if (this.head_node == null) return null;
            CookieNodeList<RequestedType> new_list = new CookieNodeList<RequestedType>();

            CookieNode<ListType>? nodes = this.head_node;
            while (nodes != null)
            {
                try
                {
                    RequestedType value = (RequestedType)Convert.ChangeType(nodes.Value, typeof(RequestedType));
                    new_list.Append(value);
                } // try
                catch
                { } // conversion failed - do nothing

                nodes = nodes.Next;
            } // while

            return new_list;
        } // PartlyChangeType




        // Setters
        /// <summary>
        /// Adds an item to the end of the list
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Append(ListType item)
        {
            if (this.head_node == null)
            {
                this.head_node = new(item);
                return;
            } // if

            CookieNode<ListType> some_node = this.head_node;
            while (some_node.GetNext() != null)
                some_node = some_node.GetNext();
            some_node.SetNext(new CookieNode<ListType>(item));
        } // Append

        /// <summary>
        /// Adds another CookieNodeList to the end of this list
        /// </summary>
        /// <param name="nodes">List to add</param>
        public void Append(CookieNodeList<ListType> nodes)
        {
            if (nodes == null || nodes.head_node == null) return;
            
            CookieNode<ListType> tail = this.head_node;
            CookieNode<ListType> other_nodes = nodes.head_node;
            if (tail == null)
            {
                tail = new CookieNode<ListType>(other_nodes.GetValue());
                this.head_node = tail;
                other_nodes = other_nodes.GetNext();
            } // if 
            else
                while (tail.GetNext() != null)
                    tail = tail.GetNext();

            // add the nodes
            other_nodes = nodes.head_node;
            while (other_nodes != null)
            {
                tail.SetNext(new CookieNode<ListType>(other_nodes.GetValue()));
                tail = tail.GetNext();
                other_nodes = other_nodes.GetNext();
            } // while
        } // Append


        /// <summary>
        /// Adds an item to the beginning of the list
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Add(ListType item)
        {
            if (this.head_node == null)
            {
                this.head_node = new(item);
                return;
            } // if

            CookieNode<ListType> some_node = new(item);
            some_node.SetNext(this.head_node);
            this.head_node = some_node;
        } // Add

        /// <summary>
        /// Adds another CookieNodeList to the beginning of this list
        /// </summary>
        /// <param name="nodes">List to add</param>
        public void Add(CookieNodeList<ListType> nodes)
        {
            if (nodes.head_node == null) return;

            CookieNode<ListType> last = nodes.head_node;
            while (last.GetNext() != null)
                last = last.GetNext();

            last.SetNext(this.head_node);
            this.head_node = nodes.head_node;
        } // Add

        /// <summary>
        /// Adds an item to index give in the list
        /// </summary>
        /// <param name="item">Item to add</param>
        public void AddAt(ListType item, int index)
        {
            if (index < 0 || index >= this.RecursionCount(this.head_node)) throw new IndexOutOfRangeException(nameof(index));

            CookieNode<ListType> newNode = new(item);
            if (this.head_node == null || index == 0)
            {
                newNode.SetNext(this.head_node);
                this.head_node = newNode;
                return;
            } // if

            CookieNode<ListType> current = this.head_node;
            int currentIndex = 0;

            while (current != null && currentIndex < index - 1)
            {
                current = current.GetNext();
                currentIndex++;
            } // while

            if (current == null)  throw new IndexOutOfRangeException(nameof(index));

            newNode.SetNext(current.GetNext());
            current.SetNext(newNode);
        } // AddAt



        // Removes:
        /// <summary>
        /// Removes the first instance of the item specified
        /// </summary>
        /// <param name="item">An item</param>
        /// <returns>True if the remove was successful; otherwise false</returns>
        public bool Remove(ListType item)
        {
            if (this.head_node == null)
                return false;

            // item is head
            if (this.head_node.Value.Equals(item))
            {
                this.head_node = this.head_node.GetNext();
                return false;
            } // if

            CookieNode<ListType> some_node = this.head_node;
            while (some_node.GetNext() != null)
            {
                if (some_node.GetNext().Value.Equals(item))
                {
                    some_node.SetNext(some_node.GetNext().GetNext());
                    return true;
                } // if
                some_node = some_node.GetNext();
            } // while
            return false;
        } // Remove

        /// <summary>
        /// Removes an item at the index specified
        /// </summary>
        /// <param name="index">An index</param>
        /// <exception cref="IndexOutOfRangeException">If the index is out-of-range</exception>
        public void RemoveAt(int index)
        {
            if (this.head_node == null) return;
            if (index < 0 || index >= this.RecursionCount(this.head_node)) throw new IndexOutOfRangeException(nameof(index));

            // item is head
            if (index == 0)
            {
                this.head_node = this.head_node.GetNext();
                return;
            } // if

            CookieNode<ListType> some_node = this.head_node;
            for (; index > 1; index--)
                some_node = some_node.GetNext();
            some_node.SetNext(some_node.GetNext().GetNext());
        } // RemoveAt

        /// <summary>
        /// Removes the last element in the list
        /// </summary>
        public void RemoveLast()
        { if (this.head_node != null) this.RemoveAt(RecursionCount(this.head_node) - 1); }
        /// <summary>
        /// Removes the first element in the list
        /// </summary>
        public void RemoveFirst()
        { if (this.head_node != null) this.RemoveAt(0); }

        /// <summary>
        /// Removes all instance of element given, leaving one the first one
        /// </summary>
        /// <param name="item">Element to delete</param>
        public void RemoveDuplicates(ListType item)
        {
            int first = this.GetIndex(item);
            int counter = this.CountElementDuplicates(item);
            if (first == -1 || counter <= 1)
                return;

            for (int i = first + 1; i < this.Length && counter > 1; i++)
                if (item.Equals(this.Get(i)))
                {
                    this.RemoveAt(i);
                    counter--;
                    i--;  // the count jumps one cause the next elem (new next) is skipped :[
                } // if
        } // RemoveDuplicates

        /// <summary>
        /// Removes all instance of element given
        /// </summary>
        /// <param name="item">Element to delete</param>
        public void RemoveAll(ListType item)
        {
            if (!this.Contains(item)) return;
            this.RemoveDuplicates(item);
            this.Remove(item);
        } // RemoveAll


        /// <summary>
        /// Wipes the list clean
        /// </summary>
        public void Clear()
        { this.head_node = null; } 



        // override

        // Object

        /// <summary>
        /// override for ToString to - ['value', 'value', 'value', ...]
        /// </summary>
        /// <returns>string of the class</returns>
        public override string ToString()
        { return ToString(", "); }

        /// <summary>
        /// override for ToString to - ['value'{split} 'value'{split} 'value'{split} ...]
        /// </summary>
        /// <returns>string of the class</returns>
        public string ToString(string split)
        {
            if (this.nodes == null)
                return "[]";

            string str = "[";
            CookieNode<T>? node = this.nodes;

            while (node != null)
            {
                str += node.ToString();
                if (node.GetNext() != null)
                    str += split;
                node = node.GetNext();
            } // while

            return str + "]";
        } // override ToString


        // IEnumerable
        public IEnumerator<ListType> GetEnumerator() // foreach!
        { return head_node.GetEnumerator(); } 
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        // ICollection

        /// <summary>
        /// Copies elements ot array given
        /// </summary>
        /// <param name="array">Array to copy to</param>
        /// <param name="arrayIndex">Copy from</param>
        public void CopyTo(ListType[] array, int arrayIndex)
        {
            foreach (var item in this)
                array[arrayIndex++] = item;
        } // CopyTo




        // Statics:
        /// <summary>
        /// The function reverses the list given
        /// </summary>
        /// <param name="list">List to reverse</param>
        public static void Reverse(CookieNodeList<object> list)
        {
            if (list == null || list.Length <= 1) return;
            CookieNodeList<object> copy_list = list.Copy();
            list.Clear();
            for (int i = 0; i < copy_list.Length; i++)
                list.Add(copy_list.Get(i));
        } // Reverse


    } // class CookieNodeList


} // namespace smth

