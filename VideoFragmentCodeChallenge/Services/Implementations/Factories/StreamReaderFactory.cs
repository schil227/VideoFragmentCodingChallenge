using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VideoFragmentCodeChallenge.Services.Interfaces.Factories;

namespace VideoFragmentCodeChallenge.Services.Implementations.Factories
{
    internal class StreamReaderFactory : IStreamReaderFactory
    {
        public StreamReader Create(string filename)
        {
            return new StreamReader(filename);
        }
    }
}
