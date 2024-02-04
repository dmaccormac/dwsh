
namespace dwsh.Commands
{
    internal class VersionCommand : Command
    {

        public VersionCommand() : base("version") { }
  
        public override void Execute(string[] parameters)
        {
            Console.WriteLine(typeof(HelpCommand).Assembly.GetName().Version);

            

        }
    }
}
