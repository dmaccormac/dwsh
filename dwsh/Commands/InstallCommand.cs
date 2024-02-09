

namespace dwsh.Commands
{
    internal class InstallCommand : InstallCommandBase
    {
        public InstallCommand() : base("install")
        {
            Help = Messages.Install;
        }

        public override void Execute(string[] parameters)
        {
            if (parameters.Length > 0 && parameters[0] == "-help")
            {
                Console.WriteLine(Help);
                return;
            }

            HandleInstallOrUninstall(parameters, Installer.Run);          
        }
    }
}
