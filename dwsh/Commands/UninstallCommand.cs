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
            try
            {
                base.Execute(parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }

            Uninstaller.Run(damewareDirectory);
        }
    }
}
