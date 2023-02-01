using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaCompiler.Commands
{
    public class CompileCommand : Command
    {
        public CompileCommand() : base("compile", "Compiles a Luna project into a .lunad file.")
        {
            AddAlias("c");

            var compiler = new Option<string>(new string[] { "--compiler", "-c" })
            {
                Name = "compiler",
                Description = "The path to the compiler to use to compile .lua source files before they are packaged into a .lunad file.",
                IsRequired = false,
            };

            var arguments = new Option<string>(new string[] { "--arguments", "-a" })
            {
                Name = "arguments",
                Description = "The arguments to use if a compiler is set to compile .lua source files before they are packaged into a .lunad file. (NOTE: If using this option, you should mark where the main filename argument with go by substituting it with {MAIN}",
                IsRequired = false,
            };

            AddOption(compiler);
            AddOption(arguments);

            this.SetHandler(HandleCommand, compiler, arguments);
        }

        private void HandleCommand(string compiler, string arguments)
        {

        }
    }
}
