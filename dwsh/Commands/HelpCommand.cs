namespace dwsh.Commands
{
    internal class HelpCommand : Command
    { 
        public HelpCommand() : base("help")
        {
            Help = Messages.Help;
        }
        public override void Execute(string[] parameters)
        {
            Console.WriteLine(Help);
        }
    }
}
