namespace dwsh
{
    internal static class Messages
    {
        public static readonly string Help =
            """
            dwsh - command shell for dameware mini remote control client
            
            Commands:
            connect     connect to a host
            log         show connection log
            package     install or uninstall dwsh
            clear       clear the console screen
            version     show version info
            exit        quit the program

            To see detailed help for a command use the -help option
            Example: connect -help

            https://github.com/dmaccormac
            """;

        public static readonly string Connect =
            """
            connect - connect to a host using dameware mini remote control client
                    
            Options:
            -host <hostname>
            connect to a computer by hostname

            -file <filename>
            connect to a computer using a .dwc file (plantext file containing name of host)

            -list 
            list all connections in current session

            Examples:
            connect -host foo
            connect -file bar.dwc
            """;


        public static readonly string Package =
            """
            package - install or uninstall dwsh.exe and .dwc file assocation

            Options:
            -install [DamewarePath]
            If DamewarePath parameter is not provided, the installer will attempt to locate the directory.

            -uninstall [DamewarePath]

            Examples:
            package -install
            package -uninstall "C:\Program Files\foo\bar"
            """;



        public static readonly string Log =
           """
            log - show connection log

            All connect and disconnect events are added to the log file.
            The calling shell must remain open for disconnect events to be logged. 
            """;
    }
}
