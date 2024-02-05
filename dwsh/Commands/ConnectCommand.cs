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
                Console.WriteLine(Help);
                return;
            }

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].StartsWith("-"))
                {
                    ProcessOption(parameters[i], parameters, i);
                }
            }

        }

        private void ProcessOption(string name, string[] parameters, int i)
        {


            if (name == "-host")
            {
                while (i < parameters.Length - 1 && !parameters[++i].StartsWith("-"))
                {
                    Connection c = new Connection(parameters[i]);
                    connections.Add(c);
                }

            }

            else if (name == "-file")
            {

                while (i < parameters.Length - 1 && !parameters[++i].StartsWith("-"))
                {

                    try
                    {

                        string host = "";
                        using (StreamReader reader = new StreamReader(parameters[i]))
                        {
                            host = reader.ReadLine();
                        }



                        Connection c = new Connection(host);
                        connections.Add(c);
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("couldn't read info from file: " + e.ToString());
                    }

                }
            }


            else if (name == "-list")
            {
                Console.WriteLine("Name\t\tID\tActive\n------------------------------");

                foreach (Connection c in connections)
                {
                    Console.WriteLine(c.Host + "\t\t" + c.Id + "\t" + c.IsActive);
                }

            }

            else
            {
                Console.WriteLine(Help);
            }

        }
    }
}
