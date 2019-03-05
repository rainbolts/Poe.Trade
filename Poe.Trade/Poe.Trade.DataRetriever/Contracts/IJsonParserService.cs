using JsonParser.Models;

namespace JsonParser.Contracts
{
    public interface IJsonParserService
    {
        RootObject Parse(string json);
    }
}