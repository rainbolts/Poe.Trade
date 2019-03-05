using System.IO;
using System.Linq;
using JsonParser.Models;
using JsonParser.Services;
using NUnit.Framework;

namespace JsonParser.Tests
{
    [TestFixture]
    public class JsonParserServiceTests
    {
        [Test]
        public void TestParse_ProducesCorrectValues2()
        {
            // Arrange
            var json = File.ReadAllText("TestFiles\\small.json");
            var sut = new FastJsonParserService();

            // Act
            var actual = sut.Parse(json);

            // Assert
            Assert.AreEqual("2653-4457-4108-4817-1510", actual.NextChangeId);
            Assert.AreEqual(1, actual.Stashes.Count);
            AssertStash0(actual.Stashes[0]);
        }

        private static void AssertStash0(Stash stash)
        {
            Assert.AreEqual("6e744b0f76179835e1f681ce81c513ea190cb021b34eaacafe4c3d4f6990395f", stash.Id);
            Assert.AreEqual("5a4oK", stash.AccountName);
            Assert.AreEqual("Please_remove_volotile", stash.LastCharacterName);
            Assert.AreEqual("What i need", stash.Name);
            Assert.AreEqual("PremiumStash", stash.StashType);
            Assert.AreEqual(2, stash.Items.Count);
            AssertStash0Item0(stash.Items[0]);
        }

        private static void AssertStash0Item0(Item item)
        {
            Assert.AreEqual(false, item.Verified);
            Assert.AreEqual(2, item.W);
            Assert.AreEqual(4, item.H);
            Assert.AreEqual(71, item.Ilvl);
            Assert.AreEqual("http://web.poecdn.com/image/Art/2DItems/Weapons/TwoHandWeapons/Bows/SarkhamsReach.png?scale=1&scaleIndex=0&w=2&h=4&v=f333c2e4005ee20a84270731baa5fa6a3", item.Icon);
            Assert.AreEqual("Hardcore", item.League);
            Assert.AreEqual("176b5e6f7af0a5bb4b48d7fdafa47501a179f4ea095815a58c82c4b5244b3cdb", item.Id);
            Assert.AreEqual(1, item.Sockets.Count);
            AssertStash0Item0Sockets0(item.Sockets[0]);
            Assert.AreEqual("<<set:MS>><<set:M>><<set:S>>Roth's Reach", item.Name);
            Assert.AreEqual(true, item.Identified);
            Assert.AreEqual("~price 10 exa", item.Note);
            Assert.AreEqual(2, item.Properties.Count);
            AssertStash0Item0Properties0(item.Properties.FirstOrDefault(p => p.Name.Equals("Bow")));
            AssertStash0Item0Properties1(item.Properties.FirstOrDefault(p => p.Name.Equals("Attacks per Second")));
            Assert.AreEqual(2, item.Requirements.Count);
            AssertStash0Item0Requirements0(item.Requirements[0]);
            Assert.AreEqual(5, item.ExplicitMods.Count);
            Assert.NotNull(item.ExplicitMods.FirstOrDefault(e => e.Equals("68% increased Physical Damage")));
            Assert.AreEqual(3, item.FlavourText.Count);
            Assert.NotNull(item.FlavourText.FirstOrDefault(e => e.Equals("\"Exiled to the sea; what a joke. \r")));
            Assert.AreEqual(3, item.FrameType);
            AssertStash0Item0Category(item.Category);
            Assert.AreEqual(10, item.X);
            Assert.AreEqual(0, item.Y);
            Assert.AreEqual("Stash1", item.InventoryId);
        }

        private static void AssertStash0Item0Sockets0(Socket socket)
        {
            Assert.AreEqual(0, socket.Group);
            Assert.AreEqual("D", socket.Attr);
            Assert.AreEqual("G", socket.SColour);
        }

        private static void AssertStash0Item0Properties0(Property property)
        {
            CollectionAssert.IsEmpty(property.Values);
            Assert.AreEqual(0, property.DisplayMode);
        }

        private static void AssertStash0Item0Properties1(Property property)
        {
            Assert.AreEqual("1.31", property.Values[0][0]);
            Assert.AreEqual("1", property.Values[0][1]);
            Assert.AreEqual(0, property.DisplayMode);
        }

        private static void AssertStash0Item0Requirements0(Property requirement)
        {
            Assert.AreEqual("Level", requirement.Name);
            Assert.AreEqual(1, requirement.Values.Count);
            Assert.AreEqual(2, requirement.Values[0].Count);
            Assert.AreEqual("18", requirement.Values[0][0]);
            Assert.AreEqual("0", requirement.Values[0][1]);
            Assert.AreEqual(0, requirement.DisplayMode);
        }

        private static void AssertStash0Item0Category(Category category)
        {
            Assert.AreEqual("weapons", category.Name);
            Assert.AreEqual(1, category.Values.Count);
            Assert.AreEqual("bow", category.Values[0]);
        }
    }
}
