using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace TestRun
{
    public static partial class TestsCollection
    {
        public static bool Test3CharacterExport(IWebDriver driver)
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

            bool result;
            try
            {
                // If the data is correctly parseable...
                var jsonObject = JsonNode.Parse(data);

                // check that the values for the attributes are correct.
                result = jsonObject?["body"]?.ToString() == "2"
                    && jsonObject?["heart"]?.ToString() == "7"
                    && jsonObject?["wits"]?.ToString() == "5";
            }
            catch
            {
                result = false;
            }

            return result;
        }

    }
}
