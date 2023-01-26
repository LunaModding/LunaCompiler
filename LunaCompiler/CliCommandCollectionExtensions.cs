using LunaCompiler.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.Data;

namespace LunaCompiler
{
    public static class CliCommandCollectionExtensions
    {
        public static IServiceCollection AddCliCommands(this IServiceCollection services)
        {
            Type grabCommandType = typeof(InitCommand);
            Type commandType = typeof(Command);

            IEnumerable<Type> commands = grabCommandType
                .Assembly
                .GetExportedTypes()
                .Where((x) => x.Namespace == "LunaCompiler.Commands" && commandType.IsAssignableFrom(x));

            foreach (Type command in commands)
            {
                services.AddSingleton(commandType, command);
            }

            return services;
        }
    }
}
