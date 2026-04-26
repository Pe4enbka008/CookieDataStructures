using System;

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
