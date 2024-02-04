namespace dwsh.Commands
{
    internal class ClearCommand : Command
    {
        public ClearCommand() : base("clear")
        {
        }

        public override void Execute(string[] parameters)
        {
            Console.Clear();
        }
    }
}
