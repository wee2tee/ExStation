using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExStation.Models;
using Newtonsoft.Json;
using System.IO;

namespace ExStation.Misc
{
    public class MessageManager
    {
        public static List<TextMessage> LoadMessage(string language_prefix = "TH")
        {
            if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"MSG_" + language_prefix + ".json"))
            {
                List<TextMessage> msg = JsonConvert.DeserializeObject<List<TextMessage>>(File.ReadAllText(@"MSG_" + language_prefix + ".json"));
                return msg;
            }

            return new List<TextMessage>();
        }
    }
}
