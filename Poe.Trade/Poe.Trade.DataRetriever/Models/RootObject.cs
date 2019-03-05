using System.Collections.Generic;

namespace JsonParser.Models
{
    public class RootObject
    {
        public string NextChangeId { get; set; }
        public List<Stash> Stashes { get; set; }
    }
}