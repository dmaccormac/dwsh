using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using static dwsh.FileHelper;


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
                if (CurrentUserIsAdministrator())
                {
                    var damewareDirectory = (parameters.Length > 1) ? parameters[1] : TryGetDamewareDirectory();

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
                if (CurrentUserIsAdministrator())
                {
                    var damewareDirectory = (parameters.Length > 1) ? parameters[1] : TryGetDamewareDirectory();

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

       
        private static bool CurrentUserIsAdministrator()
        {
            var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());     
            var isAdministrator = principal.IsInRole(WindowsBuiltInRole.Administrator);
            return isAdministrator;
        }

        private string? TryGetDamewareDirectory()
        {
            string? damewareDirectory = null;
            string[] searchDirectories = [Environment.ExpandEnvironmentVariables("%ProgramW6432%"),
                                    Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%")];


            var results = SearchForFile(Config.DamewareExecutable, searchDirectories);

            if (results.Count > 0)
                damewareDirectory = results[0];

            return damewareDirectory;
            
        }      
    }
}
