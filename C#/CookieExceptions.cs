using System;
using System.Globalization;

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
    public class CookieException : Exception
    {     // basically Interface - for try/catch
        public CookieException(string message) 
            : base(message) { }
    } // CookieException

    public class CookieEmptyStructureException : CookieException
    {
        public CookieEmptyStructureException(string structure)
            : base($"{structure} has no values") { }
    } // CookieEmptyStructureException

    public class CookieValueNotFoundException : CookieException
    {
        public CookieValueNotFoundException(object key)
            : base($"Key '{key}' was not found") { }
    } // CookieValueNotFoundException

    public class CookieIndexOutOfRangeException : CookieException
    {
        public CookieIndexOutOfRangeException(int index)
            : base($"Index {index} is out of range") { }
    } // CookieIndexOutOfRangeException

    public class CookieStructureArgumentException : CookieException
    {
        public CookieStructureArgumentException(string reason)
            : base(reason) { }
    } // CookieStructureArgumentException


} // namespace smth
