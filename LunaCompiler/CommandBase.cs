using System.CommandLine;

namespace LunaCompiler
{
    public abstract class CommandBase
    {
        public abstract string Name { get; }
        public string? Description { get; } = null;
        public abstract Option<object>[] Options { get; }
        public abstract Argument<object>[] Arguments { get; }
        public abstract Command[] SubCommands { get; }
        protected abstract Command Build(Command command);

        public Command Build()
        {
            return Build(new Command(Name, Description));
        }
    }
}
