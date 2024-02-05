using System.Diagnostics;

namespace dwsh
{
    internal class Connection
    {
        private static int _count = 1000;

        public int Id { get; private set; }
        public bool IsActive { get; private set; }
        public string Host { get; private set; }
  

        public Connection(string host)
        {
            Id = _count++;
            Host = host;

            Process process = new Process();
            process.StartInfo = new ProcessStartInfo(Config.DamewareExecutable);
            process.StartInfo.Arguments = Config.DamewareArguments + Host;
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(ProcessExited);
            IsActive = process.Start();
            UpdateHistory();
        }


        private void ProcessExited(object sender, EventArgs e)
        {
            IsActive = false;
            UpdateHistory();

        }

        private void UpdateHistory()
        {
            string historyEvent = this.IsActive ? "Connect" : "Disconnect";
            string historyEntry = $"{DateTime.Now.ToString()} {historyEvent} {Host}";
            new Shell().RunCommand("history", ["_write", historyEntry]);
        }


    }
}
