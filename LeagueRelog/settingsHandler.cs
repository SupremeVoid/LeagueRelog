using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace LeagueRelog
{
    class settingsHandler
    {
        public string currentServer = "";
        static settingsReaderWritter settingsReaderWritter = new settingsReaderWritter();

        private string leagueClientPath = "";
        private string standardLeagueClientPath = "C:\\Riot Games\\League of Legends\\LeagueClient.exe";
        private string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public settingsHandler()
        {
            currentServer = checkCurrentServer();
            setLeagueRelogSettings();
        }

        public string checkCurrentServer()
        {
            string currentServer = settingsReaderWritter.readCurrentServer();
            return currentServer;
        }

        public void changeSettings(string input)
        {
            Console.WriteLine("Applying changes...");
            bool changed = settingsReaderWritter.updateSettings(input);
            if(changed)
            {
                Console.WriteLine("Applying changes successful");
                try
                {
                    Process.Start(leagueClientPath);
                    Environment.Exit(0);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine(Environment.NewLine + "Could not find LeagueClient.exe or its corrupted. Type 'path' to change the LeagueClient.exe path");
                }
            }
            else
            {
                Console.WriteLine(Environment.NewLine + "Applying changes was unsuccessful. A problem occured.");
            }
        }

        public void changeLeagueRelogSettings()
        {
            if (leagueRelogSettingsExist())
            {
                selectLeagueClientPath();
                string newLeagueClientPath = leagueClientPath;
                using (StreamWriter sw = new StreamWriter(myDocumentsPath + "\\LeagueRelog\\settings.ini", false))
                {
                    sw.Write(newLeagueClientPath);
                }
            }
            else
            {
                createLeagueRelogSettings();
            }
 
        }

        private bool checkLeagueStandardPath()
        {
            return File.Exists(standardLeagueClientPath) ? true : false;
        }

        private bool leagueRelogSettingsExist()
        {
            return File.Exists(myDocumentsPath + "\\LeagueRelog\\settings.ini") ? true : false;
        }

        private void setLeagueRelogSettings()
        {
            if (leagueRelogSettingsExist())
            {
                leagueClientPath = File.ReadAllText(myDocumentsPath + "\\LeagueRelog\\settings.ini");
            }
            else if (checkLeagueStandardPath())
            {
                leagueClientPath = standardLeagueClientPath;
            }
            else
            {
                createLeagueRelogSettings();
            }
        }

        private void createLeagueRelogSettings()
        {
            Console.WriteLine("Creating League Relog settings...");
            if (!Directory.Exists(myDocumentsPath + "\\LeagueRelog"))
            {
                DirectoryInfo di = Directory.CreateDirectory(myDocumentsPath + "\\LeagueRelog");
            }
            using (StreamWriter sw = new StreamWriter(myDocumentsPath + "\\LeagueRelog\\settings.ini", true))
            {
                selectLeagueClientPath();
                sw.Write(leagueClientPath);
            }
            Console.WriteLine("Creating League Relog settings successful");
        }

        private void selectLeagueClientPath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.FileName = "LeagueClient.exe";
            fileDialog.InitialDirectory = "C:\\";
            fileDialog.Filter = "(LeagueClient.exe)|LeagueClient.exe";
            fileDialog.RestoreDirectory = true;
            fileDialog.Title = "Navigate to your Riot Games League of Legends Folder";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                leagueClientPath = fileDialog.FileName;
                Console.WriteLine(Environment.NewLine + "League client path set to " + leagueClientPath);
            }
        }
    }
}