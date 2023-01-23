using LunaCompiler.Commands;
using System.CommandLine;

namespace LunaCompiler
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand();
            rootCommand.AddCommand(new InitCommand().Build());

            rootCommand.SetHandler(HandleRoot);

            return await rootCommand.InvokeAsync(args);
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