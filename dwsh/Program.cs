using dwsh;

Shell shell = new();

if (args.Length == 0)
{

    while (true)
    {
        Console.Write(shell.Prompt);
        string[]? input = Console.ReadLine()?.Trim().Split();

        if (input != null) 
        {
            var command = input[0];
            var arguments = input.Skip(1).ToArray();
            shell.RunCommand(command, arguments);
        }
    }
}

else
{

    var command = args[0];
    var arguments = args.Skip(1).ToArray();
    shell.RunCommand(command, arguments);

    if (command == "connect")
    {
        Console.WriteLine(String.Join(" ", args));
        Console.Read();
    }
        

}
