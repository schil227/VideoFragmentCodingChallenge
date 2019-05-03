using System;
using System.Collections.Generic;
using System.Text;
using VideoFragmentCodeChallenge.Services.Interfaces;

namespace VideoFragmentCodeChallenge.Services.Implementations
{
    public class FragmentHandlerService : IFragmentHandlerService
    {
        private readonly IFragmentLoaderService fragmentLoaderService;
        private readonly IUniqueViewTimeCalculatorService uniqueViewTimeCalculatorService;

        public FragmentHandlerService(
            IFragmentLoaderService fragmentLoaderService,
            IUniqueViewTimeCalculatorService uniqueViewTimeCalculatorService
            )
        {
            this.fragmentLoaderService = fragmentLoaderService;
            this.uniqueViewTimeCalculatorService = uniqueViewTimeCalculatorService;
        }

        public int Handle(string fragmentDataFileName)
        {
            var fragments = fragmentLoaderService.Load(fragmentDataFileName);

            return uniqueViewTimeCalculatorService.CalculateTotal(fragments);
        }
    }
}
