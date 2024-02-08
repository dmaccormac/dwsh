﻿using static System.Environment;
using static dwsh.FileHelper;

namespace dwsh.Commands
{
    internal class LogCommand : Command
    {

        private readonly string _userProfile = GetFolderPath(SpecialFolder.UserProfile);
        private readonly string _logFile;


        public LogCommand() : base("log")
        {
            Help = Messages.Log;
            _logFile = Path.Combine(_userProfile, Config.LogFile);
        }

        public override void Execute(string[] parameters)
        {
            //show history
            if (parameters.Length == 0)
            {
                try
                {
                    Console.WriteLine($"Log file: {_logFile}");
                    var logText = ReadTextFromFile(_logFile);
                    Console.WriteLine(logText);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

            }


            else if (parameters[0] == "_write")
            {
                try
                {
                    var logEntry = parameters[1];
                    AppendTextToFile(_logFile, logEntry);
                }

                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            else
            {
                Console.WriteLine(Help);

            }

        }
     
    }
}
