﻿using System;

namespace DocoptNet
{
    public class DocoptExitException : DocoptBaseException
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public DocoptExitException()
        {
        }
        public DocoptExitException(string message) : base(message)
        {
        }
        public DocoptExitException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}