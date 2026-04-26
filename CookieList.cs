using NHunspell;
using System;
using System.Collections;


namespace smth
{
    // Nodes:
    /// <summary>
    /// Node for a node list
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
    /// Helping class that manipulates with nodes and node lists  -- not bagrut supported!
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


    // Lists:
    /// <summary>
    /// >:]  -- not bagrut supported!
    /// </summary>
    /// <typeparam name="ListType">Type of the node list</typeparam>
    public class CookieNodeList<ListType> : IEnumerable<ListType>, ICollection<ListType>
    {
        /// <summary>
        /// Number of slots busy at the moment
        /// </summary>
        private int count;
        /// <summary>
        /// The list itself
        /// </summary>
        private CookieNode<ListType>? head_node;
        /// <summary>
        /// Is the list ReadOnly
        /// </summary>
        public bool IsReadOnly { get; }


        // Builders:
        /// <summary>
        /// Class setter
        /// </summary>
        public CookieNodeList() { this.head_node = null; this.count = 0; }
        /// <summary>
        /// Class setter with valuables
        /// </summary>
        public CookieNodeList(ListType[] arr)
        {
            CookieNode<ListType> prev_node = null;
            this.count = 0;

            foreach (ListType value in arr)
            {
                CookieNode<ListType> some_node = new(value);

                if (this.count == 0)
                    this.head_node = some_node;
                else
                    prev_node.SetNext(some_node);

                prev_node = some_node;
                this.count++;
            } // foreach
        } // __init__
        /// <summary>
        /// Class setter with valuables
        /// </summary>
        private CookieNodeList(CookieNodeList<ListType> nodes)
        {
            CookieNode<ListType> prev_node = null;
            this.count = 0;

            for (int i = 0; i < nodes.Length; i++)
            {
                CookieNode<ListType> some_node = new(nodes.Get(i));

                if (this.count == 0)
                    this.head_node = some_node;
                else
                    prev_node.SetNext(some_node);

                prev_node = some_node;
                this.count++;
            } // foreach
        } // __init__



        // Length-connected
        /// <summary>
        /// Return current length of the list
        /// </summary>
        /// <returns>Int represented length</returns>
        public int Length { get { this.CheckCount(); return this.count; } }
        /// <summary>
        /// Return current length of the list
        /// </summary>
        /// <returns>Int represented length</returns>
        public int Count { get { this.CheckCount(); return this.count; } }


        /// <summary>
        /// Recounts number of nodes in the node list!
        /// </summary>
        private void CheckCount()
        { this.count = RecursionCount(this.head_node); }

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
            if (this.head_node == null || !this.Contains(item))
                return 0;

            CookieNode<ListType> node = this.head_node;
            return CountElementDuplicatesLoop(item, node);
        } // CountElementDuplicates

        /// <summary>
        /// Checks if the list has an item specified
        /// </summary>
        /// <param name="item">An item to check for</param>
        /// <returns>Number of items found</returns>
        private int CountElementDuplicatesLoop(ListType item, CookieNode<ListType> node)
        {
            int dupe = 0;
            if (node == null)
                return dupe;

            if (item.Equals(node.Value))
                dupe++;
            return dupe + CountElementDuplicatesLoop(item, node.GetNext());
        } // CountElementDuplicatesLoop



        // Getters
        /// <summary>
        /// Idk if works, but get element at index
        /// </summary>
        /// <param name="index">Index to get value from</param>
        /// <returns>Element at specified index</returns>
        public ListType this[int index] // INDEXES!
        {
            get => Get(index);
            set => this.AddAt(value, index);
        } // ListType

        /// <summary>
        /// Finds index of the item inputted. If the item is not found, returns -1
        /// </summary>
        /// <param name="item">an item that needs to be found</param>
        /// <returns>Int represented index</returns>
        public int GetIndex(ListType item)
        {
            this.CheckCount();
            if (this.head_node == null) return -1;

            CookieNode<ListType> some_node = this.head_node;
            int counter = 0;

            while (some_node != null)
            {
                if (some_node.Value.Equals(item))
                    return counter;
                counter++;
                some_node = some_node.GetNext();
            } // while
            return -1;
        } // GetIndex
        /// <summary>
        /// Finds index of the item inputted. If the item is not found, returns -1
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
        /// <exception cref="ArgumentOutOfRangeException">If the index is out-of-range</exception>
        public ListType Get(int index)
        {
            this.CheckCount();
            if (index < 0 || index >= this.count)
                throw new ArgumentOutOfRangeException(nameof(index));

            CookieNode<ListType>? some_node = this.head_node;
            for (; 0 < index; index--)
                some_node = some_node.GetNext();
            return some_node.Value;
        } // Get

        /// <summary>
        /// Returns first value of the list
        /// </summary>
        /// <returns>Found element, null if no elements are in the list</returns>
        public ListType GetFirst()
        {
            if (this.head_node == null || this.count == 0)
                return (ListType)(object)null;
            return this.Get(0);
        } // GetFirst


        // Special case : contains value
        /// <summary>
        /// Checks if the list has an item specified
        /// </summary>
        /// <param name="item">An item to check for</param>
        /// <returns>Boolean if the the item found</returns>
        public bool Contains(ListType item)
        { if (this.head_node == null) return false; return this.GetIndex(item) != -1; }

        // Special case : copy value
        /// <summary>
        /// The function return a copy of this object
        /// </summary>
        /// <returns></returns>
        public CookieNodeList<ListType> Copy()
        { return new CookieNodeList<ListType>(this); }


        // Special case : change type
        /// <summary>
        /// Changes the type of the list to the requested - be sure it's convertable!
        /// </summary>
        /// <typeparam name="RequestedType">Type to change to</typeparam>
        /// <returns>If possible, the list, if not null</returns>
        public CookieNodeList<RequestedType>? ChangeType<RequestedType>()
        {
            if (this.count == 0) return null;
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
                { return null; } // conversion failed - break

                nodes = nodes.Next;
            } // while

            return new_list;
        } // ChangeType

        /// <summary>
        /// Changes the type of the list to the requested
        /// </summary>
        /// <typeparam name="RequestedType">Type to change to</typeparam>
        /// <returns>If possible, the list, if not null</returns>
        public CookieNodeList<RequestedType>? PartlyChangeType<RequestedType>()
        {
            if (this.count == 0) return null;
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
            this.CheckCount();
            if (this.head_node == null)
            {
                this.head_node = new(item);
                this.count = 1;
                return;
            } // if

            CookieNode<ListType> some_node = this.head_node;
            while (some_node.GetNext() != null)
                some_node = some_node.GetNext();

            some_node.SetNext(new CookieNode<ListType>(item));
            this.count++;
        } // Append
        /// <summary>
        /// Adds another CookieNodeList to the end of this list
        /// </summary>
        /// <param name="nodes">List to add</param>
        public void Append(CookieNodeList<ListType> nodes)
        {
            if (nodes == null || nodes.Length == 0)
                return;

            CookieNode<ListType> tail = this.head_node;
            CookieNode<ListType> other_nodes = nodes.head_node;
            if (tail == null)
            {
                tail = new CookieNode<ListType>(other_nodes.GetValue());
                this.head_node = tail;
                other_nodes = other_nodes.GetNext();
            } // if 
            else
            {
                while (tail.GetNext() != null)
                    tail = tail.GetNext();
            } // else

            // add the nodes
            other_nodes = nodes.head_node;
            while (other_nodes != null)
            {
                tail.SetNext(new CookieNode<ListType>(other_nodes.GetValue()));
                tail = tail.GetNext();
                other_nodes = other_nodes.GetNext();
            } // while
            this.CheckCount();
        } // Append

        /// <summary>
        /// Adds an item to the beginning of the list
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Add(ListType item)
        {
            this.CheckCount();
            if (this.head_node == null)
            {
                this.head_node = new(item);
                this.count = 1;
                return;
            } // if

            CookieNode<ListType> some_node = new(item);
            some_node.SetNext(this.head_node);
            this.head_node = some_node;
            this.count++;
        } // Add
        /// <summary>
        /// Adds another CookieNodeList to the beginning of this list
        /// </summary>
        /// <param name="nodes">List to add</param>
        public void Add(CookieNodeList<ListType> nodes)
        {
            if (nodes == null || nodes.head_node == null)
                return;

            CookieNode<ListType> last = nodes.head_node;
            while (last.GetNext() != null)
                last = last.GetNext();

            last.SetNext(this.head_node);
            this.head_node = nodes.head_node;
            this.CheckCount();
        } // Add

        /// <summary>
        /// Adds an item to the beginning of the list
        /// </summary>
        /// <param name="item">Item to add</param>
        public void AddAt(ListType item, int index)
        {
            this.CheckCount();
            if (this.head_node == null)
            {
                this.head_node = new(item);
                this.count = 1;
                return;
            } // if

            CookieNode<ListType> some_node = new(item);
            some_node.SetNext(this.head_node);
            this.head_node = some_node;
            this.count++;
        } // Add



        // Removes:
        /// <summary>
        /// Removes the first instance of the item specified
        /// </summary>
        /// <param name="item">An item</param>
        /// <returns>True if the remove was successful; otherwise false</returns>
        public bool Remove(ListType item)
        {
            this.CheckCount();
            if (this.head_node == null)
                return false;

            // item is head
            if (this.head_node.Value.Equals(item))
            {
                this.head_node = this.head_node.GetNext();
                this.count--;
                return false;
            } // if

            CookieNode<ListType> some_node = this.head_node;
            while (some_node.GetNext() != null)
            {
                if (some_node.GetNext().Value.Equals(item))
                {
                    some_node.SetNext(some_node.GetNext().GetNext());
                    this.count--;
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
        /// <exception cref="ArgumentOutOfRangeException">If the index is out-of-range</exception>
        public void RemoveAt(int index)
        {
            this.CheckCount();
            if (this.head_node == null)
                return;

            if (index < 0 || index >= this.count)
                throw new ArgumentOutOfRangeException(nameof(index));

            // item is head
            if (index == 0)
            {
                this.head_node = this.head_node.GetNext();
                this.count--;
                return;
            }

            CookieNode<ListType> some_node = this.head_node;
            for (; index > 1; index--)
                some_node = some_node.GetNext();
            some_node.SetNext(some_node.GetNext().GetNext());
            this.count--;
        } // RemoveAt

        /// <summary>
        /// Removes the last element in the list
        /// </summary>
        public void RemoveLast()
        { if (this.count > 0) this.RemoveAt(this.count - 1); }
        /// <summary>
        /// Removes the first element in the list
        /// </summary>
        public void RemoveFirst()
        { if (this.count > 0) this.RemoveAt(0); }

        /// <summary>
        /// Removes all instance of element given, leaving one the first one
        /// </summary>
        /// <param name="item">Element to delete</param>
        public void RemoveDuplicates(ListType item)
        {
            int first = this.GetIndex(item);
            int counter = this.CountElementDuplicates(item);
            if (first == -1 || counter <= 1)
                return;

            for (int i = first + 1; i < this.Length && counter > 1; i++)
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
            if (!this.Contains(item)) return;
            this.RemoveDuplicates(item);
            this.Remove(item);
        } // RemoveAll


        /// <summary>
        /// Wipes the list clean
        /// </summary>
        public void Clear()
        {
            this.head_node = null;
            this.count = 0;
        } // Clear



        // overrride

        // Object
        public override string ToString()
        {
            string[] str = new string[this.count];
            CookieNode<ListType> some_node = this.head_node;
            for (int i = 0; i < this.count && some_node != null; i++)
            {
                str[i] = some_node.ToString();
                some_node = some_node.GetNext();
            } // for
            return String.Join(", ", str);
        } // override ToString

        public string ToString(string split)
        {
            string[] str = new string[this.count];
            CookieNode<ListType> some_node = this.head_node;
            for (int i = 0; i < this.count && some_node != null; i++)
            {
                str[i] = some_node.ToString();
                some_node = some_node.GetNext();
            } // for
            return String.Join(split, str);
        } // override ToString


        // IEnumerable
        public IEnumerator<ListType> GetEnumerator() // foreach!
        {
            CookieNode<ListType>? current = head_node;
            while (current != null)
            {
                yield return current.Value; // returns one value at a time
                current = current.Next;
            } // while
        } // GetEnumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        // ICollection

        /// <summary>
        /// Copies elements ot array given
        /// </summary>
        /// <param name="array">Array to copy to</param>
        /// <param name="arrayIndex">Copy from</param>
        public void CopyTo(ListType[] array, int arrayIndex)
        {
            foreach (var item in this)
                array[arrayIndex++] = item;
        } // CopyTo




        // Statics:
        /// <summary>
        /// The function reverses the list given
        /// </summary>
        /// <param name="list">List to reverse</param>
        public static void Reverse(CookieNodeList<object> list)
        {
            if (list == null || list.Length <= 1) return;
            CookieNodeList<object> copy_list = list.Copy();
            list.Clear();
            for (int i = 0; i < copy_list.Length; i++)
                list.Add(copy_list.Get(i));
        } // Reverse


    } // class CookieNodeList


    /// <summary>
    /// NodeList but always made of objects  -- not bagrut supported!
    /// </summary>
    public class CookieList : CookieNodeList<object>
    { }


} // namespace smth

