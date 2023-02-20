using NUnit.Framework;
using OpenQA.Selenium;

namespace TestRun
{
    public static partial class TestsCollection
    {
        [Test]
        /// <summary>
        /// Test that the site lets you roll dice and dice are rolled. In particular, that when you press the number 3 button,
        /// it causes 4 dice to be rolled (the feat die, and 3 success dice).
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static void Test7RollDice(IWebDriver driver)
        {
            // Go to site.
            driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");

            Roll3SuccessDice(driver);
            // Count the dice results that appear. It should be 4 results: the d12 die, plus 3 d6 dice
            var resultContainer = driver.FindElement(By.Id("rollerResultsDiv"));
            var diceResults = resultContainer.FindElements(By.ClassName("dieDiv"));

            Assert.That(diceResults.Count == 4);
        }

        private static void Roll3SuccessDice(IWebDriver driver)
        {
            // Await until the dice roller is available.
            var diceRoller = driver.AwaitForElement(By.Id("rollerControlsDiv"));

            // Click the button to roll 3 dice.
            // (This would be much more simpler by using CSS selector 
            //  #rollerControlsDiv .successDiv .action[number=3]
            // but apparently, Selenium finds it too complex.            
            var button = diceRoller.FindElements(By.ClassName("action"))
                .Where(e => e.Text == "3")
                .FirstOrDefault();
            // Often, it's not enough to wait for the element to be present. You need to wait a while for it to be interactable.
            Thread.Sleep(1000);
            // Click the button.
            button?.Click();

            // Let's wait a bit for the javascript to do the calculations.
            Thread.Sleep(500);
        }
    }
}