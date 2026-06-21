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



/*
CookieDataStructure: CookieDict contains
    IsEmpty - public function
    GetList - public function
    Get - public function
    Set - public function
    ContainsKey - public function
    ContainsValueIn - public function
    Remove - public function (2 overloads: key only, key and value)
    RemoveAll - public function
    Clear - public function
    ToString - public function (2 overloads: no-arg, with split string)

CookieDataStructure: CookieHash contains
    GetAs - public function
    GetListAs - public function
*/


namespace smth
{
    /// <summary>
    /// This CookieDataStructure requires CookieNode.cs and CookieNodeList.cs files!
    /// Dictionary created by Cookie 
    /// </summary>
    /// <typeparam name="KeyType">Type of the keys</typeparam>
    /// <typeparam name="ValueType">Types of the values</typeparam>
    public class CookieDict<KeyType, ValueType>
    {
        private CookieNodeList<KeyType> keys;
        private CookieNodeList<CookieNodeList<ValueType>> list_of_list_of_values;

        private static string className = "CookieDict";

        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieDict()
        {
            keys = new CookieNodeList<KeyType>();
            list_of_list_of_values = new CookieNodeList<CookieNodeList<ValueType>>();
        } // __init__


        /// <summary>
        /// Returns the System List of the Keys
        /// </summary>
        public List<KeyType> Keys
        {
            get
            {
                List<KeyType> all_keys = new();
                for (int i = 0; i < keys.Count; i++)
                    all_keys.Add(keys.Get(i));
                return all_keys;
            } // get
        } // Keys

        /// <summary>
        /// Returns the CookieNodeList of the Keys
        /// </summary>
        public CookieNodeList<KeyType> CookieKeys
        {
            get
            {
                CookieNodeList<KeyType> all_keys = new();
                for (int i = 0; i < keys.Count; i++)
                    all_keys.Append(keys.Get(i));
                return all_keys;
            } // get
        } // Keys


        /// <summary>
        /// Returns the System List of the Values
        /// </summary>
        public List<ValueType> Values
        {
            get
            {
                List<ValueType> all_values = new();
                for (int i = 0; i < list_of_list_of_values.Count; i++)
                {
                    CookieNodeList<ValueType> this_list = list_of_list_of_values.Get(i);
                    for (int t = 0; t < this_list.Count; t++)
                        all_values.Add(this_list.Get(t));
                } // for
                return all_values;
            } // get
        } // Values

        /// <summary>
        /// Returns the CookieNodeList of the Values
        /// </summary>
        public CookieNodeList<ValueType> CookieValues
        {
            get
            {
                CookieNodeList<ValueType> all_values = new();
                for (int i = 0; i < list_of_list_of_values.Count; i++)
                {
                    CookieNodeList<ValueType> this_list = list_of_list_of_values.Get(i);
                    for (int t = 0; t < this_list.Count; t++)
                        all_values.Append(this_list.Get(t));
                } // for
                return all_values;
            } // get
        } // Values



        /// <summary>
        /// Returns if the dictionary is empty
        /// </summary>
        /// <returns>True if there are no keys and no values</returns>
        public bool IsEmpty()
        { return this.keys.IsEmpty() && this.list_of_list_of_values.IsEmpty(); } 



        // Getters:

        /// <summary>
        /// Get element at index
        /// </summary>
        /// <param name="index">Index to get value from</param>
        /// <returns>Element at specified index</returns>
        public ValueType this[KeyType key] 
        {
            get => this.Get(key);
            set => this.Set(key, value);
        } // ListType

        /// <summary>
        /// Returns the whole list
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        /// <returns>The key list</returns>
        public CookieNodeList<ValueType> GetList(KeyType key)
        { if (ContainsKey(key)) return list_of_list_of_values.Get(keys.Find(key)); throw new CookieValueNotFoundException(key); }

        /// <summary>
        /// Gets only one (first) value
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        /// <returns>The first accuring value in the key list</returns>
        public ValueType Get(KeyType key)
        { if (ContainsKey(key)) return GetList(key).GetFirst(); throw new CookieValueNotFoundException(key); }


        // Setters:

        /// <summary>
        /// Sets new value at index given, or fully new index
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        /// <param name="value">The value</param>
        public void Set(KeyType key, ValueType value)
        {
            if (!ContainsKey(key))
            {
                keys.Append(key);
                list_of_list_of_values.Append(new CookieNodeList<ValueType>());
            } // if
            int index_in_list_of_lists = keys.Find(key);
            list_of_list_of_values.Get(index_in_list_of_lists).Append(value);
        } // Set



        // Special case: contains

        /// <summary>
        /// Searches for key in the dictionary
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        /// <returns>True if the key found; otherwise, false</returns>
        public bool ContainsKey(KeyType key)
        { return keys.Contains(key); }

        /// <summary>
        /// Searches for value in key in the dictionary
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        /// <param name="value">Value to look for</param>
        /// <returns>True if the key found; otherwise, false</returns>
        public bool ContainsValueIn(KeyType key, ValueType value)
        {
            CookieNodeList<ValueType> values_of_key = list_of_list_of_values.Get(keys.Find(key));
            return values_of_key.Contains(value);
        } // ContainsValueIn



        // Removers:

        /// <summary>
        /// Removes specified key
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        public void Remove(KeyType key)
        {
            if (!ContainsKey(key))
                return;
            int index_in_list_of_lists = keys.Find(key);
            keys.RemoveAt(index_in_list_of_lists);
            list_of_list_of_values.RemoveAt(index_in_list_of_lists);
        } // Remove

        /// <summary>
        /// Removes specified value from key
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        public void Remove(KeyType key, ValueType value)
        {
            if (!ContainsKey(key))
                return;
            int index_in_list_of_lists = keys.Find(key);
            list_of_list_of_values.Get(index_in_list_of_lists).Remove(value);
        } // Remove

        /// <summary>
        /// Removes all values like given from key
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        public void RemoveAll(KeyType key, ValueType value)
        {
            if (!ContainsKey(key))
                return;
            int index_in_list_of_lists = keys.Find(key);
            list_of_list_of_values.Get(index_in_list_of_lists).RemoveAll(value);
        } // RemoveAll

        /// <summary>
        /// Wipes the dictionary clean
        /// </summary>
        public void Clear()
        { this.list_of_list_of_values.Clear(); this.keys.Clear(); }



        // override

        /// <summary>
        /// Override of the ToString
        /// </summary>
        /// <returns>The dict as { 'key': ['value', 'value', 'value', ... ], 'key': [...], ... }</returns>
        public override string ToString()
        { return ToString(", "); } 

        /// <summary>
        /// override for ToString to - { 'key': ['value', 'value', 'value', ... ]{split}'key': [...] ...}
        /// </summary>
        /// <returns>string of the dict</returns>
        public string ToString(string split)
        {
            string str = "{";
            for (int key_index = 0; key_index < keys.Count; key_index++)
            {
                str += $"{keys.Get(key_index)}: ";
                CookieNodeList<ValueType> values = list_of_list_of_values.Get(key_index);
                str += values.ToString();
                if (key_index < keys.Count - 1) str += split; 
            } // for
            return str + "}";
        } // override ToString

    } // class CookieDict



    /// <summary>
    /// A simpler dictionary that maps any object key to any object value (or null)
    /// </summary>
    public class CookieHash : CookieDict<object, object> // key: obj and value: obj or null
    {
        /// <summary>
        /// getter with casting, can be null
        /// </summary>
        /// <typeparam name="T">type to cast to</typeparam>
        /// <param name="key">key to find</param>
        /// <returns></returns>
        public T GetAs<T>(object key)
        {
            object val = Get(key);
            if (val is T typedVal)
                return typedVal;
            return default;   // if not the type
        } // GetAs

        /// <summary>
        /// get a list of values in given type
        /// </summary>
        /// <typeparam name="T">type to return in</typeparam>
        /// <param name="key">key to find value in</param>
        /// <returns>The list</returns>
        public CookieNodeList<T> GetListAs<T>(object key)
        {
            var list = GetList(key); 
            CookieNodeList<T> typedList = new();
            if (list != null)
                for (int i = 0; i < list.Count; i++)
                    if (list.Get(i) is T item)
                        typedList.Add(item);
            return typedList;
        } // GetListAs


    } // class CookieHash


} // namespace smth


