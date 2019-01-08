using System;

namespace LeagueRelog
{
    class consoleHandler
    {
        settingsHandler settingsHandler = new settingsHandler();

        public bool waitForCommand = true;

        public string input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
                this.handleInput(value);
            }
        }

        private string _input;

        public string checkCurrentServer()
        {
            string currentVersion = settingsHandler.currentServer;
            return currentVersion;
        }

        private void handleInput(string input)
        {
            input = input.ToLower();

            if (input == "euw" || input == "1")
            {
                changeSettings("EUW");
            }
            else if (input == "na" || input == "2")
            {
                changeSettings("NA");
            }
            else if (input == "eune" || input == "3")
            {
                changeSettings("EUNE");
            }
            else if (input == "path") 
            {
                settingsHandler.changeLeagueRelogSettings();
            }
            else if (input == "q" || input == "exit")
            {
                Environment.Exit(0);
            }
        }

        private void changeSettings(string input)
        {
            settingsHandler.changeSettings(input);
        }

    }
}