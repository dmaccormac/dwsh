namespace dwsh
{
    internal static class Messages
    {
        public static readonly string Help =
            """
            dwsh - command shell for dameware mini remote control client
            
            Commands:
            connect     connect to a host
            install     install dwsh package
            uninstall   uninstall dwsh package
            log         show connection log
            clear       clear the console screen
            version     show version info
            exit        quit the program

            To see detailed help for a command use the -help option
            Example: connect -help

            https://github.com/dmaccormac
            """;

        public static readonly string Connect =
            """
            connect to a host using dameware mini remote control client
                    
            Options:
            -host <hostname>
            connect to a computer by hostname

            -file <filename>
            connect to a computer using a .dwc file (plaintext file containing name of host)

            -list 
            list all connections in current session

            Examples:
            connect -host foo
            connect -file c:\example\bar.dwc
            """;



        public static readonly string Log =
           """
            log - show connection log

            All connect and disconnect events are added to the log file.
            The calling shell must remain open for disconnect events to be logged. 
            """;

        public static readonly string Install =
        """
        install dwsh and create file association for .dwc files

        install [DamewareDirectory]
        If DamewareDirectory is not provided, the installer will attempt to locate the directory.
        
        
        Examples:
        install
        install "C:\Program Files\foo\bar"            
        """;

        public static readonly string Uninstall =
        """
        uninstall dwsh and remove file association for .dwc files

        uninstall [DamewareDirectory]
        If DamewareDirectory is not provided, the installer will attempt to locate the directory.
        
        
        Examples:
        uninstall
        uninstall "C:\Program Files\foo\bar"            
        """;
    }
}
