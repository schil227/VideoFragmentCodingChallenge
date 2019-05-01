using Microsoft.Extensions.DependencyInjection;
using VideoFragmentCodeChallenge.Services.Implementations;
using VideoFragmentCodeChallenge.Services.Implementations.Factories;
using VideoFragmentCodeChallenge.Services.Interfaces;
using VideoFragmentCodeChallenge.Services.Interfaces.Factories;

namespace VideoFragmentCodeChallenge
{
    public static class CompositionRoot
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IFragmentHandlerService, FragmentHandlerService>();
            services.AddTransient<IFragmentTotalCalculatorService, FragmentTotalCalculatorService>();
            services.AddTransient<IFragmentLoaderService, FragmentLoaderService>();
            services.AddTransient<IFragmentParserService, FragmentParserService>();
            services.AddTransient<IStreamReaderFactory, StreamReaderFactory>();
        }
    }
}
