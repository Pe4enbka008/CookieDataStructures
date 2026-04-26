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
    /// Tuple :]
    /// </summary>
    public class CookieTurtle<T> : CookieNodeList<T>
    {
        private int length;

        ///// <summary>
        ///// Class constructor
        ///// </summary>
        //public CookieTurtle(CookieNodeList<T> list)
        //{ 
        //    this.length = list.Length;

        //} // __init__


        /// <summary>
        /// Class constructor
        /// </summary>
        public CookieTurtle(T[] list) : base(list)
        { this.length = list.Length; }







    } // CookieTurtle
}
