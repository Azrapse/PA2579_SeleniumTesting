using NUnit.Framework;
using OpenQA.Selenium;
using System.Text.Json.Nodes;

namespace TestRun
{
    public static partial class TestsCollection
    {
        [Test]
        /// <summary>
        /// Test that the hobbit created in a previous test can be exported as a JSON string for the user's keeping.
        /// Then parse that JSON data and correctly find values for the body, heart, and wits attributes within it.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static void Test3CharacterExport(IWebDriver driver)
        {
            // Go to site.
            driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");

            // Let's use the character creation wizard to create a Took-blood hobbit which specializes in short swords, travel and lore
            CreateTookHobbitCharacter(driver);

            // Let's test the export feature to determine that it generates valid JSON.
            driver.AwaitForElement(By.Id("saveButton")).Click();

            // Get the data in the window that appears.
            var data = driver.AwaitForElement(By.Id("serializeDiv"))
                .FindElement(By.ClassName("serializeDataDiv"))
                .Text;

            // If the data is correctly parseable...
            var jsonObject = JsonNode.Parse(data);

            // check that the values for the attributes are correct.
            Assert.That(jsonObject?["body"]?.ToString() == "2");
            Assert.That(jsonObject?["heart"]?.ToString() == "7");
            Assert.That(jsonObject?["wits"]?.ToString() == "5");
        }

    }
}
