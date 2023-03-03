using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace console_snale
{
    internal class ReadGameFile
    {
        public GameInfo ReadSettingFile()
        {
            GameInfo? gameInfo;
            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\gameInfo.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string str = reader.ReadToEnd();                
                gameInfo = JsonSerializer.Deserialize<GameInfo>(str);

            }
            return gameInfo;
        }
        public void WriteSettingFile(GameInfo gameInfo)
        {
            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\gameInfo.txt";            
            string settingString = JsonSerializer.Serialize(gameInfo);
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(settingString);
            }
        }
    }
}
