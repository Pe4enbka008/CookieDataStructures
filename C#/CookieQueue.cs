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
    /// This CookieDataStructure requires CookieNode.cs and CookieStack.cs files!
    /// Queue created and better-ed by Cookie :]
    /// </summary>
    /// <typeparam name="QueueType">Type of the queue</typeparam>
    public class CookieQueue<QueueType> : IEnumerable<QueueType>
    {
        private CookieNode<QueueType>? head_node;
        private CookieNode<QueueType>? last_node;


        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieQueue() { this.head_node = null; this.last_node = null; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieQueue(QueueType value) 
        { this.head_node = new CookieNode<QueueType>(value); this.last_node = this.head_node; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieQueue(CookieNode<QueueType> nodes) 
        { 
            this.head_node = nodes;
            this.last_node = this.head_node;
            while (this.last_node.Next != null)
                this.last_node = this.last_node.Next; // got to the last
        } // __init__

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieQueue(QueueType[] list) 
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
        public int Length { get { return CookieNodeWorker.RecursionCount<QueueType>(this.head_node); } }


        // Getters-Setters
        /// <summary>
        /// FIFO (FIRST IN ; FIRST OUT) - puts the value at the end
        /// </summary>
        /// <param name="value">Value to save</param>
        public void Insert(QueueType value)
        { 
            if (this.head_node == null) // queue is empty
            {
                this.head_node = new CookieNode<QueueType>(value);
                this.last_node = this.head_node;
                return;
            } // if 
            this.last_node.SetNext(new CookieNode<QueueType>(value));
            this.last_node = this.last_node.Next;
        } // Insert


        /// <summary>
        /// FIFO (FIRST IN ; FIRST OUT) - gets the front value ; could be null
        /// </summary>
        /// <returns>The top (first added) value</returns>
        public QueueType? RemoveValue()
        {
            if (this.head_node == null)
                throw new Exception("No values to remove");

            CookieNode<QueueType> node = this.head_node;
            if (node.GetNext() == null)
                this.last_node = null;
            this.head_node = node.GetNext();
            node.Next = null;
            return node.Value;
        } // Remove

        /// <summary>
        /// FIFO (FIRST IN ; FIRST OUT) - gets the front value ; could be null
        /// </summary>
        public QueueType? Remove { get { return this.RemoveValue(); } }


        /// <summary>
        /// Gets the top value, if the stack is empty, returns default of the type
        /// </summary>
        /// <returns>value of the top</returns>
        public QueueType? GetTop()
        { return this.head_node != null ? this.head_node.Value : throw new Exception("No values to pop"); } 


        /// <summary>
        /// Creates a copy of the object
        /// </summary>
        /// <returns>copy of the node list</returns>
        public CookieQueue<QueueType>? Copy()
        {
            if (this.head_node == null)
                return null;

            CookieNode<QueueType> return_value = new CookieNode<QueueType>(this.head_node.Value);
            CookieNode<QueueType> current = return_value;
            CookieNode<QueueType>? nodes = this.head_node.Next;

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
        public CookieQueue<QueueType>? Reverse()
        {
            if (this.head_node == null) return null;
            if (this.Length <= 1) return this.Copy();

            CookieStack<QueueType> rev_stack = new CookieStack<QueueType>();
            CookieNode<QueueType>? current = this.head_node;

            while (current != null)
            {
                rev_stack.Push(current.Value);
                current = current.Next;
            } // while

            CookieQueue<QueueType> rev = new CookieQueue<QueueType>();
            while (!rev_stack.IsEmpty())
                rev.Insert(rev_stack.Pop);
            return rev;
        } // Reverse



        /// <summary>
        /// Moves front to back once
        /// </summary>
        public void RotateLeft()
        { if (!this.IsEmpty()) this.Insert(this.Remove); }

        /// <summary>
        /// Moves back to front once
        /// </summary>
        public void RotateRight()
        {
            if (this.IsEmpty() || this.head_node == this.last_node)  // there is one value in
                return;

            CookieNode<QueueType> current = this.head_node;
            while (current.Next != this.last_node)
                current = current.Next;
            current.SetNext(null);

            this.last_node.SetNext(this.head_node);
            this.head_node = this.last_node;
            this.last_node = current;
        } // RotateRight



        // override

        // IEnumerable
        public IEnumerator<QueueType> GetEnumerator()
        { if (head_node != null) return head_node.GetEnumerator(); return Enumerable.Empty<QueueType>().GetEnumerator(); }
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
            CookieNode<QueueType>? node = this.head_node;

            while (node != null)
            {
                str += node.ToString();
                if (node.GetNext() != null)
                    str += split;
                node = node.GetNext();
            } // while

            return str + "]";
        } // override ToString

    } // class CookieQueue


} // namespace smth
