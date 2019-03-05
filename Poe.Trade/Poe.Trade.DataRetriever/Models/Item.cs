using System.Collections.Generic;

namespace JsonParser.Models
{
    public class Item
    {
        public List<Property> AdditionalProperties { get; set; }
        public string ArtFilename { get; set; }
        public Category Category { get; set; }
        public string Colour { get; set; }
        public bool Corrupted { get; set; }
        public List<string> CraftedMods { get; set; }
        public string DescrText { get; set; }
        public bool Duplicated { get; set; }
        public List<string> EnchantMods { get; set; }
        public List<string> ExplicitMods { get; set; }
        public List<string> FlavourText { get; set; }
        public int FrameType { get; set; }
        public int H { get; set; }
        public string Icon { get; set; }
        public string Id { get; set; }
        public bool Identified { get; set; }
        public int Ilvl { get; set; }
        public List<string> ImplicitMods { get; set; }
        public string InventoryId { get; set; }
        public string League { get; set; }
        public int MaxStackSize { get; set; }
        public string Name { get; set; }
        public List<Property> NextLevelRequirements { get; set; }
        public string Note { get; set; }
        public List<Property> Properties { get; set; }
        public string ProphecyText { get; set; }
        public List<Property> Requirements { get; set; }
        public string SecDescrText { get; set; }
        public int Socket { get; set; }
        public List<Socket> Sockets { get; set; }
        public int StackSize { get; set; }
        public bool Support { get; set; }
        public int TalismanTier { get; set; }
        public List<string> UtilityMods { get; set; }
        public bool Verified { get; set; }
        public int W { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}