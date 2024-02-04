namespace dwsh.Commands
{
    internal class HelpCommand : Command
    { 
        public HelpCommand() : base("help")
        {
            Help = """
            dwsh - command shell for dameware mini remote control client.
            
            Commands:
            connect     connect to a host
            history     show connection history
            package     install or uninstall dwsh
            clear       clear the console screen
            version     show version info
            exit        quit the program

            To see detailed help for a command use the -help option
            Example: connect -help

            https://github.com/dmaccormac
            """;
        }
        public override void Execute(string[] parameters)
        {
            Console.WriteLine(Help);
        }
    }
}
