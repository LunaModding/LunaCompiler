using System.CommandLine;
using System.IO.Compression;

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

            string baseDir = Environment.CurrentDirectory;

            Console.WriteLine("Downloading template...");

            using HttpClient client = new();
            using Stream stream = client.GetStreamAsync("https://github.com/LunaModding/LunaTemplate/archive/refs/heads/main.zip").GetAwaiter().GetResult();
            using ZipArchive archive = new(stream);

            Console.WriteLine("Extracting template...");
            archive.ExtractToDirectory(baseDir, true);

            string replaceDir = Path.Combine(baseDir, "LunaTemplate-main");

            Console.WriteLine("Applying values...");
            RecursivelyReplace(replaceDir, "$id$", id);
            RecursivelyReplace(replaceDir, "$version$", version);
            RecursivelyReplace(replaceDir, "$name$", name);
            RecursivelyReplace(replaceDir, "$description$", description);

            Console.WriteLine("Moving generated project...");
            foreach (string directory in Directory.EnumerateDirectories(replaceDir))
            {
                Directory.Move(directory, Path.Combine(baseDir, Path.GetFileName(directory)));
            }

            Console.WriteLine("Cleaning up...");
            Directory.Delete(replaceDir);

            Console.WriteLine("Done!");
        }

        private void RecursivelyReplace(string basePath, string phrase, string replacement)
        {
            var dir = new DirectoryInfo(basePath);

#pragma warning disable CS0618
            string replacedName = string.Copy(dir.FullName).Replace(phrase, replacement);
#pragma warning restore CS0618
            if (dir.FullName != replacedName && !Directory.Exists(replacedName))
                Directory.Move(dir.FullName, replacedName);
            dir = new DirectoryInfo(replacedName);
             
            foreach (FileInfo file in dir.GetFiles())
            {
                string changedFilePath = Path.Combine(dir.FullName, file.Name.Replace(phrase, replacement));
                File.Move(file.FullName, changedFilePath);
                var fileInfo = new FileInfo(changedFilePath);

                string text = File.ReadAllText(fileInfo.FullName);
                text = text.Replace(phrase, replacement);
                File.WriteAllText(fileInfo.FullName, text);
            }

            foreach (DirectoryInfo directory in dir.GetDirectories())
            {
                RecursivelyReplace(directory.FullName, phrase, replacement);
            }
        }
    }
}
