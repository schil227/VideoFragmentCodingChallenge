using Microsoft.Extensions.DependencyInjection;
using System;
using VideoFragmentCodeChallenge.Services.Interfaces;

namespace VideoFragmentCodeChallenge
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please specifiy a file when calling this program.");
                Console.WriteLine(@"E.g.: .\VideoFragmentCodingChallenge.exe .\myTestData.txt");
                return 0;
            }

            var services = new ServiceCollection();
            CompositionRoot.AddServices(services);

            var serviceProvider = services.BuildServiceProvider();

            var fragmentHandlingService = serviceProvider.GetService<IFragmentHandlerService>();

            Console.WriteLine($"Processing Fragments from file {args[0]}");

            var totalTimeInMs = fragmentHandlingService.Handle(args[0]);

            Console.WriteLine($"Total Unique View Time: {totalTimeInMs}");
            Console.ReadKey();

            return totalTimeInMs;
        }
    }
}
