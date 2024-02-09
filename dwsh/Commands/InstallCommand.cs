

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

            try
            {
                base.Execute(parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }

            Installer.Run(damewareDirectory);
        }
    }
}
