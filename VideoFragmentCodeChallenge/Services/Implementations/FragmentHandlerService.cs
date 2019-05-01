using System;
using System.Collections.Generic;
using System.Text;
using VideoFragmentCodeChallenge.Services.Interfaces;

namespace VideoFragmentCodeChallenge.Services.Implementations
{
    public class FragmentHandlerService : IFragmentHandlerService
    {
        private readonly IFragmentLoaderService fragmentLoaderService;
        private readonly IFragmentTotalCalculatorService fragmentTotalCalculatorService;

        public FragmentHandlerService(
            IFragmentLoaderService fragmentLoaderService,
            IFragmentTotalCalculatorService fragmentTotalCalculatorService
            )
        {
            this.fragmentLoaderService = fragmentLoaderService;
            this.fragmentTotalCalculatorService = fragmentTotalCalculatorService;
        }

        public int Handle(string fragmentDataFileName)
        {
            var fragments = fragmentLoaderService.Load(fragmentDataFileName);

            return fragmentTotalCalculatorService.CalculateTotal(fragments);
        }
    }
}
