using System.Data;
using static dwsh.FileHelper;

namespace dwsh.Commands
{
    public class ConnectCommand : Command
    {
        private static List<Connection> connections = new List<Connection>();

        public ConnectCommand() : base("connect")
        {
            Help = Messages.Connect;
        }

        public override void Execute(string[] parameters)
        {
            if (parameters.Length == 0)
            {
                try
                { 
                    Console.Write("Enter hostname: ");
                    var host = Console.ReadLine();

                    if (host != null && host.Trim() != "")
                        ConnectToHost(host);
                    else
                        Console.WriteLine("Host cannot be null.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to host: {ex.Message}");
                }
            }

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].StartsWith("-"))
                {
                    ProcessOption(parameters[i], parameters, ref i);
                }
            }
        }

        private void ProcessOption(string name, string[] parameters, ref int i)
        {
            switch (name)
            {
                case "-host":
                    ProcessHostOption(parameters, ref i);
                    break;
                case "-file":
                    ProcessFileOption(parameters, ref i);
                    break;
                case "-list":
                    ProcessListOption();
                    break;
                default:
                    Console.WriteLine(Help);
                    break;
            }
        }
        private void ProcessOptionParameters(string[] parameters, ref int i, Func<string, string> extractor)
        {
            if (i == parameters.Length - 1)
            {
                Console.WriteLine("You must provide at least one parameter.");
                return;
            }

            while (i < parameters.Length - 1 && !parameters[i + 1].StartsWith("-"))
            {
                try
                {
                    var parameter = parameters[++i];
                    var host = extractor(parameter);
                    ConnectToHost(host);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing parameter: {ex.Message}");
                }
            }
        }

        private void ProcessHostOption(string[] parameters, ref int i)
        {
            ProcessOptionParameters(parameters, ref i, ExtractHostFromParameter);
        }

        private void ProcessFileOption(string[] parameters, ref int i)
        {
            ProcessOptionParameters(parameters, ref i, ExtractHostFromFile);
        }

        private string ExtractHostFromParameter(string parameter)
        {
            return parameter;
        }

        private string ExtractHostFromFile(string file)
        {
            return ReadTextFromFile(file);
        }

        private static void ConnectToHost(string host)
        {
            try
            {
                Connection c = new(host);
                connections.Add(c);
            }

            catch (Exception)
            {
                throw;
            }

        }
        private void ProcessListOption()
        {
            Console.WriteLine("ID\tActive\tHost");
            Console.WriteLine("--\t------\t----");

            foreach (Connection c in connections)
            {
                Console.WriteLine($"{c.Id}\t{c.IsActive}\t{c.Host}");
            }
        }
    }
}
