using System.Collections.Generic;

namespace JsonParser.Models
{
    public class Property
    {
        public int DisplayMode { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public List<List<string>> Values { get; set; }
    }
}