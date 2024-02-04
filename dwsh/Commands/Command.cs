namespace dwsh.Commands
{
    public abstract class Command
    {
        public string Name { get; private set; }
        protected string? Help { get; set; }

        protected Command(string Name) { this.Name = Name;}
        public abstract void Execute(string[] parameters);

    }

 }
