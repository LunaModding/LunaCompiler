using System.CommandLine;

namespace LunaCompiler.Commands
{
    public class InitCommand : Command
    {
        public InitCommand() : base("init", "Creates a new Luna project.")
        {
            AddAlias("i");

            var id = new Argument<string>()
            {
                Name = "id",
                Description = "The id of the project to create. Should be all lower-case",
            };

            var version = new Option<string>(new string[] { "--version", "-v" })
            {
                Name = "version",
                Description = "The version of the project to create.",
                IsRequired = false,
            };
            version.SetDefaultValue("0.1.0");

            var name = new Option<string>(new string[] { "--name", "-n" })
            {
                Name = "name",
                Description = "The name of the project to create.",
                IsRequired = false,
            };
            
            var description = new Option<string>(new string[] { "--description", "-d" })
            {
                Name = "description",
                Description = "The description of the project to create.",
                IsRequired = false,
            };

            AddArgument(id);
            AddOption(version);
            AddOption(name);
            AddOption(description);

            this.SetHandler(HandleCommand, id, version, name, description);
        }

        private void HandleCommand(string id, string version, string name, string description)
        {
            name = string.IsNullOrEmpty(name) ? id : name;
            description = string.IsNullOrEmpty(description) ? $"{id}, v{version}" : description;
        }
    }
}
