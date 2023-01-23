using System.CommandLine;

namespace LunaCompiler.Commands
{
    public class InitCommand : CommandBase
    {
        public override string Name => "init";

        public override Option<object>[] Options => Array.Empty<Option<object>>();

        public override Argument<object>[] Arguments => Array.Empty<Argument<object>>();

        public override Command[] SubCommands => Array.Empty<Command>();

        public void Handler()
        {
            Console.WriteLine("Nice init bro.");
        }

        protected override Command Build(Command command)
        {
            command.SetHandler(Handler);
            return command;
        }
    }
}
