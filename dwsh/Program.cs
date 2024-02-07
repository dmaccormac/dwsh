using System.Text.RegularExpressions;

namespace dwsh
{
    internal static class Program
    {
        public static void Main(String[] args)
        {
            Shell shell = new();

            if (args.Length == 0)
            {

                while (true)
                {
                    Console.Write(shell.Prompt);
                    string? input = Console.ReadLine()?.Trim();

                    if (input != null && input.Length > 0)
                    {
                        var parsedInput = SplitWithQuotes(input);
                        var command = parsedInput[0];
                        var arguments = parsedInput.Skip(1).ToArray();
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

        }

        private static string[] SplitWithQuotes(string input)
        {
            return Regex.Matches(input, @"(""|').+?(\1)|[^ ]+")
                        .Cast<Match>()
                        .Select(m => m.Value.Trim('"', '\''))
                        .ToArray();
        }
    }
}



