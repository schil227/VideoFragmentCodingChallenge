using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VideoFragmentCodeChallenge.DataModel;
using VideoFragmentCodeChallenge.Services.Exceptions;
using VideoFragmentCodeChallenge.Services.Interfaces;
using VideoFragmentCodeChallenge.Services.Interfaces.Factories;

namespace VideoFragmentCodeChallenge.Services.Implementations
{
    public class FragmentLoaderService : IFragmentLoaderService
    {
        public readonly IStreamReaderFactory streamReaderFactory;
        public readonly IFragmentParserService fragmentParserService;

        public FragmentLoaderService(
            IStreamReaderFactory streamReaderFactory,
            IFragmentParserService fragmentParserService
            )
        {
            this.streamReaderFactory = streamReaderFactory;
            this.fragmentParserService = fragmentParserService;
        }

        public IReadOnlyCollection<Fragment> Load(string filename)
        {
            if (filename == null)
            {
                throw new ArgumentNullException(nameof(filename));
            }

            var fragments = new List<Fragment>();

            try
            {
                using (var file = streamReaderFactory.Create(filename))
                {
                    string line;

                    while ((line = file.ReadLine()) != null)
                    {
                        if (!line.StartsWith('#') && !string.IsNullOrWhiteSpace(line))
                        {
                            fragments.Add(fragmentParserService.Parse(line));
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                throw new FragmentPopulatorServiceException($"Failed to read the file {filename}", ex);
            }

            return fragments;
        }
    }
}
