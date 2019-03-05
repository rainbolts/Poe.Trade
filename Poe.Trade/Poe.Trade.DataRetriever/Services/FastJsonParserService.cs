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
        private delegate T ParseArrayItemAction<out T>(ref Utf8JsonReader reader);

        private delegate void ParsePropertyAction<in T>(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName,
            T parseProperty);

        public RootObject Parse(string json)
        {
            ReadOnlySpan<byte> jsonUtf8 = Encoding.UTF8.GetBytes(json);
            var reader = new Utf8JsonReader(jsonUtf8, true, default);
            reader.Read();
            return ParseObject<RootObject>(ref reader, ParseRootObjectProperty);
        }

        private static void ParseRootObjectProperty(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName,
            RootObject root)
        {
            if (propertyName.SequenceEqual(PropertyNameBytes.BytesNextChangeId))
            {
                reader.Read();
                root.NextChangeId = reader.GetString();
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesStashes))
            {
                root.Stashes = ParseNamedArray(ref reader, ParseStash);
            }
            else
            {
                Skip(ref reader);
            }
        }

        private static Stash ParseStash(ref Utf8JsonReader reader)
        {
            return ParseObject<Stash>(ref reader, ParseStashProperty);
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
                stash.Items = ParseNamedArray(ref reader, ParseItem);
            }
            else
            {
                Skip(ref reader);
            }
        }

        private static Item ParseItem(ref Utf8JsonReader reader)
        {
            return ParseObject<Item>(ref reader, ParseItemProperty);
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
                item.Sockets = ParseNamedArray(ref reader, ParseSocket);
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
                item.Properties = ParseNamedArray(ref reader, ParseProperty);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesAdditionalProperties))
            {
                item.AdditionalProperties = ParseNamedArray(ref reader, ParseProperty);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesRequirements))
            {
                item.Requirements = ParseNamedArray(ref reader, ParseProperty);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesNextLevelRequirements))
            {
                item.NextLevelRequirements = ParseNamedArray(ref reader, ParseProperty);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesExplicitMods))
            {
                item.ExplicitMods = ParseNamedArray(ref reader, ParseStringOrNumber);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesImplicitMods))
            {
                item.ImplicitMods = ParseNamedArray(ref reader, ParseStringOrNumber);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesUtilityMods))
            {
                item.UtilityMods = ParseNamedArray(ref reader, ParseStringOrNumber);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesCraftedMods))
            {
                item.CraftedMods = ParseNamedArray(ref reader, ParseStringOrNumber);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesEnchantMods))
            {
                item.EnchantMods = ParseNamedArray(ref reader, ParseStringOrNumber);
            }
            else if (propertyName.SequenceEqual(PropertyNameBytes.BytesFlavourText))
            {
                item.FlavourText = ParseNamedArray(ref reader, ParseStringOrNumber);
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

        private static Socket ParseSocket(ref Utf8JsonReader reader)
        {
            return ParseObject<Socket>(ref reader, ParseSocketProperty);
        }

        private static void ParseSocketProperty(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName,
            Socket socket)
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

        private static Property ParseProperty(ref Utf8JsonReader reader)
        {
            return ParseObject<Property>(ref reader, ParsePropertyProperty);
        }

        private static void ParsePropertyProperty(ref Utf8JsonReader reader, ReadOnlySpan<byte> propertyName,
            Property property)
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
                property.Values = ParseNamedArray(ref reader, ParseStringArray);
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

        private static List<string> ParseStringArray(ref Utf8JsonReader reader)
        {
            var results = new List<string>();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                results.Add(ParseStringOrNumber(ref reader));
            }

            return results;
        }

        private static string ParseStringOrNumber(ref Utf8JsonReader reader)
        {
            return reader.TokenType == JsonTokenType.String
                ? reader.GetString()
                : reader.GetInt32().ToString();
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
                category.Values = ParseUnnamedArray(ref reader, ParseStringOrNumber);
            }

            return category;
        }

        private static List<T> ParseNamedArray<T>(ref Utf8JsonReader reader, ParseArrayItemAction<T> parseArrayItem)
        {
            reader.Read();
            return ParseUnnamedArray(ref reader, parseArrayItem);
        }

        private static List<T> ParseUnnamedArray<T>(ref Utf8JsonReader reader, ParseArrayItemAction<T> parseArrayItem)
        {
            var results = new List<T>();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                results.Add(parseArrayItem(ref reader));
            }

            return results;
        }

        private static T ParseObject<T>(ref Utf8JsonReader reader, ParsePropertyAction<T> parseProperty) where T : new()
        {
            var obj = new T();

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                var propertyName = reader.ValueSpan;
                parseProperty(ref reader, propertyName, obj);
            }

            return obj;
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
                while (reader.Read() && depth <= reader.CurrentDepth)
                {
                }
            }
        }
    }
}
