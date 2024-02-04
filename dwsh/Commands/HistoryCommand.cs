using static System.Environment;

namespace dwsh.Commands
{
    internal class HistoryCommand : Command
    {

        private string _userProfile = GetFolderPath(SpecialFolder.UserProfile);
        private const string Logfile = @"\dwsh.log";
        

        public HistoryCommand() : base("history")
        {
            Help = """
                    history - show connection history

                    options:
                    -clear
                    remove all entries in history
                    """;
        }

        public override void Execute(string[] parameters)
        {
            //show history
            if (parameters.Length == 0)
            {
                string? line;
                try
                {
                    StreamReader sr = new(_userProfile + @"\" + Logfile);
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        Console.WriteLine(line);
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }

            }
            // clear history
            else if (parameters[0] == "-clear")
            {
                WriteLog("", false);
            }

            // help message
            else if (parameters[0] == "-help" || parameters[0] == "help")
            {
                Console.WriteLine(Help);
            }

            // add to history
            else if (parameters[0] == "_write")
            {
                WriteLog(parameters[1], true);
            }

            else
            {
                Console.WriteLine(Help);

            }

        }

        private void WriteLog(string message, bool append)
        {
            try
            {
                StreamWriter sw = new StreamWriter(_userProfile + @"\" + Logfile, append);
                sw.WriteLine(message);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }


    }
}
