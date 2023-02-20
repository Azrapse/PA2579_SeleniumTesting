using NUnit.Framework;
using OpenQA.Selenium;

namespace TestRun
{
    public static partial class TestsCollection
    {
        [Test]
        /// <summary>
        /// Test the Online Character Server feature by opening it, then loading all public characters, then loading one of them at random.
        /// The test is considered passed if the "Culture" input in the character sheet changes to anything at all.
        /// (Even an invalid character will fill up the Culture input with some "undefined" text, which is not an error.)
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static void Test6OnlineCharacterServer(IWebDriver driver)
        {
            // Go to site.
            driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");

            // Click the online character server button
            var characterServerButton = driver.AwaitForElement(By.Id("onlineButton"));
            // Often, it's not enough to wait for the element to be present. You need to wait a while for it to be interactable.            
            Thread.Sleep(1000);
            characterServerButton.Click();

            // Click the button to list all public uploaded characters
            var publicCharactersButton = driver.AwaitForElement(By.Id("onlineListPublicButton"));
            publicCharactersButton.Click();

            // Wait for characters to load, then click any one of them.
            driver.AwaitForElement(By.ClassName("characterLoadButton"));
            var characterButtons = driver.AwaitForElement(By.Id("onlineCharacterList"))
                .FindElements(By.ClassName("characterLoadButton"))
                .ToList();
            // Click one of those buttons at random
            characterButtons[Random.Shared.Next(characterButtons.Count)].Click();

            // Wait for a while for it to load. We know it has loaded when the character's culture input has some value.
            var cultureInput = driver.FindElement(By.Id("cultureInput"));
            var attempts = 5;
            while (string.IsNullOrWhiteSpace(cultureInput.Text))
            {
                attempts--;
                if (attempts < 0)
                {
                    break;
                }
                Thread.Sleep(1000);
            }
            Assert.That(!string.IsNullOrWhiteSpace(cultureInput.Text));
        }
    }
}
