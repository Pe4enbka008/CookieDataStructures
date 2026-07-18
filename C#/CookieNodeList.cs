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
CookieDataStructure: CookieNodeList contains
    IsEmpty - public function
    RecursionCount - private static function
    CountElementDuplicates - public function
    CountElementDuplicatesLoop - private function
    GetIndex - public function
    Find - public function
    Get - public function
    GetFirst - public function
    Contains - public function
    Copy - public function
    CopyTo - public function
    ChangeType - public function
    PartlyChangeType - public function
    Append - public function (2 overloads: ListType, CookieNodeList<ListType>)
    Add - public function (2 overloads: ListType, CookieNodeList<ListType>)
    AddAt - public function
    Remove - public function
    RemoveAt - public function
    RemoveLast - public function
    RemoveFirst - public function
    RemoveDuplicates - public function
    RemoveAll - public function
    Clear - public function
    GetEnumerator - public function (and IEnumerable.GetEnumerator implementation)
    ToString - public function (2 overloads: no-arg, with split string)
    Reverse - public static function
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
        private CookieNode<ListType>? head_node;
        private bool readOnly;

        private static string className = "CookieNodeList";

        /// <summary>
        /// Is the list ReadOnly
        /// </summary>
        public bool IsReadOnly { get { return readOnly; } }


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
            this.head_node = null;
            CookieNode<ListType> prev_node = null;

            foreach (ListType value in arr)
            {
                CookieNode<ListType> some_node = new(value);

                if (this.head_node == null)
                    this.head_node = some_node;
                else
                    prev_node.SetNext(some_node);
                prev_node = some_node;
            } // foreach
        } // __init__
        /// <summary>
        /// Class setter with valuables
        /// </summary>
        private CookieNodeList(CookieNodeList<ListType> nodes)
        {
            this.head_node = null;
            CookieNode<ListType> prev_node = null;

            foreach (ListType value in nodes)
            {
                CookieNode<ListType> some_node = new(value);

                if (this.head_node == null)
                    this.head_node = some_node;
                else
                    prev_node.SetNext(some_node);
                prev_node = some_node;
            } // foreach
        } // __init__



        // Length-connected
        /// <summary>
        /// Return current length of the list
        /// </summary>
        /// <returns>Int represented length</returns>
        public int Count { get { return RecursionCount(this.head_node); } }

        /// <summary>
        /// Return if the list is empty
        /// </summary>
        /// <returns>Int represented length</returns>
        public bool IsEmpty()
        { return this.head_node == null; }


        /// <summary>
        /// Switches ReadOnly to opposite
        /// </summary>
        public void SwitchReadOnly()
        { this.readOnly = !this.readOnly; }


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
            if (node.Equals(item)) dupe++;
            return dupe + CountElementDuplicatesLoop(item, node.GetNext());
        } // CountElementDuplicatesLoop



        // Getters:
        
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
        /// Finds index of the item inputted. If the item is not found, throws Exception
        /// </summary>
        /// <param name="item">an item that needs to be found</param>
        /// <returns>Int represented index</returns>
        /// <exception cref="CookieEmptyStructureException">If no values are in the structure</exception>
        public int GetIndex(ListType item)
        {
            if (this.head_node == null) throw new CookieEmptyStructureException(className);

            CookieNode<ListType> some_node = this.head_node;
            int counter = 0;

            while (some_node != null)
            {
                if (some_node.Equals(item))
                    return counter;
                counter++;
                some_node = some_node.GetNext();
            } // while
            return -1;
        } // GetIndex

        /// <summary>
        /// Finds index of the item inputted. If the item is not found, throws Exception
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
        /// <exception cref="CookieEmptyStructureException">If no values are in the structure</exception>
        /// <exception cref="CookieIndexOutOfRangeException">If the index is out-of-range</exception>
        public ListType Get(int index)
        {
            if (this.head_node == null)
                throw new CookieEmptyStructureException(className);
            else if (index < 0 || index >= RecursionCount(this.head_node)) 
                throw new CookieIndexOutOfRangeException(index);

            CookieNode<ListType>? some_node = this.head_node;
            for (; 0 < index; index--)
                some_node = some_node.GetNext();
            return some_node.Value;
        } // Get

        /// <summary>
        /// Returns first value of the list
        /// </summary>
        /// <returns>Found element</returns>
        /// <exception cref="CookieEmptyStructureException">If no values are in the structure</exception>
        public ListType GetFirst()
        {
            if (this.head_node == null)
                throw new CookieEmptyStructureException(className);
            return this.Get(0);
        } // GetFirst


        // Special case : contains value

        /// <summary>
        /// Checks if the list has an item specified
        /// </summary>
        /// <param name="item">An item to check for</param>
        /// <returns>Boolean if the the item found</returns>
        /// <exception cref="CookieIndexOutOfRangeException">If the index is out-of-range</exception>
        public bool Contains(ListType item)
        { 
            if (this.head_node == null) 
                return false;
            try
            {
                this.GetIndex(item);
                return true;
            } // try
            catch (CookieIndexOutOfRangeException)
            { return false; }
        } // Contains

        // Special case : copy value

        /// <summary>
        /// The function return a copy of this object
        /// </summary>
        /// <returns></returns>
        public CookieNodeList<ListType> Copy()
        { return new CookieNodeList<ListType>(this); }

        // ICollection

        /// <summary>
        /// Copies elements ot array given (for ICollection)
        /// </summary>
        /// <param name="array">Array to copy to</param>
        /// <param name="arrayIndex">Copy from</param>
        public void CopyTo(ListType[] array, int arrayIndex)
        {
            foreach (var item in this)
                array[arrayIndex++] = item;
        } // CopyTo


        // Special case : reverse

        /// <summary>
        /// The function reverses the list
        /// </summary>
        /// <exception cref="CookieEmptyStructureException">If no values are in the structure</exception>
        public void Reverse()
        {
            if (this.head_node == null)
                throw new CookieEmptyStructureException(className + " cannot be converted/edited due to access level being ReadOnly.");

            if (this.Count <= 1) return;
            CookieNodeList<ListType> copy_list = this.Copy();
            this.Clear();
            for (int i = 0; i < copy_list.Count; i++)
                this.Add(copy_list.Get(i));
        } // Reverse


        // Special case : change type

        /// <summary>
        /// Changes the type of the list to the requested - be sure it's convertible!
        /// </summary>
        /// <typeparam name="RequestedType">Type to change to</typeparam>
        /// <returns>The converted list</returns>
        /// <exception cref="CookieEmptyStructureException">If no values are in the structure</exception>
        /// <exception cref="CookieStructureArgumentException">If conversion failed</exception>
        public CookieNodeList<RequestedType> ChangeType<RequestedType>()
        {
            if (this.head_node == null) throw new CookieEmptyStructureException(className);
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
                { throw new CookieStructureArgumentException($"Failed to convert to type {nameof(RequestedType)}"); } // conversion failed - return

                nodes = nodes.Next;
            } // while

            return new_list;
        } // ChangeType

        /// <summary>
        /// Changes the type of the list to the requested
        /// </summary>
        /// <typeparam name="RequestedType">Type to change to</typeparam>
        /// <returns>A list of possible values</returns>
        /// <exception cref="CookieEmptyStructureException">If no values are in the structure</exception>
        public CookieNodeList<RequestedType> PartlyChangeType<RequestedType>()
        {
            if (this.head_node == null) 
                throw new CookieEmptyStructureException(className + " cannot be converted/edited due to access level being ReadOnly.");
            
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
                throw new CookieEmptyStructureException(className + " cannot be converted/edited due to access level being ReadOnly.");

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
            if (this.head_node == null)
                throw new CookieEmptyStructureException(className + " cannot be converted/edited due to access level being ReadOnly.");

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
                throw new CookieEmptyStructureException(className + " cannot be converted/edited due to access level being ReadOnly.");

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
            if (this.head_node == null)
                throw new CookieEmptyStructureException(className + " cannot be converted/edited due to access level being ReadOnly.");

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
        /// <exception cref="CookieIndexOutOfRangeException">If the index is out-of-range</exception>
        /// <exception cref="CookieEmptyStructureException">If no values are in the structure</exception>
        public void AddAt(ListType item, int index)
        {
            if (this.head_node == null)
                throw new CookieEmptyStructureException(className + " cannot be converted/edited due to access level being ReadOnly.");

            if (index < 0 || index >= RecursionCount(this.head_node)) throw new CookieIndexOutOfRangeException(index);
            else if (this.head_node == null) throw new CookieEmptyStructureException(className);

            if (index == 0)
            {
                this.head_node.SetValue(item);
                return;
            } // if

            CookieNode<ListType> current = this.head_node;
            int currentIndex = 0;

            while (current != null && currentIndex < index)
            {
                current = current.GetNext();
                currentIndex++;
            } // while

            if (current == null) throw new CookieIndexOutOfRangeException(index);
            current.SetValue(item);
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
                throw new CookieEmptyStructureException(className + " cannot be converted/edited due to access level being ReadOnly.");

            if (this.head_node == null)
                return false;

            // item is head
            if (this.head_node.Equals(item))
            {
                this.head_node = this.head_node.GetNext();
                return true;
            } // if

            CookieNode<ListType> some_node = this.head_node;
            while (some_node.GetNext() != null)
            {
                if (some_node.GetNext().Equals(item))
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
        /// <exception cref="CookieIndexOutOfRangeException">If the index is out-of-range</exception>
        /// <exception cref="CookieEmptyStructureException">If no values are in the structure</exception>
        public void RemoveAt(int index)
        {
            if (this.head_node == null)
                throw new CookieEmptyStructureException(className + " cannot be converted/edited due to access level being ReadOnly.");

            if (this.head_node == null) throw new CookieEmptyStructureException(className);
            if (index < 0 || index >= RecursionCount(this.head_node)) throw new CookieIndexOutOfRangeException(index);

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
            if (this.head_node == null)
                throw new CookieEmptyStructureException(className + " cannot be converted/edited due to access level being ReadOnly.");

            int first = this.GetIndex(item);
            int counter = this.CountElementDuplicates(item);
            if (first == -1 || counter <= 1)
                return;

            for (int i = first + 1; i < this.Count && counter > 1; i++)
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
            if (this.head_node == null)
                throw new CookieEmptyStructureException(className + " cannot be converted/edited due to access level being ReadOnly.");

            if (!this.Contains(item)) return;
            this.RemoveDuplicates(item);
            this.Remove(item);
        } // RemoveAll


        /// <summary>
        /// Wipes the list clean
        /// </summary>
        public void Clear()
        {
            if (this.head_node == null)
                throw new CookieEmptyStructureException(className + " cannot be converted/edited due to access level being ReadOnly.");
            this.head_node = null; 
        } // Clear



        // override

        // IEnumerable
        /// <summary>
        /// Returns an enumerator that walks the list from first to last element (enables foreach)
        /// </summary>
        /// <returns>An enumerator over the list's values, in order</returns>
        public IEnumerator<ListType> GetEnumerator()
        { if (head_node != null) return head_node.GetEnumerator(); return Enumerable.Empty<ListType>().GetEnumerator(); }
        /// <summary>
        /// Non-generic IEnumerable.GetEnumerator implementation, forwards to the generic version
        /// </summary>
        /// <returns>An enumerator over the list's values, in order</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        // object

        /// <summary>
        /// override for ToString to - ['value', 'value', 'value', ...]
        /// </summary>
        /// <returns>string of the class</returns>
        public override string ToString()
        { return ToString(", "); }

        /// <summary>
        /// override for ToString to - ['value'{split}'value'{split}'value'{split} ...]
        /// </summary>
        /// <returns>string of the class</returns>
        public string ToString(string split)
        {
            if (this.head_node == null)
                return "[]";

            string str = "[";
            CookieNode<ListType>? node = this.head_node;

            while (node != null)
            {
                str += node.ToString();
                if (node.GetNext() != null)
                    str += split;
                node = node.GetNext();
            } // while

            return str + "]";
        } // override ToString


    } // class CookieNodeList


} // namespace smth

