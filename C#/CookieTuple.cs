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
CookieDataStructure: CookieTuple contains
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
    ChangeType - public function
    PartlyChangeType - public function
    GetEnumerator - public function (and IEnumerable.GetEnumerator implementation)
    ToString - public function (2 overloads: no-arg, with split string)
*/


namespace smth
{
    /// <summary>
    /// This CookieDataStructure requires CookieNode.cs file!
    /// Tuple created and better-ed by Cookie :]
    /// </summary>
    /// <typeparam name="TupleType">Type of the node tuple</typeparam>
    public class CookieTuple<TupleType> : IEnumerable<TupleType>
    {
        private CookieNode<TupleType> head_node;

        private static string className = "CookieTuple";


        // Builders:
        /// <summary>
        /// Class setter with valuables
        /// </summary>
        public CookieTuple(TupleType[] arr)
        {
            CookieNode<TupleType> prev_node = null;

            foreach (TupleType value in arr)
            {
                CookieNode<TupleType> some_node = new(value);

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
        private CookieTuple(CookieTuple<TupleType> nodes)
        {
            CookieNode<TupleType> prev_node = null;

            foreach (TupleType value in nodes)
            {
                CookieNode<TupleType> some_node = new(value);

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
        public int Length { get { return RecursionCount(this.head_node); } }

        /// <summary>
        /// If the tuple is empty
        /// </summary>
        /// <returns>Int represented length</returns>
        public bool IsEmpty()
        { return this.head_node == null; }


        /// <summary>
        /// Counts number of nodes in the node list! Recursively
        /// </summary>
        /// <param name="node">head node</param>
        /// <returns>Number of nodes in the given list</returns>
        private static int RecursionCount(CookieNode<TupleType> node)
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
        public int CountElementDuplicates(TupleType item)
        {
            if (this.head_node == null || !this.Contains(item)) return 0;
            return CountElementDuplicatesLoop(item, this.head_node);
        } // CountElementDuplicates

        /// <summary>
        /// Checks if the list has an item specified
        /// </summary>
        /// <param name="item">An item to check for</param>
        /// <returns>Number of items found</returns>
        private int CountElementDuplicatesLoop(TupleType item, CookieNode<TupleType> node)
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
        public TupleType this[int index] // INDEXES!
        {
            get => this.Get(index);
        } // ListType

        /// <summary>
        /// Finds index of the item inputted. If the item is not found, throws Exception
        /// </summary>
        /// <param name="item">an item that needs to be found</param>
        /// <returns>Int represented index</returns>
        public int GetIndex(TupleType item)
        {
            if (this.head_node == null) 
                throw new CookieValueNotFoundException(item);

            CookieNode<TupleType> some_node = this.head_node;
            int counter = 0;

            while (some_node != null)
            {
                if (some_node.Value.Equals(item))
                    return counter;
                counter++;
                some_node = some_node.GetNext();
            } // while
            throw new CookieValueNotFoundException(item);
        } // GetIndex
        /// <summary>
        /// Finds index of the item inputted. If the item is not found, throws Exception
        /// </summary>
        /// <param name="item">an item that needs to be found</param>
        /// <returns>Int represented index</returns>
        public int Find(TupleType item)
        { return this.GetIndex(item); }

        /// <summary>
        /// Finds item by the inputted index
        /// </summary>
        /// <param name="index">An index type Int</param>
        /// <returns>Found element</returns>
        /// <exception cref="IndexOutOfRangeException">If the index is out-of-range</exception>
        public TupleType Get(int index)
        {
            if (index < 0 || index >= RecursionCount(this.head_node)) 
                throw new CookieIndexOutOfRangeException(index);

            CookieNode<TupleType>? some_node = this.head_node;
            for (; 0 < index; index--)
                some_node = some_node.GetNext();
            return some_node.Value;
        } // Get

        /// <summary>
        /// Returns first value of the list
        /// </summary>
        /// <returns>Found element, null if no elements are in the list</returns>
        public TupleType GetFirst()
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
        public bool Contains(TupleType item)
        {
            try 
            {
                this.GetIndex(item);
                return true;
            } // try
            catch (CookieEmptyStructureException)
            { return false; }
        } // Contains

        // Special case : copy value
        /// <summary>
        /// The function return a copy of this object
        /// </summary>
        /// <returns></returns>
        public CookieTuple<TupleType> Copy()
        { return new CookieTuple<TupleType>(this); }


        // Special case : change type
        /// <summary>
        /// Changes the type of the list to the requested - be sure it's convertible!
        /// </summary>
        /// <typeparam name="RequestedType">Type to change to</typeparam>
        /// <returns>If possible, the list, if not null list</returns>
        /// <exception cref="CookieEmptyStructureException">If no values are in the structure</exception>
        /// <exception cref="CookieStructureArgumentException">If conversion failed</exception>
        public CookieTuple<RequestedType> ChangeType<RequestedType>()
        {
            if (this.head_node == null) throw new CookieEmptyStructureException(className);
            RequestedType[] save_list = new RequestedType[this.Length];
            int count = 0;

            CookieNode<TupleType>? nodes = this.head_node;
            while (nodes != null)
            {
                try
                {
                    RequestedType value = (RequestedType)Convert.ChangeType(nodes.Value, typeof(RequestedType));
                    save_list[count++] = value;
                } // try
                catch
                { throw new CookieStructureArgumentException($"Failed to convert to type {nameof(RequestedType)}"); } // conversion failed - return

                nodes = nodes.Next;
            } // while

            RequestedType[] new_list = new RequestedType[count];
            count -= 1;
            for (; count >= 0; count--)
                new_list[count] = save_list[count];
            return new(new_list);
        } // ChangeType

        /// <summary>
        /// Changes the type of the list to the requested
        /// </summary>
        /// <typeparam name="RequestedType">Type to change to</typeparam>
        /// <returns>A list of possible values</returns>
        /// <exception cref="CookieEmptyStructureException">If no values are in the structure</exception>
        public CookieTuple<RequestedType> PartlyChangeType<RequestedType>()
        {
            if (this.head_node == null) throw new CookieEmptyStructureException(className);
            RequestedType[] save_list = new RequestedType[this.Length];
            int count = 0;

            CookieNode<TupleType>? nodes = this.head_node;
            while (nodes != null)
            {
                try
                {
                    RequestedType value = (RequestedType)Convert.ChangeType(nodes.Value, typeof(RequestedType));
                    save_list[count++] = value;
                } // try
                catch
                { } // conversion failed - do nothing

                nodes = nodes.Next;
            } // while

            RequestedType[] new_list = new RequestedType[count];
            count -= 1;
            for (; count >= 0; count--)
                new_list[count] = save_list[count];
            return new(new_list);
        } // PartlyChangeType


        // override

         // IEnumerable
        /// <summary>
        /// Returns an enumerator that walks the tuple from first to last element (enables foreach)
        /// </summary>
        /// <returns>An enumerator over the tuple's values, in order</returns>
        public IEnumerator<TupleType> GetEnumerator() 
        { return head_node.GetEnumerator(); }
        /// <summary>
        /// Non-generic IEnumerable.GetEnumerator implementation, forwards to the generic version
        /// </summary>
        /// <returns>An enumerator over the tuple's values, in order</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        

        // object

        /// <summary>
        /// override for ToString to - ('value', 'value', 'value', ...)
        /// </summary>
        /// <returns>string of the class</returns>
        public override string ToString()
        { return ToString(", "); }

        /// <summary>
        /// override for ToString to - ('value'{split}'value'{split}'value'{split} ...)
        /// </summary>
        /// <returns>string of the class</returns>
        public string ToString(string split)
        {
            if (this.head_node == null)
                return "()";

            string str = "(";
            CookieNode<TupleType>? node = this.head_node;

            while (node != null)
            {
                str += node.ToString();
                if (node.GetNext() != null)
                    str += split;
                node = node.GetNext();
            } // while

            return str + ")";
        } // override ToString
        


    } // CookieTuple
}
