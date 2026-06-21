using System;
using System.Globalization;


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
        private static TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        public CookieStructureArgumentException(string reason)
            : base(textInfo.ToTitleCase(reason)) { }
    } // CookieStructureArgumentException


} // namespace smth
