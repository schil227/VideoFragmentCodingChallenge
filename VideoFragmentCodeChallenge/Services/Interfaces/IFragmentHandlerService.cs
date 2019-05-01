using System;
using System.Collections.Generic;
using System.Text;

namespace VideoFragmentCodeChallenge.Services.Interfaces
{
    public interface IFragmentHandlerService
    {
        int Handle(string fragmentDataFileName);
    }
}
