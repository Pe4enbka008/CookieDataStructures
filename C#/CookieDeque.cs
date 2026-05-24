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
    /// Queue created and better-ed by Cookie :]
    /// </summary>
    /// <typeparam name="DequeType">Type of the queue</typeparam>6
    public class CookieDeque<DequeType> : IEnumerable<DequeType>
    {
        private CookieNode<DequeType>? head_node;
        private CookieNode<DequeType>? last_node;


        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieDeque() { this.head_node = null; this.last_node = null; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieDeque(DequeType value) 
        { this.head_node = new CookieNode<DequeType>(value); this.last_node = this.head_node; }


        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieDeque(CookieNode<DequeType> nodes) 
        { 
            this.head_node = nodes;
            this.last_node = this.head_node;
            while (this.last_node.Next != null)
                this.last_node = this.last_node.Next; // got to the last
        } // __init__

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieDeque(DequeType[] list) 
        { 
            this.head_node = CookieNodeWorker.ArrayToNodes(list);
            this.last_node = this.head_node;
            while (this.last_node.Next != null)
                this.last_node = this.last_node.Next; // got to the last
        } // __init__


        /// <summary>
        /// Returns if the queue is empty
        /// </summary>
        /// <returns>true is the queue is empty</returns>
        public bool IsEmpty() 
        { return this.head_node == null && this.last_node == null; }

        /// <summary>
        /// Returns queue's length 
        /// </summary>
        public int Length { get { return CookieNodeWorker.RecursionCount<DequeType>(this.head_node); } }



        // Getters-Setters
        /// <summary>
        /// Puts the value at the start
        /// </summary>
        /// <param name="value">Value to save</param>
        public void PushFront(DequeType value)
        {
            if (this.head_node == null) // queue is empty
            {
                this.head_node = new(value);
                this.last_node = this.head_node;
                return;
            } // if 
            CookieNode<DequeType> node = new(value, this.head_node);
            this.head_node = node;
        } // PushFront

        /// <summary>
        /// Gets the front value ; could be null
        /// </summary>
        /// <returns>The top (first added) value</returns>
        public DequeType? PopFront()
        {
            if (this.head_node == null)
                throw new Exception("No values to pop");

            CookieNode<DequeType> node = this.head_node;
            if (node.Next == null)
                this.last_node = null;
            this.head_node = node.GetNext();
            node.Next = null;
            return node.Value;
        } // PopFront


        /// <summary>
        /// Puts the value at the end
        /// </summary>
        /// <param name="value">Value to save</param>
        public void PushBack(DequeType value)
        {
            if (this.head_node == null) // queue is empty
            {
                this.head_node = new(value);
                this.last_node = this.head_node;
                return;
            } // if 
            this.last_node.SetNext(new(value));
            this.last_node = this.last_node.Next;
        } // PushBack

        /// <summary>
        /// Gets the last value ; could be null
        /// </summary>
        /// <returns>The top (first added) value</returns>
        public DequeType? PopBack()
        {
            if (this.head_node == null)
                throw new Exception("No values to pop");

            CookieNode<DequeType> node = this.last_node;
            this.last_node = this.head_node;
            while (this.last_node.Next != node)
                this.last_node = this.last_node.Next;
            this.last_node.Next = null;
            return node.Value;
        } // PopBack



        /// <summary>
        /// Gets the top value, if the stack is empty, returns default of the type
        /// </summary>
        /// <returns>value of the top</returns>
        public DequeType? GetTop()
        { return this.head_node != null ? this.head_node.Value : throw new Exception("No values to pop"); } 


        /// <summary>
        /// Creates a copy of the object
        /// </summary>
        /// <returns>copy of the node list</returns>
        public CookieDeque<DequeType>? Copy()
        {
            if (this.head_node == null)
                return null;

            CookieNode<DequeType> return_value = new CookieNode<DequeType>(this.head_node.Value);
            CookieNode<DequeType> current = return_value;
            CookieNode<DequeType>? nodes = this.head_node.Next;

            while (nodes != null)
            {
                current.Next = new(nodes.Value);
                current = current.Next;
                nodes = nodes.Next;
            } // while

            return new(return_value);
        } // Copy


        /// <summary>
        /// Clears the Queue
        /// </summary>
        public void Clear()
        { this.head_node = null; this.last_node = null; }



        /// <summary>
        /// Creates a copy of the object in type of the CookieQueue 
        /// </summary>
        /// <returns>copy of the node list</returns>
        public CookieDeque<DequeType>? Reverse()
        {
            if (this.head_node == null) return null;
            if (this.Length <= 1) return this.Copy();

            CookieDeque<DequeType> rev = new();
            CookieNode<DequeType>? current = this.head_node;

            while (current != null)
            {
                rev.PushFront(current.Value);
                current = current.Next;
            } // while
            return rev;
        } // Reverse



        /// <summary>
        /// Moves front to back once
        /// </summary>
        public void RotateLeft()
        { if (!this.IsEmpty()) this.PushBack(this.PopFront()); }
 
        /// <summary>
        /// Moves back to front once
        /// </summary>
        public void RotateRight()
        { if (!this.IsEmpty()) this.PushFront(this.PopBack()); } 



        // override

        // IEnumerable
        public IEnumerator<DequeType> GetEnumerator()
        { return this.head_node.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // object 

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
            if (this.head_node == null)
                return "[]";

            string str = "[";
            CookieNode<DequeType>? node = this.head_node;

            while (node != null)
            {
                str += node.ToString();
                if (node.GetNext() != null)
                    str += split;
                node = node.GetNext();
            } // while

            return str + "]";
        } // override ToString

    } // class CookieDeque


} // namespace smth
