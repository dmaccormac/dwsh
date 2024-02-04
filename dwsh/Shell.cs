using System.Collections;
using dwsh.Commands;

namespace dwsh
{
    public class Shell
    {

        private Hashtable _Commands = new Hashtable();
        public string Prompt { get; private set; }

        public Shell()
        {
            InitCommands();
            Prompt = "dwsh: ";
        }

        private void InitCommands()
        {
            AddCommand(new PackageCommand());
            AddCommand(new ConnectCommand());
            AddCommand(new HistoryCommand());
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
            Command command = (Command)_Commands[name];
            if (command == null)
            {
                Console.WriteLine("Command not found. Type help for a list of available commands.");
            }
            else
            {
                command.Execute(parameters);
            }

        }

    }
}

