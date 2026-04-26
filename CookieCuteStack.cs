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
    /// <summary>
    /// Stack created and better-ed by Cookie :]
    /// </summary>
    /// <typeparam name="T">Type of the stack</typeparam>
    public class CookieStack<T>
    {
        private CookieNode<T>? nodes;

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieStack() { this.nodes = null; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieStack(T value) { this.nodes = new CookieNode<T>(value); }

        /// <summary>
        /// Class constructor  -- not bagrut supported!
        /// </summary>
        public CookieStack(CookieNodeList<T> list) { this.nodes = CookieNodeWorker.NodeListToNodes(list); }

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieStack(CookieNode<T> nodes) { this.nodes = nodes; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieStack(T[] list) { this.nodes = CookieNodeWorker.ArrayToNodes(list); }


        /// <summary>
        /// Returns if the stack is empty
        /// </summary>
        /// <returns>true is the stack is empty</returns>
        public bool IsEmpty() { return this.nodes == null; }

        /// <summary>
        /// Returns stack's length  -- not bagrut supported!
        /// </summary>
        public int Length { get { return CookieNodeWorker.RecursionCount<T>(this.nodes); } }


        // Getters-Setters
        /// <summary>
        /// LIFO (LAST IN ; FIRST OUT) - puts the value in front
        /// </summary>
        /// <param name="value">Value to save</param>
        public void Push(T value) 
        { this.nodes = new CookieNode<T>(value, this.nodes); }


        /// <summary>
        /// FILO (FIRST IN ; LAST OUT) - gets the front value ; could be null
        /// </summary>
        /// <returns>The top (last added) value</returns>
        public T? PopValue()
        {
            if (this.nodes == null)
                return default;  // cause.... Just cause >:]

            CookieNode<T> node = this.nodes;
            this.nodes = node.GetNext();
            node.Next = null;
            return node.Value;
        } // Pop

        /// <summary>
        /// Easier way of Pop function :]  -- not bagrut supported!
        /// </summary>
        public T? Pop { get { return this.PopValue(); } }


        /// <summary>
        /// Gets the top value, if the stack is empty, returns default of the type
        /// </summary>
        /// <returns>value of the top</returns>
        public T? GetTop()
        { return this.nodes != null ? this.nodes.Value : default; } // if (this.nodes != null) return this.nodes.Value; return default; 


        /// <summary>
        /// Creates a copy of the object in type of the Nodes  -- not bagrut supported!
        /// </summary>
        /// <returns>copy of the node list</returns>
        public CookieNode<T>? Copy()
        {
            if (this.nodes == null)
                return null;

            CookieNode<T> return_value = new CookieNode<T>(this.nodes.Value);
            CookieNode<T> current = return_value;
            CookieNode<T>? nodes = this.nodes.Next;

            while (nodes != null)
            {
                current.Next = new(nodes.Value);
                current = current.Next;
                nodes = nodes.Next;
            } // while

            return return_value;
        } // Copy


        /// <summary>
        /// Clears the Stack
        /// </summary>
        public void Clear()
        { this.nodes = null; }



        // override
        /// <summary>
        /// override for ToString to - ['value', 'value', 'value', ...]
        /// </summary>
        /// <returns>string of the class</returns>
        public override string ToString()
        {
            if (this.nodes == null)
                return "[]";

            string str = "[";
            CookieNode<T>? node = this.nodes;

            while (node != null)
            {
                str += node.ToString();
                if (node.GetNext() != null)
                    str += ", ";
                node = node.GetNext();
            } // while

            return str + "]";
        } // override ToString

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

    } // class CookieStack


    /// <summary>
    /// Queue created and better-ed by Cookie :]
    /// </summary>
    /// <typeparam name="T">Type of the queue</typeparam>
    public class CookieQueue<T>
    {
        private CookieNode<T>? nodes;
        private CookieNode<T>? pointer_to_end;


        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieQueue() { this.nodes = null; this.pointer_to_end = null; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieQueue(T value) 
        { this.nodes = new CookieNode<T>(value); this.pointer_to_end = this.nodes; }


        /// <summary>
        /// Class constructor  -- not bagrut supported!
        /// </summary>
        public CookieQueue(CookieNodeList<T> list) 
        { 
            this.nodes = CookieNodeWorker.NodeListToNodes(list);
            this.pointer_to_end = this.nodes;
            while (this.pointer_to_end.Next != null)
                this.pointer_to_end = this.pointer_to_end.Next; // got to the last
        } // __init__


        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieQueue(CookieNode<T> nodes) 
        { 
            this.nodes = nodes;
            this.pointer_to_end = this.nodes;
            while (this.pointer_to_end.Next != null)
                this.pointer_to_end = this.pointer_to_end.Next; // got to the last
        } // __init__

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieQueue(T[] list) 
        { 
            this.nodes = CookieNodeWorker.ArrayToNodes(list);
            this.pointer_to_end = this.nodes;
            while (this.pointer_to_end.Next != null)
                this.pointer_to_end = this.pointer_to_end.Next; // got to the last
        } // __init__


        /// <summary>
        /// Returns if the queue is empty
        /// </summary>
        /// <returns>true is the queue is empty</returns>
        public bool IsEmpty() 
        { return this.nodes == null && this.pointer_to_end == null; }

        /// <summary>
        /// Returns queue's length  -- not bagrut supported!
        /// </summary>
        public int Length { get { return CookieNodeWorker.RecursionCount<T>(this.nodes); } }


        // Getters-Setters
        /// <summary>
        /// FIFO (FIRST IN ; FIRST OUT) - puts the value at the end
        /// </summary>
        /// <param name="value">Value to save</param>
        public void Insert(T value)
        { 
            if (this.nodes == null) // queue is empty
            {
                this.nodes = new CookieNode<T>(value);
                this.pointer_to_end = this.nodes;
                return;
            } // if 
            this.pointer_to_end.SetNext(new CookieNode<T>(value));
            this.pointer_to_end = this.pointer_to_end.Next;
        } // Insert


        /// <summary>
        /// FIFO (FIRST IN ; FIRST OUT) - gets the front value ; could be null
        /// </summary>
        /// <returns>The top (first added) value</returns>
        public T? RemoveValue()
        {
            if (this.nodes == null)
                return default;  // cause.... Just cause >:]

            CookieNode<T> node = this.nodes;
            if (node.GetNext() == null)
                this.pointer_to_end = null;
            this.nodes = node.GetNext();
            node.Next = null;
            return node.Value;
        } // Remove

        /// <summary>
        /// Easier way of Remove function :]  -- not bagrut supported!
        /// </summary>
        public T? Remove { get { return this.RemoveValue(); } }


        /// <summary>
        /// Gets the top value, if the stack is empty, returns default of the type
        /// </summary>
        /// <returns>value of the top</returns>
        public T? GetTop()
        { return this.nodes != null ? this.nodes.Value : default; } // if (this.nodes != null) return this.nodes.Value; return default; 


        /// <summary>
        /// Creates a copy of the object in type of the Nodes  -- not bagrut supported!
        /// </summary>
        /// <returns>copy of the node list</returns>
        public CookieNode<T>? Copy()
        {
            if (this.nodes == null)
                return null;

            CookieNode<T> return_value = new CookieNode<T>(this.nodes.Value);
            CookieNode<T> current = return_value;
            CookieNode<T>? nodes = this.nodes.Next;

            while (nodes != null)
            {
                current.Next = new(nodes.Value);
                current = current.Next;
                nodes = nodes.Next;
            } // while

            return return_value;
        } // Copy


        /// <summary>
        /// Clears the Queue
        /// </summary>
        public void Clear()
        { this.nodes = null; this.pointer_to_end = null; }



        // override
        /// <summary>
        /// override for ToString to - ['value', 'value', 'value', ...]
        /// </summary>
        /// <returns>string of the class</returns>
        public override string ToString()
        {
            if (this.nodes == null)
                return "[]";

            string str = "[";
            CookieNode<T>? node = this.nodes;

            while (node != null)
            {
                str += node.ToString();
                if (node.GetNext() != null)
                    str += ", ";
                node = node.GetNext();
            } // while

            return str + "]";
        } // override ToString

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

    } // class CookieQueue


} // namespace smth
