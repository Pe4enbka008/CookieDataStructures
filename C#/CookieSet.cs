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
    /// This CookieDataStructure requires CookieNode.cs, CookieNodeList.cs and CookieDict.cs files!
    /// Tuple created and better-ed by Cookie :]
    /// </summary>
    /// <typeparam name="SetType">Type of the node tuple</typeparam>
    public class CookieSet<SetType> : IEnumerable<SetType>
    {
        private CookieDict<SetType, bool> values;
        public CookieSet()
        {
            values = new CookieDict<SetType, bool>();
        } // __init__


        public bool IsEmpty()
        {  return values.IsEmpty(); }



        // Getters:

        /// <summary>
        /// Get element at index
        /// </summary>
        /// <param name="index">Index to get value from</param>
        /// <returns>Element at specified index</returns>
        public SetType this[int index] // KEYS!
        {
            get => this.Get(index);
            set => this.SetAt(index, value);
        } // ListType


        /// <summary>
        /// Gets only one (first) value
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        /// <returns>The first accuring value in the key list</returns>
        public SetType Get(int index)
        { return this.values.CookieKeys[index]; }


        // Setters:

        /// <summary>
        /// Sets new value at index given, or fully new index
        /// </summary>
        /// <param name="index">Index of value to change</param>
        /// <param name="new_value">New value</param>
        public void Add(SetType new_value)
        { this.values.Set(new_value, true); } // Add

        /// <summary>
        /// Sets new value at index given, or fully new index
        /// </summary>
        /// <param name="index">Index of value to change</param>
        /// <param name="new_value">New value</param>
        public void SetAt(int index, SetType new_value)
        {
            try
            {
                SetType value = this.Get(index);
                this.values.Remove(value);
            } // try
            catch { }
            this.values.Set(new_value, true);
        } // SetAt


        // Removers:

        /// <summary>
        /// Removes specified key
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        public void Remove(SetType value)
        { this.values.Remove(value); } 

        /// <summary>
        /// Wipes the dictionary clean
        /// </summary>
        public void Clear()
        { this.values.Clear(); }



        // Special case: contains

        /// <summary>
        /// Searches for key in the dictionary
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        /// <returns>True if the key found; otherwise, false</returns>
        public bool Contains(SetType value)
        { return this.values.ContainsKey(value); }


        // Special case: copy

        /// <summary>
        /// Searches for key in the dictionary
        /// </summary>
        /// <param name="key">Key to the value(s)</param>
        /// <returns>True if the key found; otherwise, false</returns>
        public CookieSet<SetType> Copy()
        {
            if (this.IsEmpty())
                return new CookieSet<SetType>();
            CookieSet<SetType> set = new CookieSet<SetType>();
            foreach (SetType value in this)
                set.Add(value);
            return set;
        } // Copy



        // Combinations:

        /// <summary>
        /// Combine all unique values
        /// </summary>
        /// <param name="set">set to combine with</param>
        /// <returns>combination</returns>
        public CookieSet<SetType> Union(CookieSet<SetType> set)
        {   
            CookieSet<SetType> new_set = new CookieSet<SetType>();
            foreach(SetType value in this)
                new_set.Add(value);
            foreach (SetType value in set)
                new_set.Add(value);
            return new_set;
        } // Union

        /// <summary>
        /// Combine only shared values
        /// </summary>
        /// <param name="set">set to combine with</param>
        /// <returns>combination</returns>
        public CookieSet<SetType> Intersection(CookieSet<SetType> set)
        {
            CookieSet<SetType> new_set = new CookieSet<SetType>();
            foreach (SetType value in this)
                if (set.Contains(value))
                    new_set.Add(value);
            return new_set;
        } // Intersection

        /// <summary>
        /// Combine values that exist in current set
        /// </summary>
        /// <param name="set">set to combine with</param>
        /// <returns>combination</returns>
        public CookieSet<SetType> Difference(CookieSet<SetType> set)
        { 
            CookieSet<SetType> new_set = new CookieSet<SetType>();
            foreach (SetType value in this)
                if (!set.Contains(value))
                    new_set.Add(value);
            return new_set;
        } // Difference

        /// <summary>
        /// Combine values that exist in one of the sets
        /// </summary>
        /// <param name="set">set to combine with</param>
        /// <returns>combination</returns>
        public CookieSet<SetType> SymmetricDifference(CookieSet<SetType> set)
        {  
            CookieSet<SetType> new_set = new CookieSet<SetType>();
            foreach (SetType value in this)
                if (!set.Contains(value))
                    new_set.Add(value);
            foreach (SetType value in set)
                if (!this.Contains(value))
                    new_set.Add(value);
            return new_set;
        } // SymmetricDifference



        // override

        // IEnumerable
        public IEnumerator<SetType> GetEnumerator()
        { return this.values.CookieKeys.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // object 

        /// <summary>
        /// Override of the ToString
        /// </summary>
        /// <returns>The dist as { key: [], ... }</returns>
        public override string ToString()
        { return ToString(", "); }

        /// <summary>
        /// override for ToString to - { key: ['value', 'value', 'value', ... ]{split}key: [...] ...}
        /// </summary>
        /// <returns>string of the class</returns>
        public string ToString(string split)
        {
            CookieNodeList<SetType> list = this.values.CookieKeys;
            if (list.IsEmpty())
                return "{}";

            string str = "{";
            for (int index = 0; index < list.Count; index++)
            {
                str += list[index].ToString();
                if (index < list.Count - 1) str += split;
            } // for
            return str + "}";
        } // override ToString


    } // CookieTurtle
}
