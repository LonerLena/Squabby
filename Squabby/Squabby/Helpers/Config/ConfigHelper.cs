using System.IO;
using System.Text.Json;

namespace Squabby.Helpers.Config
{
    public class ConfigHelper
    {
        /// <summary>
        /// Name of config file
        /// </summary>
        const string ConfigFile = "Config.json";

        /// <summary>
        /// Get loaded config
        /// </summary>
        public static Config GetConfig() => _config;

        /// <summary>
        /// Instance of loaded config file
        /// </summary>
        private static Config _config;

        /// <summary>
        /// Load config when needed for the first time
        /// </summary>
        /// <exception cref="FileNotFoundException">config file could not be found</exception>
        static ConfigHelper()
        {
            if(!File.Exists(ConfigFile)) throw new FileNotFoundException("Config file could not be found");
            _config = JsonSerializer.Deserialize<Config>(File.ReadAllText(ConfigFile)); 
        }
    }
}