using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VideoFragmentCodeChallenge.Services.Interfaces.Factories
{
    public interface IStreamReaderFactory
    {
        StreamReader Create(string filename);
    }
}
