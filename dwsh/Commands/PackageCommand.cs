using System.Security.Principal;


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
            if (parameters.Length == 0)
            {
                Console.WriteLine(Help);
                return;
            }

            string parameter = parameters[0];

            if (parameter == "-install")
            {
                if (IsAdministrator())
                {
                    string? damewareDirectory = (parameters.Length > 1) ? parameters[1] : GetDamewareDirectory();
                    if (damewareDirectory == null)
                        Console.WriteLine("Could not find Dameware installation path.");
                    else
                        Installer.Run(damewareDirectory);

                }

                else
                    Console.WriteLine("Admnistrative privileges are required for this action.");
            }

            else if (parameter == "-uninstall")
            {
                if (IsAdministrator())
                {
                    string? damewareDirectory = (parameters.Length > 1) ? parameters[1] : GetDamewareDirectory();
                    if (damewareDirectory == null)
                        Console.WriteLine("Could not find Dameware installation path.");
                    else
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

       
        private static bool IsAdministrator()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        private string? GetDamewareDirectory()
        {
            string[] directories = [Environment.ExpandEnvironmentVariables("%ProgramW6432%"),
                                    Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%")];

            foreach (string directory in directories)
            {
                String[] files = Directory.GetFiles(directory, Config.DamewareExecutable, SearchOption.AllDirectories);
                foreach (string file in files)
                    return Path.GetDirectoryName(file);

            }

            return null;                    
        }      
    }
}
