using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YamlDotNet.Serialization;
using System.IO;
using Newtonsoft.Json.Converters;
using System.Dynamic;

namespace LeagueRelog
{
    class settingsReaderWritter
    {
        private string _settingsPath = "C:\\Riot Games\\League of Legends\\Config\\LeagueClientSettings.yaml";
        private string _settings;
        private JObject _currentSettings;

        public settingsReaderWritter()
        {
            readFile();
            if(_settings != null)
            {
                readSettings();
            }
        }

        public string readCurrentServer()
        {
            if (_settings != null)
            {
                string currentServer = _currentSettings["install"]["globals"]["region"].ToString();
                //Console.WriteLine(_currentSettings.ToString());
                return currentServer;
            }
            else
            {
                return "Unknown";
            }

        }

        public bool updateSettings(string input)
        {
            JObject updatedSettings = _currentSettings;

            updatedSettings["install"]["globals"]["region"] = input;

            return writeSettings(updatedSettings);
        }

        private void readFile()
        {
            Console.WriteLine("Searching League client settings file..");
            try
            {
                _settings = File.ReadAllText(_settingsPath);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("League client settings file not found or corrupted...");
            }
        }

        private bool writeSettings(JObject updatedSettings)
        {
            try
            {
                var serializer = new Serializer();
                var expConverter = new ExpandoObjectConverter();
                dynamic deserializedObject = JsonConvert.DeserializeObject<ExpandoObject>(updatedSettings.ToString(), expConverter);

                string yaml = serializer.Serialize(deserializedObject);
                StreamWriter settingsFile = new StreamWriter(_settingsPath);
                settingsFile.Write(yaml);
                settingsFile.Close();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private void readSettings()
        {
            Console.WriteLine("Reading League client settings...");
            var stringReader = new StringReader(_settings);
            var deserializer = new Deserializer();
            var serializer = new JsonSerializer();
            StringWriter stringWriter = new StringWriter();

            var yamlObject = deserializer.Deserialize(stringReader);
            serializer.Serialize(stringWriter, yamlObject);
            JObject settingsJSON = JObject.Parse(stringWriter.ToString());
            _currentSettings = settingsJSON;
        }
    }
}