using Microsoft.Extensions.DependencyInjection;
using VideoFragmentCodeChallenge.Services.Implementations;
using VideoFragmentCodeChallenge.Services.Implementations.Factories;
using VideoFragmentCodeChallenge.Services.Interfaces;
using VideoFragmentCodeChallenge.Services.Interfaces.Factories;

namespace VideoFragmentCodeChallenge
{
    public static class CompositionRoot
    {
        /* Coding Challenge: Things I thought of
         * 
         *      The 'D' in SOLID 
         *  Dependency inversion is really neat. applying it to a project
         *  as small as this may seem like using a sledgehammer to crack
         *  a nut, but it really is quite useful. For one, coding to 
         *  abstractions is way easier than having high level modules 
         *  depend on everything all the way down. By reducing coupling
         *  between modules, the design becomes more clear. Also, it 
         *  allows for unit testing to take place via mocking said 
         *  abstractions.
         */
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IFragmentHandlerService, FragmentHandlerService>();
            services.AddTransient<IUniqueViewTimeCalculatorService, UniqueViewTimeCalculatorService>();
            services.AddTransient<IFragmentLoaderService, FragmentLoaderService>();
            services.AddTransient<IFragmentParserService, FragmentParserService>();
            services.AddTransient<IStreamReaderFactory, StreamReaderFactory>();
        }
    }
}
