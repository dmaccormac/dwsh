using System.Collections;
using dwsh.Commands;

namespace dwsh
{
    public class Shell
    {

        private readonly Hashtable _Commands = [];
        public readonly string Prompt = "dwsh: ";

        public Shell()
        {
            InitCommands();

        }

        private void InitCommands()
        {
            AddCommand(new PackageCommand());
            AddCommand(new ConnectCommand());
            AddCommand(new LogCommand());
            AddCommand(new ClearCommand());
            AddCommand(new HelpCommand());
            AddCommand(new VersionCommand());
            AddCommand(new ExitCommand());
        }

        private void AddCommand(Command command)
        {
            _Commands.Add(command.Name, command);
        }
        public void RunCommand(string name, string[] parameters)
        {
            if (_Commands[name] is not Command command)
                Console.WriteLine("Command not found. Type help for a list of available commands.");
            
            else
                command.Execute(parameters);           

        }
    }
}

