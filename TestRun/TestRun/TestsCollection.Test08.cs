using NUnit.Framework;
using OpenQA.Selenium;

namespace TestRun
{
    public static partial class TestsCollection
    {
        [Test]
        /// <summary>
        /// Test that when rolling dice, if the "Keep the Best" option is marked, it causes two feat dice to be rolled instead
        /// of one, and that one of them is discarded.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static void Test8RollDiceKeepTheBest(IWebDriver driver)
        {
            // Go to site.
            driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");

            // Await until the "Roll twice keep best" input is present, then check it.
            var keepBestInput = driver.AwaitForElement(By.Id("rollTwiceKeepBest"));
            // Often, it's not enough to wait for the element to be present. You need to wait a while for it to be interactable.
            Thread.Sleep(1000);
            keepBestInput.Click();

            // Reuse the test code from previous tests to roll 3 success dice.
            Roll3SuccessDice(driver);

            // Count the dice results that appear. It should be 5 results: 2 d12 dice, one of them discarded, plus 3 d6 dice
            var resultContainer = driver.FindElement(By.Id("rollerResultsDiv"));
            var diceResults = resultContainer.FindElements(By.ClassName("dieDiv"));

            var d12RolledCount = diceResults
                .Count(d => d.GetAttribute("class").Contains("d12DieDiv"));
            var d12DiscardedCount = diceResults
                .Count(d => d.GetAttribute("class").Contains("d12DieDiv") && d.GetAttribute("class").Contains("discarded"));

            Assert.That(diceResults.Count == 5 && d12RolledCount == 2 && d12DiscardedCount == 1);
        }

    }
}
