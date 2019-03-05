using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using JsonParser.Contracts;
using JsonParser.Models;

namespace JsonParser.Services
{
    public class FastJsonParserService : IJsonParserService
    {
        public RootObject Parse(string json)
        {
            ReadOnlySpan<byte> jsonUtf8 = Encoding.UTF8.GetBytes(json);
            var reader = new Utf8JsonReader(jsonUtf8, true, default);
            return ParseRootObject(ref reader);
        }

        private static RootObject ParseRootObject(ref Utf8JsonReader reader)
        {
            var root = new RootObject();

            reader.Read();
            while (reader.Read()
                   && reader.TokenType != JsonTokenType.EndObject)
            {
                var rootPropertyName = reader.ValueSpan;
                reader.Read();
                ParseRootObjectProperty(ref reader, rootPropertyName, root);
            }
            return root;
        }

        private static void ParseRootObjectProperty(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, RootObject root)
        {
            if (propertyName.SequenceEqual(PropertyNameBytes.BytesNextChangeId))
            {
                root.NextChangeId = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesStashes))
            {
                root.Stashes = ParseStashArray(ref reader);
            }
            else
            {
                Skip(ref reader);
            }
        }

        private static List<Stash> ParseStashArray(ref Utf8JsonReader reader)
        {
            var stashes = new List<Stash>();
            while (reader.Read()
                   && reader.TokenType != JsonTokenType.EndArray)
            {
                stashes.Add(ParseStash(ref reader));
            }

            return stashes;
        }

        private static Stash ParseStash(ref Utf8JsonReader reader)
        {
            var stash = new Stash();
            while (reader.Read()
                   && reader.TokenType != JsonTokenType.EndObject)
            {
                var stashPropertyName = reader.ValueSpan;
                ParseStashProperty(ref reader, stashPropertyName, stash);
            }
            return stash;
        }

        private static void ParseStashProperty(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, Stash stash)
        {
            if (propertyName.SequenceEqual(PropertyNameBytes.BytesAccountName))
            {
                reader.Read();
                if (reader.TokenType == JsonTokenType.String)
                    stash.AccountName = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesLastCharacterName))
            {
                reader.Read();
                if (reader.TokenType == JsonTokenType.String)
                    stash.LastCharacterName = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesId))
            {
                reader.Read();
                stash.Id = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesStash))
            {
                reader.Read();
                if (reader.TokenType == JsonTokenType.String)
                    stash.Name = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesStashType))
            {
                reader.Read();
                stash.StashType = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesItems))
            {
                stash.Items = ParseItemArray(ref reader);
            }
            else
            {
                Skip(ref reader);
            }
        }

        private static List<Item> ParseItemArray(ref Utf8JsonReader reader)
        {
            reader.Read();
            var results = new List<Item>();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                results.Add(ParseItem(ref reader));
            }
            return results;
        }

        private static Item ParseItem(ref Utf8JsonReader reader)
        {
            var item = new Item();
            while (reader.Read()
                   && reader.TokenType != JsonTokenType.EndObject)
            {
                var itemPropertyName = reader.ValueSpan;
                ParseItemProperty(ref reader, itemPropertyName, item);
            }
            return item;
        }

        private static void ParseItemProperty(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, Item item)
        {
            if (propertyName.SequenceEqual(PropertyNameBytes.BytesVerified))
            {
                reader.Read();
                item.Verified = reader.GetBoolean();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesW))
            {
                reader.Read();
                item.W = reader.GetInt32();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesH))
            {
                reader.Read();
                item.H = reader.GetInt32();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesIlvl))
            {
                reader.Read();
                item.Ilvl = reader.GetInt32();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesTalismanTier))
            {
                reader.Read();
                item.TalismanTier = reader.GetInt32();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesCorrupted))
            {
                reader.Read();
                item.Corrupted = reader.GetBoolean();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesDuplicated))
            {
                reader.Read();
                item.Duplicated = reader.GetBoolean();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesStackSize))
            {
                reader.Read();
                item.StackSize = reader.GetInt32();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesMaxStackSize))
            {
                reader.Read();
                item.MaxStackSize = reader.GetInt32();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesIcon))
            {
                reader.Read();
                item.Icon = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesArtFilename))
            {
                reader.Read();
                item.ArtFilename = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesLeague))
            {
                reader.Read();
                item.League = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesId))
            {
                reader.Read();
                item.Id = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesSockets))
            {
                item.Sockets = ParseSocketArray(ref reader);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesName))
            {
                reader.Read();
                item.Name = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesSecDescrText))
            {
                reader.Read();
                item.SecDescrText = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesDescrText))
            {
                reader.Read();
                item.DescrText = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesIdentified))
            {
                reader.Read();
                item.Identified = reader.GetBoolean();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesNote))
            {
                reader.Read();
                item.Note = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesProperties))
            {
                item.Properties = ParsePropertyArray(ref reader);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesAdditionalProperties))
            {
                item.AdditionalProperties = ParsePropertyArray(ref reader);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesRequirements))
            {
                item.Requirements = ParsePropertyArray(ref reader);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesNextLevelRequirements))
            {
                item.NextLevelRequirements = ParsePropertyArray(ref reader);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesExplicitMods))
            {
                reader.Read();
                item.ExplicitMods = ParseStringArray(ref reader);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesImplicitMods))
            {
                reader.Read();
                item.ImplicitMods = ParseStringArray(ref reader);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesUtilityMods))
            {
                reader.Read();
                item.UtilityMods = ParseStringArray(ref reader);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesCraftedMods))
            {
                reader.Read();
                item.CraftedMods = ParseStringArray(ref reader);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesEnchantMods))
            {
                reader.Read();
                item.EnchantMods = ParseStringArray(ref reader);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesFlavourText))
            {
                reader.Read();
                item.FlavourText = ParseStringArray(ref reader);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesProphecyText))
            {
                reader.Read();
                item.ProphecyText = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesFrameType))
            {
                reader.Read();
                item.FrameType = reader.GetInt32();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesCategory))
            {
                item.Category = ParseCategory(ref reader);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesX))
            {
                reader.Read();
                item.X = reader.GetInt32();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesY))
            {
                reader.Read();
                item.Y = reader.GetInt32();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesInventoryId))
            {
                reader.Read();
                item.InventoryId = reader.GetString();
            }
            else
            {
                Skip(ref reader);
            }
        }

        private static List<Socket> ParseSocketArray(ref Utf8JsonReader reader)
        {
            reader.Read();
            var results = new List<Socket>();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                results.Add(ParseSocket(ref reader));
            }
            return results;
        }

        private static Socket ParseSocket(ref Utf8JsonReader reader)
        {
            var socket = new Socket();
            while (reader.Read()
                   && reader.TokenType != JsonTokenType.EndObject)
            {
                var propertyName = reader.ValueSpan;
                ParseSocketProperty(ref reader, propertyName, socket);
            }
            return socket;
        }

        private static void ParseSocketProperty(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, Socket socket)
        {
            if (propertyName.SequenceEqual(PropertyNameBytes.BytesAttr))
            {
                reader.Read();
                socket.Attr = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesGroup))
            {
                reader.Read();
                socket.Group = reader.GetInt32();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesSColour))
            {
                reader.Read();
                socket.SColour = reader.GetString();
            }
            else
            {
                Skip(ref reader);
            }
        }

        private static List<Property> ParsePropertyArray(ref Utf8JsonReader reader)
        {
            reader.Read();
            var results = new List<Property>();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                results.Add(ParseProperty(ref reader));
            }
            return results;
        }

        private static Property ParseProperty(ref Utf8JsonReader reader)
        {
            var property = new Property();
            while (reader.Read()
                   && reader.TokenType != JsonTokenType.EndObject)
            {
                var propertyName = reader.ValueSpan;
                ParsePropertyProperty(ref reader, propertyName, property);
            }
            return property;
        }

        private static void ParsePropertyProperty(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName, Property property)
        {
            if (propertyName.SequenceEqual(PropertyNameBytes.BytesDisplayMode))
            {
                reader.Read();
                property.DisplayMode = reader.GetInt32();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesName))
            {
                reader.Read();
                property.Name = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesValues))
            {
                property.Values = ParseStringArrayArray(ref reader);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesType))
            {
                reader.Read();
                property.Type = reader.GetInt32();
            }
            else
            {
                Skip(ref reader);
            }
        }

        private static List<List<string>> ParseStringArrayArray(ref Utf8JsonReader reader)
        {
            reader.Read();
            var results = new List<List<string>>();
            while (reader.Read()
                   && reader.TokenType != JsonTokenType.EndArray)
            {
                results.Add(ParseStringArray(ref reader));
            }
            return results;
        }

        private static List<string> ParseStringArray(ref Utf8JsonReader reader)
        {
            var results = new List<string>();
            reader.Read();
            while (reader.TokenType != JsonTokenType.EndArray)
            {
                var result = reader.TokenType == JsonTokenType.String
                    ? reader.GetString()
                    : reader.GetInt32().ToString();
                results.Add(result);
                reader.Read();
            }
            return results;
        }

        private static Category ParseCategory(ref Utf8JsonReader reader)
        {
            var category = new Category();
            while (reader.Read()
                   && reader.TokenType != JsonTokenType.EndObject)
            {
                reader.Read();
                category.Name = reader.GetString();
                reader.Read();
                category.Values = ParseStringArray(ref reader);
            }
            return category;
        }

        private static void Skip(ref Utf8JsonReader reader)
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                reader.Read();
            }

            if (reader.TokenType == JsonTokenType.StartObject
                || reader.TokenType == JsonTokenType.StartArray)
            {
                var depth = reader.CurrentDepth;
                while (reader.Read() && depth <= reader.CurrentDepth) { }
            }
        }
    }
}
