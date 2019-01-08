using System;

namespace LeagueRelog
{
    class Program
    {
        static string leagueRelog = @"
 _                                   ____      _                ___   _ 
| |    ___  __ _  __ _ _   _  ___   |  _ \ ___| | ___   __ _   / _ \ / |
| |   / _ \/ _` |/ _` | | | |/ _ \  | |_) / _ \ |/ _ \ / _` | | | | || |
| |__|  __/ (_| | (_| | |_| |  __/  |  _ <  __/ | (_) | (_| | | |_| || |
|_____\___|\__,_|\__, |\__,_|\___|  |_| \_\___|_|\___/ \__, |  \___(_)_|
                 |___/                                |___/     
";
        [STAThread]
        static void Main(string[] args)
        {
            Console.Write(leagueRelog);
            Console.WriteLine("By SupremeVoid");
            Console.WriteLine("");
            consoleHandler consoleHandler = new consoleHandler();
            Console.WriteLine("");
            Console.WriteLine("Current Server: " + consoleHandler.checkCurrentServer());
            Console.WriteLine("");
            if(consoleHandler.checkCurrentServer() != "Unknown")
            {
                Console.WriteLine("Type 'path' if you want to change the path of your LeagueClient.exe");
                Console.WriteLine("");
                Console.WriteLine("Choose a server you want to log in to");
                Console.WriteLine("1. EUW");
                Console.WriteLine("2. NA");
                Console.WriteLine("3. EUNE");
                Console.WriteLine("");

                while (consoleHandler.waitForCommand)
                {
                    consoleHandler.input = Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Check the errors above and your league installation. Contact me if you need any help");
            }
            Console.ReadLine();
        }
    }
}
