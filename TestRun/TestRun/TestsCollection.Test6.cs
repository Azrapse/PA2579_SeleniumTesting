using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRun
{
    public static partial class TestsCollection
    {
        public static bool Test6OnlineCharacterServer(IWebDriver driver)
        {
            // Go to site.
            driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");

            // Click the online character server button
            var characterServerButton = driver.AwaitForElement(By.Id("onlineButton"));
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
                    return false;
                }
                Thread.Sleep(1000);
            }

            return true;
        }
    }
}
