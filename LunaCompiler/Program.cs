using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;

namespace LunaCompiler
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            ServiceProvider serviceProvider = BuildServiceProvider();
            Parser parser = BuildParser(serviceProvider);

            return await parser.InvokeAsync(args).ConfigureAwait(false);
        }

        private static Parser BuildParser(ServiceProvider serviceProvider)
        {
            var commandLineBuilder = new CommandLineBuilder();

            foreach (Command command in serviceProvider.GetServices<Command>())
            {
                commandLineBuilder.Command.AddCommand(command);
                commandLineBuilder.Command.SetHandler(HandleRoot);
            }

            return commandLineBuilder.UseDefaults().Build();
        }

        private static ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddCliCommands();

            return services.BuildServiceProvider();
        }

        private static void HandleRoot()
        {
            Console.WriteLine("Luna Compiler - Create and compile Luna projects.");
            Console.WriteLine();
            Console.WriteLine("-- Commands --");
            Console.WriteLine("init - Creates a new Luna project.");
            Console.WriteLine("build - Compiles the Luna project in the current directory to a .lunad file.");
        }
    }
}