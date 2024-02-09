namespace dwsh.Commands
{
    internal class UninstallCommand : InstallCommandBase
    {
        public UninstallCommand() : base("uninstall")
        {
            Help = Messages.Uninstall;
        }

        public override void Execute(string[] parameters)
        {
            if (parameters.Length > 0 && parameters[0] == "-help")
            {
                Console.WriteLine(Help);
                return;
            }

            HandleInstallOrUninstall(parameters, Uninstaller.Run);
        }
    }
}
