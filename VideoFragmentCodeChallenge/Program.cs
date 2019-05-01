using Microsoft.Extensions.DependencyInjection;
using System;
using VideoFragmentCodeChallenge.Services.Interfaces;

namespace VideoFragmentCodeChallenge
{
    class Program
    {
        static int Main(string[] args)
        {
            var services = new ServiceCollection();
            CompositionRoot.AddServices(services);

            var serviceProvider = services.BuildServiceProvider();

            var fragmentHandlingService = serviceProvider.GetService<IFragmentHandlerService>();

            Console.WriteLine($"Processing Fragments from file {args[0]}");

            var totalTimeInMs = fragmentHandlingService.Handle(args[0]);

            Console.WriteLine($"Total time of fragments: {totalTimeInMs}");
            Console.ReadKey();

            return totalTimeInMs;
        }
    }
}
