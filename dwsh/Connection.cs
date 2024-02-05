using System.Diagnostics;

namespace dwsh
{
    class Connection
    {
        private static int _count = 1000;

        public int Id { get; private set; }
        public bool IsActive { get; private set; }
        public string Host { get; private set; }
  

        public Connection(string host)
        {
            Id = ++_count;
            this.Host = host;

            Process process = new Process();
            process.StartInfo = new ProcessStartInfo(Config.DamewareExecutable);
            process.StartInfo.Arguments = Config.DamewareArguments + host;
            process.EnableRaisingEvents = true; 
            process.Exited += new EventHandler(ProcessExited);
            IsActive = process.Start(); 

            string HistoryEntry = (DateTime.Now.ToString() + " Connect " + this.Host);
            new Shell().RunCommand("history", ["_write", HistoryEntry]);
        }


        private void ProcessExited(object sender, EventArgs e)
        {
            this.IsActive = false;
            string HistoryEntry = (DateTime.Now.ToString() + " Disconnect " + this.Host);
            new Shell().RunCommand("history", ["_write", HistoryEntry]);

        }


    }
}
