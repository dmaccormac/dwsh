using static System.Environment;

namespace dwsh.Commands
{
    internal class HistoryCommand : Command
    {

        private readonly string _userProfile = GetFolderPath(SpecialFolder.UserProfile);
        private readonly string _logFile;


        public HistoryCommand() : base("history")
        {
            Help = """
                    history - show connection history

                    options:
                    -clear
                    remove all entries in history
                    """;

            _logFile = Path.Combine(_userProfile, Config.LogFile);
        }

        public override void Execute(string[] parameters)
        {
            //show history
            if (parameters.Length == 0)
            {
                string? line;
                try
                {
                    StreamReader sr = new(_logFile);
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
                File.Create(_logFile).Close();
            }


            // add to history
            else if (parameters[0] == "_write")
            {
                AppendLog(parameters[1]);
            }

            else
            {
                Console.WriteLine(Help);

            }

        }

        private void AppendLog(string message)
        {
            try
            {
                StreamWriter sw = new StreamWriter(_logFile, true);
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
