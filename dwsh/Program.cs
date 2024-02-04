using dwsh;

Shell shell = new Shell();

if (args.Length == 0)
{

    while (true)
    {
        Console.Write(shell.Prompt);
        string[]? input = Console.ReadLine().Split();


        if (input.Length > 0) 
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
