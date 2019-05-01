using System;
using System.Collections.Generic;
using System.Text;

namespace VideoFragmentCodeChallenge.Services.Exceptions
{
    public class FragmentParserServiceException : Exception
    {
        public FragmentParserServiceException() :
            base()
        {
        }

        public FragmentParserServiceException(string message) :
            base(message)
        {
        }


        public FragmentParserServiceException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
