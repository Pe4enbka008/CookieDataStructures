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
CookieDataStructure: CookieRingBuffer contains
    GetOldest - public function
    GetNewest - public function
    IsEmpty - public function
    IsFull - public function
    Add - public function
    AddFront - public function
    SetAt - public function
    Pop - public function
    PopLast - public function
    Clear - public function
    Contains - public function
    RotateLeft - public function
    RotateRight - public function
    GetEnumerator - public function (and IEnumerable.GetEnumerator implementation)
    ToString - public function (2 overloads: no-arg, with splitter string)
*/


namespace smth
{
    /// <summary>
    /// This CookieDataStructure requires CookieNode.cs file!
    /// Ring Buffer created by Cookie 
    /// </summary>
    /// <typeparam name="RingType">Type of the ring buffer</typeparam>
    public class CookieRingBuffer<RingType> : IEnumerable<RingType>
    {
        private CookieNode<RingType> head_node;   // oldest
        private CookieNode<RingType> write_node;  // next write place

        private int count;
        private int capacity;

        private static string className = "CookieRingBuffer";


        /// <summary>
        /// Class constructor - builds a fixed-size circular ring of empty nodes
        /// </summary>
        /// <param name="size">Capacity of the ring buffer</param>
        /// <exception cref="CookieStructureArgumentException">If size is 0 or negative</exception>
        public CookieRingBuffer(int size)
        {
            if (size <= 0) throw new CookieStructureArgumentException("Size cannot be 0 or negative");
            this.capacity = size;
            this.count = 0;
            this.head_node = new CookieNode<RingType>(null);
            CookieNode<RingType> current = this.head_node;

            for (int i = 1; i < size; i++)
            {
                current.SetNext(new CookieNode<RingType>(null));
                current = current.GetNext();
            } // for
            current.SetNext(this.head_node);
            this.write_node = this.head_node;
        } // __init__

        // Special case: count:
        /// <summary>Gets the current number of stored values</summary>
        public int Count { get { return count; } }
        /// <summary>Gets the maximum number of values the buffer can hold</summary>
        public int Capacity { get { return capacity; } }



        // Getters:
        /// <summary>
        /// Returns the oldest stored value without removing it
        /// </summary>
        /// <returns>The oldest value</returns>
        /// <exception cref="CookieEmptyStructureException">If no values are in the list</exception>
        public RingType GetOldest()
        {
            if (this.IsEmpty()) throw new CookieEmptyStructureException(className);
            return this.head_node.Value;
        } // GetOldest

        /// <summary>
        /// Returns the newest stored value without removing it
        /// </summary>
        /// <returns>The newest value</returns>
        /// <exception cref="CookieEmptyStructureException">If no values are in the list</exception>
        public RingType GetNewest()
        {
            if (this.IsEmpty()) throw new CookieEmptyStructureException(className);
            CookieNode<RingType> current = this.head_node;
            for (int i = 1; i < this.count; i++)
                current = current.Next;
            return current.Value;
        } // GetNewest


        // Special case: empty:

        /// <summary>Returns if the ring buffer has no stored values</summary>
        /// <returns>True if empty</returns>
        public bool IsEmpty()
        { return count == 0; } 


        // Special case: empty:

        /// <summary>Returns if the ring buffer is at capacity</summary>
        /// <returns>True if full</returns>
        public bool IsFull()
        { return count == capacity; } 



        // Setters:

        /// <summary>
        /// Adds value to the end.
        /// If full, overwrites oldest value.
        /// </summary>
        /// <param name="value"></param>
        public void Add(RingType value)
        {
            this.write_node.SetValue(value);
            if (this.IsFull())
                this.head_node = this.head_node.Next;
            else
                this.count++;
            this.write_node = this.write_node.Next;
        } // Add

        /// <summary>
        /// Adds value to the beginning.
        /// If full, overwrites newest value.
        /// </summary>
        /// <param name="value"></param>
        public void AddFront(RingType value)
        {
            CookieNode<RingType> current = this.head_node;
            if (this.IsFull())
            {
                for (int i = 1; i < this.count; i++)
                    current = current.Next;
                current.SetValue(value);
                return;
            } // if

            while (current.Next != this.head_node)
                current = current.Next;
            current.SetValue(value);
            this.head_node = current;
            this.count++;
        } // AddFront

        /// <summary>
        /// Overwrites the value stored at the given logical index (0 = oldest)
        /// </summary>
        /// <param name="index">Logical index to write to</param>
        /// <param name="value">New value to store</param>
        /// <exception cref="CookieIndexOutOfRangeException">If the index is out of range</exception>
        public void SetAt(int index, RingType value)
        {
            if (index < 0 || index >= this.count) throw new CookieIndexOutOfRangeException(index);
            CookieNode<RingType> current = this.head_node;
            for (; 0 < index; index--)
                current = current.Next;
            current.SetValue(value);
        } // AddFront


        // Removers:

        /// <summary>
        /// Removes and returns oldest value.
        /// </summary>
        /// <returns>oldest value</returns>
        /// <exception cref="CookieEmptyStructureException">If no values are in the list</exception>
        public RingType Pop()
        {
            if (this.IsEmpty()) throw new CookieEmptyStructureException(className);
            RingType value = this.head_node.Value;
            this.head_node.SetValue(default);
            this.head_node = this.head_node.Next;
            this.count--;
            return value;
        } // Add

        /// <summary>
        /// Removes and returns newest value.
        /// </summary>
        /// <returns>newest value</returns>
        /// <exception cref="CookieEmptyStructureException">If no values are in the list</exception>
        public RingType PopLast()
        {
            if (this.IsEmpty()) throw new CookieEmptyStructureException(className);
            CookieNode<RingType> current = this.head_node;
            for (int i = 1; i < this.count; i++)
                current = current.Next;
            RingType value = current.Value;
            current.SetValue(default); 
            this.write_node = current;
            this.count--;
            return value;
        } // AddFront


        /// <summary>
        /// Clears the ring
        /// </summary>
        /// <returns></returns>
        public void Clear()
        {
            if (this.IsEmpty()) return;
            CookieNode<RingType> current = this.head_node;
            for (int i = 0; i < this.capacity; i++)
            {
                current.SetValue(default); 
                current = current.Next;
            } // for
            this.count = 0;
            this.write_node = this.head_node;
        } // Clear



        // Special case: contains:

        /// <summary>
        /// Checks if a value currently exists in the ring buffer
        /// </summary>
        /// <param name="value">Value to check for</param>
        /// <returns>True if found</returns>
        public bool Contains(RingType value)
        {
            CookieNode<RingType> current = this.head_node;
            for (int i = 0; i < this.count; i++)
            {
                if (Equals(current.Value, value)) return true;
                current = current.Next;
            } // for
            return false;
        } // Contains



        // Rotater:

        /// <summary>
        /// Moves front to back once
        /// </summary>
        public void RotateLeft()
        {
            if (this.IsEmpty()) return;
            this.head_node = this.head_node.Next;
        } // RotateLeft

        /// <summary>
        /// Moves back to front once
        /// </summary>
        public void RotateRight()
        {
            if (this.IsEmpty()) return;
            CookieNode<RingType> current = this.head_node;
            for (int i = 1; i < this.count; i++)
                current = current.Next;
            this.head_node = current;
        } // RotateRight



        // override:

        // IEnumerable
        /// <summary>
        /// Returns an enumerator that walks the buffer from oldest to newest (enables foreach)
        /// </summary>
        /// <returns>An enumerator over the buffer's stored values, oldest first</returns>
        public IEnumerator<RingType> GetEnumerator() // foreach!
        {
            if (this.IsEmpty()) yield break;
            CookieNode<RingType>? current = this.head_node;
            for (int i = 0; i < this.count; i++)
            {
                yield return current.Value;
                current = current.Next;
            } // for
        } // GetEnumerator
        /// <summary>
        /// Non-generic IEnumerable.GetEnumerator implementation, forwards to the generic version
        /// </summary>
        /// <returns>An enumerator over the buffer's stored values, oldest first</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        // object 

        /// <summary>
        /// Returns string of the value saved here
        /// </summary>
        /// <returns>Value as a string</returns>
        public override string ToString()
        { return ToString(", "); } // override ToString

        /// <summary>
        /// Returns string of the value saved here
        /// </summary>
        /// <returns>Value as a string</returns>
        public string ToString(string splitter)
        {
            if (this.IsEmpty()) return "()";

            CookieNode<RingType> current = this.head_node;
            string return_value = "(";
            for (int i = 0; i < this.count; i++)
            {
                return_value += current.ToString();
                if (i < this.count - 1)
                    return_value += splitter;

                current = current.Next;
            } // for
            return return_value + ")";
        } // ToString


    } // CookieRingBuffer
}
