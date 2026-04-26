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
    /// Dictinary created bu Cookie  -- not bagrut supported!
    /// </summary>
    /// <typeparam name="TypeKey">Type of the keys</typeparam>
    /// <typeparam name="TypeValue">Types of the values</typeparam>
    public class CookieDict<TypeKey, TypeValue>
    {
        private CookieNodeList<TypeKey> keys;
        private CookieNodeList<CookieNodeList<TypeValue>> list_of_list_of_values;
        public CookieDict()
        {
            keys = new CookieNodeList<TypeKey>();
            list_of_list_of_values = new CookieNodeList<CookieNodeList<TypeValue>>();
        } // __init__

        /// <summary>
        /// Returns the System List of the Keys
        /// </summary>
        public List<TypeKey> Keys
        {
            get
            {
                List<TypeKey> all_keys = new();
                for (int i = 0; i < keys.Length; i++)
                    all_keys.Add(keys.Get(i));
                return all_keys;
            } // get
        } // Keys

        /// <summary>
        /// Returns the CookieNodeList of the Keys
        /// </summary>
        public CookieNodeList<TypeKey> CookieKeys
        {
            get
            {
                CookieNodeList<TypeKey> all_keys = new();
                for (int i = 0; i < keys.Length; i++)
                    all_keys.Append(keys.Get(i));
                return all_keys;
            } // get
        } // Keys


        /// <summary>
        /// Returns the System List of the Values
        /// </summary>
        public List<TypeValue> Values
        {
            get
            {
                List<TypeValue> all_values = new();
                for (int i = 0; i < list_of_list_of_values.Length; i++)
                {
                    CookieNodeList<TypeValue> this_list = list_of_list_of_values.Get(i);
                    for (int t = 0; t < this_list.Length; t++)
                        all_values.Add(this_list.Get(t));
                } // for
                return all_values;
            } // get
        } // Values

        /// <summary>
        /// Returns the CookieNodeList of the Values
        /// </summary>
        public CookieNodeList<TypeValue> CookieValues
        {
            get
            {
                CookieNodeList<TypeValue> all_values = new();
                for (int i = 0; i < list_of_list_of_values.Length; i++)
                {
                    CookieNodeList<TypeValue> this_list = list_of_list_of_values.Get(i);
                    for (int t = 0; t < this_list.Length; t++)
                        all_values.Append(this_list.Get(t));
                } // for
                return all_values;
            } // get
        } // Values




        /// <summary>
        /// Sets new value at index given, or fully new index
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        /// <param name="value">The value</param>
        public void Set(TypeKey key, TypeValue value)
        {
            if (!ContainsIndex(key))
            {
                keys.Append(key);
                list_of_list_of_values.Append(new CookieNodeList<TypeValue>());
            } // if
            int index_in_list_of_lists = keys.Find(key);
            list_of_list_of_values.Get(index_in_list_of_lists).Append(value);
        } // Set



        /// <summary>
        /// Returns the whole list
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        /// <returns>The key list</returns>
        public CookieNodeList<TypeValue> GetList(TypeKey key)
        {
            if (!ContainsIndex(key))
                return null;
            return list_of_list_of_values.Get(keys.Find(key));
        } // GetList

        /// <summary>
        /// Gets only one (first) value
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        /// <returns>The first accuring value in the key list</returns>
        public TypeValue Get(TypeKey key)
        {
            if (ContainsIndex(key))
                return GetList(key).GetFirst();
            return default;
        } // Get



        /// <summary>
        /// Removes specified key
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        public void Remove(TypeKey key)
        {
            if (!ContainsIndex(key))
                return;
            int index_in_list_of_lists = keys.Find(key);
            keys.RemoveAt(index_in_list_of_lists);
            list_of_list_of_values.RemoveAt(index_in_list_of_lists);
        } // Remove

        /// <summary>
        /// Removes specified value from key
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        public void Remove(TypeKey key, TypeValue value)
        {
            if (!ContainsIndex(key))
                return;
            int index_in_list_of_lists = keys.Find(key);
            list_of_list_of_values.Get(index_in_list_of_lists).Remove(value);
        } // Remove

        /// <summary>
        /// Removes all values like given from key
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        public void RemoveAll(TypeKey key, TypeValue value)
        {
            if (!ContainsIndex(key))
                return;
            int index_in_list_of_lists = keys.Find(key);
            list_of_list_of_values.Get(index_in_list_of_lists).RemoveAll(value);
        } // RemoveAll



        /// <summary>
        /// Searches for key in the dictionary
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        /// <returns>True if the key found; otherwise, false</returns>
        public bool ContainsIndex(TypeKey key)
        { return keys.Contains(key); }

        /// <summary>
        /// Searches for value in key in the dictionary
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        /// <param name="value">Value to look for</param>
        /// <returns>True if the key found; otherwise, false</returns>
        public bool ContainsValueIn(TypeKey key, TypeValue value)
        {
            int index_in_list_of_lists = keys.Find(key);
            CookieNodeList<TypeValue> values_at_key = list_of_list_of_values.Get(index_in_list_of_lists);
            return values_at_key.Contains(value);
        } // ContainsValueIn



        // override
        /// <summary>
        /// Override of the ToString
        /// </summary>
        /// <returns>The dist as { key: [], ... }</returns>
        public override string ToString()
        {
            string str = "{ ";
            for (int key = 0; key < keys.Length; key++)
            {
                str += $"{keys.Get(key)}: [";
                CookieNodeList<TypeValue> values = list_of_list_of_values.Get(key);
                str += values.ToString() + "]";
                if (key < keys.Length - 1) str += ", ";  // no comma in the end!
            } // for
            return str + " }";
        } // override ToString

        /// <summary>
        /// override for ToString to - ['value'{split} 'value'{split} 'value'{split} ...]
        /// </summary>
        /// <returns>string of the class</returns>
        public string ToString(string split)
        {
            string str = "{ ";
            for (int key = 0; key < keys.Length; key++)
            {
                str += $"{keys.Get(key)}: [";
                CookieNodeList<TypeValue> values = list_of_list_of_values.Get(key);
                str += values.ToString() + "]";
                if (key < keys.Length - 1) str += split; 
            } // for
            return str + " }";
        } // override ToString

    } // class CookieDict


    public class CookieHash : CookieDict<object, object?> // key: obj and value: obj or null
    {
        /// <summary>
        /// getter with casting, can be null
        /// </summary>
        /// <typeparam name="T">type to cast to</typeparam>
        /// <param name="key">key to find</param>
        /// <returns></returns>
        public T? GetAs<T>(object key)
        {
            object? val = Get(key);
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
            var list = GetList(key);   // inherited GetList
            CookieNodeList<T> typedList = new();
            if (list != null)
                for (int i = 0; i < list.Length; i++)
                    if (list.Get(i) is T item)
                        typedList.Add(item);
            return typedList;
        } // GetListAs


    } // class CookieHash


} // namespace smth


