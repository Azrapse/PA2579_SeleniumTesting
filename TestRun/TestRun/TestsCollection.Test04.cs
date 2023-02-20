using NUnit.Framework;
using OpenQA.Selenium;

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

        [Test]
        /// <summary>
        /// Test that the application correctly imports the test character, then check that the name contains "Aragorn" at the end
        /// of the process.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static void Test4CharacterImport(IWebDriver driver)
        {
            // Go to site.
            driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");

            // Let's test the import feature to determine that it loads a character's JSON.
            var loadButton = driver.AwaitForElement(By.Id("loadButton"));
            // Often, it's not enough to wait for the element to be present. You need to wait a while for it to be interactable.
            Thread.Sleep(1000);
            loadButton.Click();

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
            Assert.That(nameBox.GetAttribute("value").Contains("Aragorn"));
        }
    }
}
