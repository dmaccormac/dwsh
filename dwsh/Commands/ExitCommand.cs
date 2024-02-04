namespace dwsh.Commands
{
    internal  class ExitCommand : Command
    {
        public ExitCommand() : base("exit")
        {
        }

        public override void Execute(string[] parameters)
        {
            Environment.Exit(0);
        }
    }
}
