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
    /// Tuple created and better-ed by Cookie :]
    /// </summary>
    /// <typeparam name="RingType">CookieRingBuffer type</typeparam>
    public class CookieRingBuffer<RingType> : IEnumerable<RingType>
    {
        private CookieNode<RingType> head_node;   // oldest
        private CookieNode<RingType> write_node;  // next write place

        private int count;
        private int capacity;


        public CookieRingBuffer(int size)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException("Size cannot be 0 or negative");
            this.capacity = size;
            this.count = 0;
            this.head_node = new CookieNode<RingType>();
            CookieNode<RingType> current = this.head_node;

            for (int i = 1; i < size; i++)
            {
                current.SetNext(new CookieNode<RingType>());
                current = current.GetNext();
            } // for
            current.SetNext(this.head_node);
            this.write_node = this.head_node;
        } // __init__

        // Special case: count:
        public int Count { get { return count; } }
        public int Capacity { get { return capacity; } }



        // Getters:
        public RingType GetOldest()
        {
            if (this.IsEmpty()) throw new Exception("No values were saved");
            return this.head_node.Value;
        } // GetOldest

        public RingType GetNewest()
        {
            if (this.IsEmpty()) throw new Exception("No values were saved");
            CookieNode<RingType> current = this.head_node;
            for (int i = 1; i < this.count; i++)
                current = current.Next;
            return current.Value;
        } // GetNewest


        // Special case: empty:

        public bool IsEmpty()
        { return count == 0; } // IsEmpty


        // Special case: empty:

        public bool IsFull()
        { return count == capacity; } // IsFull



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

        public void SetAt(int index, RingType value)
        {
            if (index < 0 || index >= this.count) throw new IndexOutOfRangeException();
            CookieNode<RingType> current = this.head_node;
            for (; 0 < index; index--)
                current = current.Next;
            current.SetValue(value);
        } // AddFront


        // Removers:

        /// <summary>
        /// Removes and returns oldest value.
        /// </summary>
        /// <returns></returns>
        public RingType Pop()
        {
            if (this.IsEmpty()) throw new Exception("No values were saved");
            RingType value = this.head_node.Value;
            this.head_node.SetValue(default);
            this.head_node = this.head_node.Next;
            this.count--;
            return value;
        } // Add

        /// <summary>
        /// Removes and returns newest value.
        /// </summary>
        /// <returns></returns>
        public RingType PopLast()
        {
            if (this.IsEmpty()) throw new Exception("No values were saved");
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
        public IEnumerator<RingType> GetEnumerator() // foreach!
        {
            CookieNode<RingType>? current = this.head_node;
            for (int i = 0; i < this.count; i++)
            {
                yield return current.Value;
                current = current.Next;
            } // for
        } // GetEnumerator
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
