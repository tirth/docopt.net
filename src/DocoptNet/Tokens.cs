using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DocoptNet
{
    public class Tokens: IEnumerable<string>
    {
        public Type ErrorType { get; }
        
        private readonly List<string> _tokens = new List<string>();

        public Tokens(IEnumerable<string> source, Type errorType)
        {
            ErrorType = errorType ?? typeof(DocoptInputErrorException);
            _tokens.AddRange(source);
        }

        public Tokens(string source, Type errorType)
        {
            ErrorType = errorType ?? typeof(DocoptInputErrorException);
            _tokens.AddRange(source.Split(new char[0], StringSplitOptions.RemoveEmptyEntries));
        }

        public bool ThrowsInputError 
            => ErrorType == typeof (DocoptInputErrorException);


        public static Tokens FromPattern(string pattern)
        {
            var spacedOut = Regex.Replace(pattern, @"([\[\]\(\)\|]|\.\.\.)", @" $1 ");
            var source = Regex.Split(spacedOut, @"\s+|(\S*<.*?>)").Where(x => !string.IsNullOrEmpty(x));
            return new Tokens(source, typeof(DocoptLanguageErrorException));
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _tokens.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string Move()
        {
            string s = null;
            if (_tokens.Count > 0)
            {
                s = _tokens[0];
                _tokens.RemoveAt(0);
            }
            return s;
        }

        public string Current()
        {
            return (_tokens.Count > 0) ? _tokens[0] : null;
        }

        public Exception CreateException(string message)
        {
            return Activator.CreateInstance(ErrorType, message) as Exception;
        }

        public override string ToString()
        {
            return $"current={Current()},count={_tokens.Count}";
        }
    }
}