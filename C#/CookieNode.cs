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
    /// <typeparam name="T">Type of the node list</typeparam>
    public class CookieNode<T> : IEnumerable<T>
    {
        /// <summary>
        /// Node value
        /// </summary>
        private T? value;
        private CookieNode<T>? next;

        /// <summary>
        /// Is the list ReadOnly
        /// </summary>
        public bool IsReadOnly { get; }

        /// <summary>
        /// Class setter
        /// </summary>
        public CookieNode() { this.value = default; this.next = null; }
        /// <summary>
        /// Class setter with valuable
        /// </summary>
        public CookieNode(T value) { this.value = value; this.next = null; }
        /// <summary>
        /// Class setter with valuable  -- not bagrut supported!
        /// </summary>
        public CookieNode(CookieNode<T>? node) { this.value = node.GetValue(); this.next = null; }
        /// <summary>
        /// Class setter with valuable
        /// </summary>
        public CookieNode(T value, CookieNode<T>? next) { this.value = value; this.next = next; }


        // Value:
        /// <summary>
        /// Return value of the node  -- not bagrut supported!
        /// </summary>
        /// <returns>The value</returns>
        public T Value { get { return this.value; } }

        /// <summary>
        /// The function gets the mode value
        /// </summary>
        /// <returns>The value</returns>
        public T GetValue()
        { return this.value; }


        /// <summary>
        /// The function sets value if it's valid
        /// </summary>
        /// <param name="new_value">New value to set</param>
        public void SetValue(T new_value)
        { if (this.Valid(new_value)) this.value = new_value; }

        /// <summary>
        /// The function check if the given value is valid  -- not bagrut supported!
        /// </summary>
        /// <param name="value">New value to set</param>
        /// <returns>True if values given is valid</returns>
        private bool Valid(T value)
        { return true; }



        // Next:
        /// <summary>
        /// Returns next node, also can set the next node  -- not bagrut supported!
        /// </summary>
        /// <returns>The value</returns>
        public CookieNode<T>? Next { get { return this.next; } set { if (value is CookieNode<T> || value == null) this.next = value; } }

        /// <summary>
        /// The function returns next node
        /// </summary>
        /// <returns>Next node saved</returns>
        public CookieNode<T> GetNext()
        { return this.next; }


        /// <summary>
        /// The function saves next node
        /// </summary>
        /// <param name="new_value">The next node</param>
        public void SetNext(CookieNode<T> new_value)
        { this.next = new_value; }


        // override:
        /// <summary>
        /// Returns string of the value saved here
        /// </summary>
        /// <returns>Value as a string</returns>
        public override string ToString()
        { return $"{this.value}"; }



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

    } // class CookieNode


    /// <summary>
    /// Helping class that manipulates with nodes and node lists 
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
                return new CookieNode<T>();
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

        /// <summary>
        /// Creates node list based on array 
        /// </summary>
        /// <typeparam name="T">Any type for the nodes list</typeparam>
        /// <param name="list">Array to fill with</param>
        /// <returns>Node list</returns>
        public static CookieNode<T> NodeListToNodes<T>(CookieNodeList<T>? list)
        {
            if (list == null || list.Length == 0)
                return new CookieNode<T>();

            CookieNode<T> nodes = null, current = null;
            for (int i = 0; i < list.Length; i++)
            {
                T elem = list.Get(i);
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
            } // for
            return nodes;
        } // NodeListToNodes


        // Creaters:
        /// <summary>
        /// creates a Node list made of random strings using lib Hunspell
        /// </summary>
        /// <param name="length">Length of the list</param>
        /// <returns>the made list</returns>
        private static CookieNodeList<string> CreateStringList(int length)
        {
            // Downlaod .dic and .aff; in console "dotnet add package NHunspell"
            CookieNodeList<string> list = new CookieNodeList<string>();
            using (Hunspell hunspell = new Hunspell("en_US.aff", "en_US.dic"))
            {
                Random rnd = new Random();
                while (list.Length < length)
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
                        if (!list.Contains(suggestion) && hunspell.Spell(suggestion) && suggestion.Length > 3 && Char.IsAsciiLetterLower(suggestion[0])) // no repeats
                            list.Add(suggestion);
                    } // if
                } // while
            } // using
            return list;
        } // CreateList - string

        /// <summary>
        /// creates a Node list made of random strings using lib Hunspell
        /// </summary>
        /// <param name="length">Length of the list</param>
        /// <param name="least_letter_count">the least number of letters in the string, set to 4</param>
        /// <param name="most_letter_count">the most number of letters in the string, set to 100</param>
        /// <returns>the made list</returns>
        public static CookieNodeList<string> CreateList(int length, int least_letter_count = 4, int most_letter_count = 100)
        {
            if (least_letter_count > 8)
                least_letter_count = 0;
            if (most_letter_count < 4)
                most_letter_count = 10000;
            CookieNodeList<string> list = CreateStringList(length);
            int count = 0;
            while (count != length)
            {
                string word = list.Get(count);
                while (word.Length > most_letter_count || word.Length < least_letter_count)
                    word = CreateStringList(1).Get(0);
                list.AddAt(word, count);
                count++;
            } // while
            return list;
        } // CreateList - string

        /// <summary>
        /// creates a Node list made of random int/double/float numbers using lib Random
        /// </summary>
        /// <typeparam name="T">int double or float</typeparam>
        /// <param name="length">Length of the list</param>
        /// <param name="repeat">if reapeating is allowed</param>
        /// <returns>the made list</returns>
        public static CookieNodeList<T> CreateList<T>(int length, bool repeat = true)
        {
            if ((typeof(T) != typeof(int) && typeof(T) != typeof(float) && typeof(T) != typeof(double)) || length == 0)
                return new CookieNodeList<T>();

            CookieNodeList<T> list = new CookieNodeList<T>();
            Random rnd = new Random();
            while (list.Length < length)
            {
                T number = (T)(object)rnd.Next(length * 3);
                if (rnd.Next(4) % 2 == 0)
                    number = (T)(object)(-1 * (int)(object)number);
                if (!list.Contains(number) && !repeat) // no repeats
                    list.Append(number);
            } // while
            return list;
        } // CreateList - int; double; float

        /// <summary>
        /// creates a Node list made of random chars 
        /// </summary>
        /// <param name="length">Length of the list</param>
        /// <param name="repeat">if reapeating is allowed</param>
        /// <returns>the made list</returns>
        public static CookieNodeList<char> CreateList(int length, bool repeat = true)
        {
            if (length > 26 * 2)
                repeat = true;

            CookieNodeList<char> list = new CookieNodeList<char>();
            Random rnd = new Random();
            while (list.Length < length)
            {
                char letter = (char)('a' + rnd.Next(26));
                if (Char.IsLetter(letter) && rnd.Next(6) + 1 / 2 == 0)
                    letter = Char.ToUpper(letter);

                if (!repeat && list.Contains(letter))
                    continue;
                list.Add(letter);
            } // while
            return list;
        } // CreateList - char

        /// <summary>
        /// creates a Node list made of random bools using lib Random
        /// </summary>
        /// <param name="length">Length of the list</param>
        /// <returns>the made list</returns>
        public static CookieNodeList<bool> CreateList(int length)
        {
            CookieNodeList<bool> list = new CookieNodeList<bool>();
            Random rnd = new Random();
            while (list.Length < length)
                list.Add(rnd.Next(26) % 2 == 0);
            return list;
        } // CreateList - bool


        // Useful basics:
        /// <summary>
        /// Makes link of nodes printable
        /// </summary>
        /// <typeparam name="T">Type of the node link</typeparam>
        /// <param name="nodes">Head node</param>
        /// <returns>string that's made of the node values</returns>
        public static string MakeNodeListPrintable<T>(CookieNode<T>? nodes)
        {
            string str = "-->";
            while (nodes != null)
            {
                str += $"({nodes.GetValue()})-->";
                nodes = nodes.GetNext();
            }
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
        /// The function checks if th element in in the given list
        /// </summary>
        /// <typeparam name="T">Type of the list and element</typeparam>
        /// <param name="nodes">Node list</param>
        /// <param name="item">Element/Item to look for</param>
        /// <returns>True if the element is in the node list</returns>
        public static bool ContainsElement<T>(CookieNode<T>? nodes, T item)
        {
            CookieNode<T> some_node = nodes;
            while (some_node != null)
            {
                if (some_node.GetValue().Equals(item))
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
            /// Recursivly counts number of the 
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="nodes"></param>
            /// <returns></returns>
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
        /// <param name="item">Element/Item to look for</param>
        /// <returns>Number of the elements found</returns>
        public static int RecursionCountElement<T>(CookieNode<T>? nodes, T item)
        {
            if (nodes == null) return 0;
            int same = 0;
            if (nodes.GetValue().Equals(item))
                same = 1;
            return same + RecursionCountElement(nodes.GetNext(), item);
        } // RecursionCountElement
        

        // Add/Remove
        /// <summary>
        /// Removes item from the list
        /// </summary>
        /// <param name="nodes">Nodes to add to</param>
        /// <param name="item">Item to add</param>
        public static CookieNode<T>? Remove<T>(CookieNode<T>? nodes, T item)
        {
            int length = RecursionCount(nodes);
            if (nodes == null)
                return nodes;

            // item is head
            if (nodes.GetValue().Equals(item))
            {
                nodes = nodes.GetNext();
                return nodes;
            } // if

            CookieNode<T> some_node = nodes;
            while (some_node.GetNext() != null)
            {
                if (some_node.GetNext().GetValue().Equals(item))
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

