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
CookieDataStructure: CookieStack contains
    IsEmpty - public function
    Push - public function
    PopValue - public function
    GetTop - public function
    Copy - public function
    Clear - public function
    Reverse - public function
    RotateLeft - public function
    RotateRight - public function
    GetEnumerator - public function (and IEnumerable.GetEnumerator implementation)
    ToString - public function (2 overloads: no-arg, with split string)
*/


namespace smth
{
    /// <summary>
    /// This CookieDataStructure requires CookieNode.cs file!
    /// Stack created and better-ed by Cookie :]
    /// </summary>
    /// <typeparam name="StackType">Type of the stack</typeparam>
    public class CookieStack<StackType> : IEnumerable<StackType>
    {
        private CookieNode<StackType>? head_node;

        private static string className = "CookieStack";

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieStack() { this.head_node = null; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieStack(StackType value) { this.head_node = new CookieNode<StackType>(value); }

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieStack(CookieNode<StackType>? nodes) { this.head_node = nodes; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieStack(StackType[] list) { this.head_node = CookieNodeWorker.ArrayToNodes(list); }


        /// <summary>
        /// Returns if the stack is empty
        /// </summary>
        /// <returns>true is the stack is empty</returns>
        public bool IsEmpty() { return this.head_node == null; }

        /// <summary>
        /// Returns stack's length 
        /// </summary>
        public int Length { get { return CookieNodeWorker.RecursionCount<StackType>(this.head_node); } }


        // Getters-Setters
        /// <summary>
        /// FILO (FIRST IN ; LAST OUT) - puts the value in front
        /// </summary>
        /// <param name="value">Value to save</param>
        public void Push(StackType value) 
        { this.head_node = new CookieNode<StackType>(value, this.head_node); }


        /// <summary>
        /// LIFO (LAST IN ; FIRST OUT) - gets the front value ; could be null
        /// </summary>
        /// <returns>The top (last added) value</returns>
        public StackType PopValue()
        {
            if (this.head_node == null)
                throw new CookieEmptyStructureException(className);

            CookieNode<StackType> node = this.head_node;
            this.head_node = node.GetNext();
            node.Next = null;
            return node.Value;
        } // Pop

        /// <summary>
        /// Easier way of Pop function :]
        /// </summary>
        public StackType Pop { get { return this.PopValue(); } }


        /// <summary>
        /// Gets the top value, if the stack is empty, returns default of the type
        /// </summary>
        /// <returns>value of the top</returns>
        public StackType GetTop()
        { return this.head_node != null ? this.head_node.Value : throw new CookieEmptyStructureException(className); } 


        /// <summary>
        /// Creates a copy of the object
        /// </summary>
        /// <returns>copy of the node list</returns>
        public CookieStack<StackType> Copy()
        {
            if (this.head_node == null)
                throw new CookieEmptyStructureException(className);

            CookieNode<StackType> return_value = new CookieNode<StackType>(this.head_node.Value);
            CookieNode<StackType> current = return_value;
            CookieNode<StackType>? nodes = this.head_node.Next;

            while (nodes != null)
            {
                current.Next = new(nodes.Value);
                current = current.Next;
                nodes = nodes.Next;
            } // while

            return new(return_value);
        } // Copy


        /// <summary>
        /// Clears the Stack
        /// </summary>
        public void Clear()
        { this.head_node = null; }


        /// <summary>
        /// Creates a copy of the object in type of the CookieStack
        /// </summary>
        /// <returns>copy of the node list</returns>
        public CookieStack<StackType> Reverse()
        {
            if (this.Length <= 1) return this.Copy();

            CookieStack<StackType> rev = new CookieStack<StackType>();
            CookieNode<StackType>? current = this.head_node;

            while (current != null)
            {
                rev.Push(current.Value);
                current = current.Next;
            } // while
            return rev;
        } // Reverse



        /// <summary>
        /// Moves front to back once
        /// </summary>
        public void RotateLeft()
        { if (!this.IsEmpty()) this.Push(this.Pop); }

        /// <summary>
        /// Moves back to front once
        /// </summary>
        public void RotateRight()
        {
            if (this.IsEmpty() || this.head_node.Next == null)  // there is one value in
                return;

            CookieNode<StackType> current = this.head_node;
            while (current.Next.Next != null)
                current = current.Next;
            CookieNode<StackType> last = current.Next;
            current.SetNext(null);

            last.SetNext(this.head_node);
            this.head_node = last;
        } // RotateRight



        // override

        // IEnumerable
        /// <summary>
        /// Returns an enumerator that walks the stack from top to bottom (enables foreach)
        /// </summary>
        /// <returns>An enumerator over the stack's values, top first</returns>
        public IEnumerator<StackType> GetEnumerator()
        { if (this.head_node != null) return this.head_node.GetEnumerator(); return Enumerable.Empty<StackType>().GetEnumerator(); }
        /// <summary>
        /// Non-generic IEnumerable.GetEnumerator implementation, forwards to the generic version
        /// </summary>
        /// <returns>An enumerator over the stack's values, top first</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        // object 

        /// <summary>
        /// override for ToString to - ['value', 'value', 'value', ...]
        /// </summary>
        /// <returns>string of the class</returns>
        public override string ToString()
        {
            return ToString(", ");
        } // override ToString

        /// <summary>
        /// override for ToString to - ['value'{split}'value'{split}'value'{split} ...]
        /// </summary>
        /// <returns>string of the class</returns>
        public string ToString(string split)
        {
            if (this.head_node == null)
                return "[]";

            string str = "[";
            CookieNode<StackType>? node = this.head_node;

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



} // namespace smth
