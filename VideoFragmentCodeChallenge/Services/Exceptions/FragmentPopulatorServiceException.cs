using System;
using System.Collections.Generic;
using System.Text;

namespace VideoFragmentCodeChallenge.Services.Exceptions
{
    public class FragmentPopulatorServiceException : Exception
    {
        public FragmentPopulatorServiceException() :
            base()
        {
        }

        public FragmentPopulatorServiceException(string message) :
            base(message)
        {
        }


        public FragmentPopulatorServiceException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
