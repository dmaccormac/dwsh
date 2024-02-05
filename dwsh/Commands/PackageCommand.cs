﻿using System.Security.Principal;


namespace dwsh.Commands
{
    internal class PackageCommand : Command
    {

        public PackageCommand() : base("package")
        {

            Help = Messages.Package;

        }

        public override void Execute(string[] parameters)
        {
            // help message
            if (parameters.Length == 0)
            {
                Console.WriteLine(Help);
                return;
            }

            // install
            if (parameters[0] == "-install")
            {
                if (IsAdministrator())
                {
                    string damewareDirectory = (parameters.Length > 1) ? parameters[1] : GetDamewareDirectory();
                    Installer.Run(damewareDirectory);
                }

                else
                    Console.WriteLine("Admnistrative privileges are required for this action.");
            }

            // uninstall
            else if (parameters[0] == "-uninstall")
            {
                if (IsAdministrator())
                {
                    string damewareDirectory = (parameters.Length > 1) ? parameters[1] : GetDamewareDirectory();
                    Uninstaller.Run(damewareDirectory);
                }

                else
                    Console.WriteLine("Admnistrative privileges are required for this action.");
            }

            else
            {
                Console.WriteLine(Help);
            }

        }
        private bool IsAdministrator()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        private string GetDamewareDirectory()
        {
            string[] directories = [Environment.ExpandEnvironmentVariables("%ProgramW6432%"),
                                    Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%")];

            foreach (string directory in directories)
            {
                String[] files = Directory.GetFiles(directory, Config.DamewareExecutable, SearchOption.AllDirectories);
                foreach (string file in files)
                    return Path.GetDirectoryName(file);

            }

            throw new Exception("Could not find Dameware installation directory");                     
        }      
    }
}
