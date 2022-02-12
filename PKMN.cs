using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Utils
{
    public class PKMN
    {
        List<PKMNData> Pokemon = JsonConvert.DeserializeObject<List<PKMNData>>(ReadJsonData());

        public static string ReadJsonData()
        {
            return File.ReadAllText("PokemonData.json");
        }

        public string[] getPokemonNames(string Lang = "english")
        {
            var PokemonWithValidName = Pokemon.Where(x => x.name.ContainsKey(Lang));
            var NameList = PokemonWithValidName.Select(x => x.name[Lang]).ToArray();
            return NameList;
        }
    }

    public class PKMNData
    {
        public int id { get; set; }
        public Dictionary<string, string> name { get; set; }
        public string[] type { get; set; }
    }
}
