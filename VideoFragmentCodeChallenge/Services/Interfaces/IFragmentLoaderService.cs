using System;
using System.Collections.Generic;
using System.Text;
using VideoFragmentCodeChallenge.DataModel;

namespace VideoFragmentCodeChallenge.Services.Interfaces
{
    public interface IFragmentLoaderService
    {
        IReadOnlyCollection<Fragment> Load(string filename);
    }
}
