using System.Collections.Generic;

namespace JsonParser.Models
{
    public class Stash
    {
        public string AccountName { get; set; }
        public string Id { get; set; }
        public List<Item> Items { get; set; }
        public string LastCharacterName { get; set; }
        public string Name { get; set; }
        public string StashType { get; set; }
    }
}