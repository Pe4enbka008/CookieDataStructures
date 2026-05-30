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
    /// Node for many things!
    /// </summary>
    /// <typeparam name="T">Type of the node</typeparam>
    public class CookieNode<T> : IEnumerable<T>
    {
        /// <summary>
        /// Node value
        /// </summary>
        private T value;
        private CookieNode<T>? next;


        /// <summary>
        /// Class setter
        /// </summary>
        public CookieNode() { this.value = default; this.next = null; }
        /// <summary>
        /// Class setter with valuable
        /// </summary>
        public CookieNode(T value) { this.value = value; this.next = null; }
        /// <summary>
        /// Class setter with valuable
        /// </summary>
        public CookieNode(CookieNode<T> node) { this.value = node.GetValue(); this.next = null; }
        /// <summary>
        /// Class setter with valuable
        /// </summary>
        public CookieNode(T value, CookieNode<T>? next) { this.value = value; this.next = next; }


        // Value:
        /// <summary>
        /// Return value of the node 
        /// </summary>
        /// <returns>The value</returns>
        public T Value { get { return GetValue(); } }

        /// <summary>
        /// The function gets the node value
        /// </summary>
        /// <returns>The value</returns>
        public T GetValue()
        { return this.value; }


        /// <summary>
        /// The function sets value 
        /// </summary>
        /// <param name="new_value">New value to set</param>
        public void SetValue(T new_value)
        { this.value = new_value; }



        // Next:
        /// <summary>
        /// Returns next node, also can set the next node 
        /// </summary>
        /// <returns>The value</returns>
        public CookieNode<T>? Next { get { return GetNext(); } set { SetNext(value); } }

        /// <summary>
        /// The function returns next node
        /// </summary>
        /// <returns>Next node saved</returns>
        public CookieNode<T>? GetNext()
        { return this.next; }


        /// <summary>
        /// The function saves next node
        /// </summary>
        /// <param name="new_value">The next node</param>
        public void SetNext(CookieNode<T>? new_value)
        { this.next = new_value; }

        /// <summary>
        /// The function saves next node
        /// </summary>
        /// <param name="new_value">The next node</param>
        public void SetNext(T new_value)
        { this.next = new(new_value); }
       


        // override:
        // IEnumerable
        public IEnumerator<T> GetEnumerator() // foreach!
        {
            CookieNode<T>? current = this;
            while (current != null)
            {
                yield return current.Value; // returns one value at a time
                current = current.Next;
            } // while
        } // GetEnumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        // object:
        /// <summary>
        /// Returns string of the value saved here
        /// </summary>
        /// <returns>Value as a string</returns>
        public override string ToString()
        { return $"{this.value}"; }

        public override bool Equals(object? obj)
        {
            if (obj is CookieNode<T>) return this.Value.Equals(((CookieNode<T>)obj).Value);
            if (obj is T) return this.Value.Equals((T)obj);
            return false;
        } // override Equals


    } // class CookieNode


    /// <summary>
    /// Helping class that manipulates with nodes
    /// The class requires NHunspell; If you don't want to download it, delete 'creators'
    /// </summary>
    public class CookieNodeWorker
    {
        // Fillers:
        /// <summary>
        /// Creates node list based on array 
        /// </summary>
        /// <typeparam name="T">Any type for the nodes list</typeparam>
        /// <param name="list">Array to fill with</param>
        /// <returns>Node list</returns>
        public static CookieNode<T> ArrayToNodes<T>(T[]? list)
        {
            if (list == null || list.Length == 0)
                return null;
            CookieNode<T> nodes = null, current = null;
            foreach (T elem in list)
            {
                if (nodes == null)
                {
                    nodes = new(elem);
                    current = nodes;
                } // if 
                else
                {
                    current.SetNext(new CookieNode<T>(elem));
                    current = current.GetNext();
                } // else
            } // foreach
            return nodes;
        } // ArrayToNodes


        // Creators:
        /// <summary>
        /// creates a Node list made of random strings using lib Hunspell
        /// </summary>
        /// <param name="length">Length of the list</param>
        /// <returns>the made list</returns>
        public static CookieNode<string> CreateStringList(int length)
        {
            // Downlaod .dic and .aff; in console "dotnet add package NHunspell"
            CookieNode<string> list = new CookieNode<string>("Cookie");
            using (Hunspell hunspell = new Hunspell("en_US.aff", "en_US.dic"))
            {
                Random rnd = new Random();
                while (length > 0)
                {
                    int word_length = rnd.Next(3, 9);
                    char[] chars = new char[word_length];
                    for (int i = 0; i < word_length; i++)  // random char-list from a-z
                        chars[i] = (char)('a' + rnd.Next(26));

                    string word = new string(chars);
                    List<string> suggestions = hunspell.Suggest(word); // suggestions for the fake word

                    if (suggestions.Count > 0)  // choose one suggestion
                    {
                        string suggestion = suggestions[rnd.Next(suggestions.Count)];
                        if (!ContainsElement<string>(list, suggestion) && hunspell.Spell(suggestion) && suggestion.Length > 3 && Char.IsAsciiLetterLower(suggestion[0])) // no repeats
                        {
                            Add<string>(list, suggestion);
                            length--;
                        } // if
                    } // if
                } // while
            } // using
            return list.Next;
        } // CreateList - string

        /// <summary>
        /// creates a Node list made of random strings using lib Hunspell
        /// </summary>
        /// <param name="length">Length of the list</param>
        /// <param name="least_letter_count">the least number of letters in the string, set to 4</param>
        /// <param name="most_letter_count">the most number of letters in the string, set to 100</param>
        /// <returns>the made list</returns>
        public static CookieNode<string> CreateList(int length, int least_letter_count = 4, int most_letter_count = 100)
        {
            if (least_letter_count > 8)
                least_letter_count = 0;
            if (most_letter_count < 4)
                most_letter_count = 10000;
            CookieNode<string> list = CreateStringList(length);
            while (length > 0)
            {
                string word = CreateStringList(1).GetValue();
                while (word.Length < least_letter_count || word.Length > most_letter_count)
                    word = CreateStringList(1).GetValue();
                Add<string>(list, word);
                length--;
            } // while
            return list;
        } // CreateList - string

        /// <summary>
        /// creates a Node list made of random int/double/float numbers using lib Random
        /// </summary>
        /// <typeparam name="T">int, double or float</typeparam>
        /// <param name="length">Length of the list</param>
        /// <param name="repeat">if reapeating is allowed</param>
        /// <returns>the made list</returns>
        public static CookieNode<T> CreateList<T>(int length, bool repeat = true)
        {
            if ((typeof(T) != typeof(int) && typeof(T) != typeof(float) && typeof(T) != typeof(double)) || length == 0)
                return null;

            CookieNode<T> list = new CookieNode<T>((T)(object)-1);
            Random rnd = new Random();
            while (length > 0)
            {
                T number = (T)(object)rnd.Next(-length * 3, length * 3);
                if (ContainsElement<T>(list, number) && !repeat)
                    continue;
                Add<T>(list, number);
                length--;
            } // while
            return list.Next;
        } // CreateList - int; double; float

        /// <summary>
        /// creates a Node list made of random chars 
        /// </summary>
        /// <param name="length">Length of the list</param>
        /// <param name="repeat">if reapeating is allowed</param>
        /// <returns>the made list</returns>
        public static CookieNode<char> CreateList(int length, bool repeat = true)
        {
            if (length > 26 * 2)
                repeat = true;

            CookieNode<char> list = new CookieNode<char>('C');
            Random rnd = new Random();
            while (length > 0)
            {
                char letter = (char)('a' + rnd.Next(26));
                if (Char.IsLetter(letter) && rnd.Next(6) + 1 / 2 == 0)
                    letter = Char.ToUpper(letter);

                if (ContainsElement<char>(list, letter) && !repeat)
                    continue;
                Add<char>(list, letter);
                length--;
            } // while
            return list.Next;
        } // CreateList - char

        /// <summary>
        /// creates a Node list made of random bools using lib Random
        /// </summary>
        /// <param name="length">Length of the list</param>
        /// <returns>the made list</returns>
        public static CookieNode<bool> CreateList(int length)
        {
            CookieNode<bool> list = new CookieNode<bool>(true);
            Random rnd = new Random();
            while (length > 0)
            {
                Add<bool>(list, rnd.Next(26) % 2 == 0);
                length--;
            } // while
            return list.Next;
        } // CreateList - bool


        // Useful basics:
        /// <summary>
        /// Makes link of nodes printable
        /// </summary>
        /// <typeparam name="T">Type of the node link</typeparam>
        /// <param name="nodes">Head node</param>
        /// <returns>string that's made of the node values</returns>
        public static string MakeNodesPrintable<T>(CookieNode<T>? nodes)
        {
            string str = "-->";
            while (nodes != null)
            {
                str += $"({nodes.GetValue()})-->";
                nodes = nodes.GetNext();
            } // while
            str += "NULL";
            return str;
        } // MakeNodeListPrintable 

        /// <summary>
        /// Counts number of elements in the list recursively
        /// </summary>
        /// <param name="nodes">Node list</param>
        /// <returns>Number of elements in the node list</returns>
        public static int RecursionSum(CookieNode<int>? nodes)
        {
            if (nodes == null) return 0;
            return nodes.GetValue() + RecursionSum(nodes.GetNext());
        } // RecursionSum

        /// <summary>
        /// The function checks if the element in in the given list
        /// </summary>
        /// <typeparam name="T">Type of the list and element</typeparam>
        /// <param name="nodes">Node list</param>
        /// <param name="value">Element/Item to look for</param>
        /// <returns>True if the element is in the node list</returns>
        public static bool ContainsElement<T>(CookieNode<T>? nodes, T value)
        {
            CookieNode<T> some_node = nodes;
            while (some_node != null)
            {
                if (nodes.Equals(value))
                    return true;
                some_node = some_node.GetNext();
            } // while
            return false;
        } // ContainsElement


        /// <summary>
        /// Reverses the node list 
        /// </summary>
        /// <typeparam name="T">Type of the list</typeparam>
        /// <param name="nodes">Node list to reverse</param>
        /// <returns>Reversed list</returns>
        public static CookieNode<T>? Reverse<T>(CookieNode<T>? nodes)
        {
            if (nodes == null || RecursionCount<T>(nodes) <= 1) 
                return nodes; 
            
            CookieNode<T> head = null;
            while (nodes != null) 
            {
                CookieNode<T> node = new(nodes);
                node.Next = head; 
                head = node; 
                nodes = nodes.Next; 
            } // while
            return head; 
        } // Reverse


        // Count:
        /// <summary>
        /// Recursively counts number of nodes linked
        /// </summary>
        /// <typeparam name="T">type of the node list</typeparam>
        /// <param name="nodes">nodes</param>
        /// <returns>number of nodes linked</returns>
        public static int RecursionCount<T>(CookieNode<T>? nodes)
        {
            if (nodes == null) return 0;
            return 1 + RecursionCount(nodes.GetNext());
        } // RecursionCount


        /// <summary>
        /// Recursively counts number of elements in the given list
        /// </summary>
        /// <typeparam name="T">Type of the list and element</typeparam>
        /// <param name="nodes">Node list</param>
        /// <param name="value">Element/Item to look for</param>
        /// <returns>Number of the elements found</returns>
        public static int RecursionCountElement<T>(CookieNode<T>? nodes, T value)
        {
            if (nodes == null) return 0;
            int same = 0;
            if (nodes.Equals(value))
                same = 1;
            return same + RecursionCountElement(nodes.GetNext(), value);
        } // RecursionCountElement
        

        // Add/Remove:
        /// <summary>
        /// Removes item from the list
        /// </summary>
        /// <param name="nodes">Nodes to add to</param>
        /// <param name="value">Item to add</param>
        public static CookieNode<T>? Remove<T>(CookieNode<T>? nodes, T value)
        {
            int length = RecursionCount(nodes);
            if (nodes == null)
                return nodes;

            // item is head
            if (nodes.Equals(value))
            {
                nodes = nodes.GetNext();
                return nodes;
            } // if

            CookieNode<T> some_node = nodes;
            while (some_node.GetNext() != null)
            {
                if (some_node.Next.Equals(value))
                {
                    some_node.SetNext(some_node.GetNext().GetNext());
                    return nodes;
                } // if
                some_node = some_node.GetNext();
            } // while
            return nodes;
        } // Remove


        /// <summary>
        /// Adds an item to the end of the list
        /// </summary>
        /// <param name="nodes">Nodes to add to</param>
        /// <param name="item">Item to add</param>
        public static CookieNode<T>? Append<T>(CookieNode<T>? nodes, T item)
        {
            if (nodes == null)
                return new CookieNode<T>(item);

            CookieNode<T> head = nodes;
            while (nodes.GetNext() != null)
                nodes = nodes.GetNext();

            nodes.SetNext(new CookieNode<T>(item));
            return head;
        } // Append

        /// <summary>
        /// Adds an item to the beginning of the list
        /// </summary>
        /// <param name="nodes">Nodes to add to</param>
        /// <param name="item">Item to add</param>
        public static CookieNode<T>? Add<T>(CookieNode<T>? nodes, T item)
        {
            CookieNode<T> node = new CookieNode<T>(item);
            if (nodes == null)
                return node;

            node.SetNext(nodes);
            return node;
        } // Add


    } // class CookieNodeWorker


} // namespace smth

