using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lexicon_Slutuppgift.Core.Collections;

namespace Lexicon_Slutuppgift.Core
{
    public class MembersHandler : ItemHandler<Member>
    {   
        private int memberCount = 0;
        private Dictionary<string, int> Config;
        public MembersHandler(string inputString) : base(inputString)
        {
            try
            {
                if (File.Exists($"memberConfig.json"))
                {
                    Config = JsonSerializer.Deserialize<Dictionary<string, int>>(File.ReadAllText($"memberConfig.json"));
                    memberCount = Config["membersQuantity"];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading the catalog: {ex.Message}");
            }
        }

        public override bool Add(Member newItem)
        {
            if (newItem.Name == null) return false;
            if (newItem.PhoneNumber == null) return false;
            if (newItem.Address == null) return false;
            try
            {
                
                memberCount++;
                newItem.IdNr = memberCount.ToString("D7");
                Catalog.Add(newItem);
                PushConfig(memberCount);
                PushCatalogToMain();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Adding the book: {ex.Message}");
                return false;
            }
        }

        public void PushConfig(int inputInt)
        {
            Dictionary<string, int> config = new();
            config.Add("membersQuantity", inputInt);
            Config = config;
            File.WriteAllText($"memberConfig.json", JsonSerializer.Serialize(Config));
        } 

    }
}
