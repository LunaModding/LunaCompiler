using System.CommandLine;
using System.IO.Compression;
using System.Text;

namespace LunaCompiler.Commands
{
    public class CompileCommand : Command
    {
        public CompileCommand() : base("compile", "Compiles a Luna project into a .lunad file.")
        {
            AddAlias("c");

            this.SetHandler(HandleCommand);
        }

        private void HandleCommand()
        {
            Console.WriteLine("Locating manifest...");
            string[] manifests = Directory.EnumerateFiles(Environment.CurrentDirectory, "manifest.json", SearchOption.AllDirectories).ToArray();

            if (manifests.Length == 0 || manifests == null)
            {
                throw new FileNotFoundException("Unable to find a manifest for the Luna project.");
            }

            string manifest = manifests[0];

            string? projectDir = Path.GetDirectoryName(manifest);

            // Todo, read manifest and compile lua

            string outputName = Path.Combine(Environment.CurrentDirectory, $"{Path.GetFileName(projectDir)}.lunad");

            if (File.Exists(outputName))
            {
                File.Delete(outputName);
            }

            Console.WriteLine("Packaging project...");
            ZipFile.CreateFromDirectory(projectDir, outputName, CompressionLevel.Optimal, false);

            Console.WriteLine("Done!");
        }
    }
}
