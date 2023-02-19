using OpenQA.Selenium;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRun
{
    public static partial class TestsCollection
    {
        private const string characterJsonFile = "Aragorn.json";
        private static string GetTestJson()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), characterJsonFile);
            return File.ReadAllText(path);
        }

        public static bool Test4CharacterImport(IWebDriver driver)
        {
            // Go to site.
            driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");

            // Let's test the import feature to determine that it loads a character's JSON.
            driver.AwaitForElement(By.Id("loadButton")).Click();

            var json = GetTestJson();

            // Find the textarea were to place the JSON
            var container = driver.AwaitForElement(By.Id("deserializeDiv"))
                .FindElement(By.ClassName("deserializeDataDiv"));
            container.Clear();
            container.SendKeys(json);

            // Click next
            driver.AwaitForElement(By.Id("deserializeNextButton")).Click();

            // Check that the name of the character contains "Aragorn".
            var nameBox = driver.AwaitForElement(By.Id("nameInput"));
            return nameBox.GetAttribute("value").Contains("Aragorn");
        }
    }
}
