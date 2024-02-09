using System.Security.Principal;
using static dwsh.FileHelper;

namespace dwsh.Commands
{
    internal abstract class InstallCommandBase : Command
    {
        protected string damewareDirectory;

        protected InstallCommandBase(string commandName) : base(commandName)
        {
            damewareDirectory = "";
        }

        public override void Execute(string[] parameters)
        {
            if (parameters.Length > 0 && parameters[0] == "-help")
            {
                Console.WriteLine(Help);
                return;
            }

            if (!CurrentUserIsAdministrator())
            {
                Console.WriteLine("Administrative privileges are required for this action.");
                return;
            }

            try 
            {
                damewareDirectory = (parameters.Length > 1) ? parameters[1] : TryGetDamewareDirectory();

            }
            catch (Exception)
            {
                Console.WriteLine("Error getting Dameware directory");       
                throw;
            }

        }

        private static bool CurrentUserIsAdministrator()
        {
            var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static string TryGetDamewareDirectory()
        {
            string[] searchDirectories = { Environment.ExpandEnvironmentVariables("%ProgramW6432%"),
                                            Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%") };

            var results = SearchForFile(Config.DamewareExecutable, searchDirectories);

            if (results.Count > 0)
                return Path.GetDirectoryName(results[0]) ?? throw new DirectoryNotFoundException();
            else
                throw new FileNotFoundException();
  
        }
    }
}
