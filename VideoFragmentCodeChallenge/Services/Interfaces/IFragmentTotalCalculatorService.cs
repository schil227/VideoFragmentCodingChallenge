using System;
using System.Collections.Generic;
using System.Text;
using VideoFragmentCodeChallenge.DataModel;

namespace VideoFragmentCodeChallenge.Services.Interfaces
{
    public interface IFragmentTotalCalculatorService
    {
        int CalculateTotal(IReadOnlyCollection<Fragment> fragments);
    }
}
