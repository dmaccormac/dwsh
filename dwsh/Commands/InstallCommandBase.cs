using System.Security.Principal;
using static dwsh.FileHelper;

namespace dwsh.Commands
{
    internal abstract class InstallCommandBase : Command
    {
        protected InstallCommandBase(string commandName) : base(commandName)
        {
        }

        protected static void HandleInstallOrUninstall(string[] parameters, Action<string> action)
        {

            if (!CurrentUserIsAdministrator())
            {
                Console.WriteLine("Administrative privileges are required for this action.");
                return;
            }

            try
            {
                var damewareDirectory = (parameters.Length > 1) ? parameters[1] : TryGetDamewareDirectory();
                action(damewareDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
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

            Console.WriteLine("Searching for Dameware installation...");
            var results = SearchForFile(Config.DamewareExecutable, searchDirectories);

            if (results.Count > 0)
                return Path.GetDirectoryName(results[0]) ?? throw new DirectoryNotFoundException();
            else
                throw new FileNotFoundException();
  
        }
    }
}
