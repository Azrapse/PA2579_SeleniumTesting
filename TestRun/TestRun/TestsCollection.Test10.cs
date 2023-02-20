using NUnit.Framework;
using OpenQA.Selenium;

namespace TestRun
{
    public static partial class TestsCollection
    {
        [Test]
        /// Test that when rolling dice, if the character is weary (the Weary option is marked), it causes success dice with results
        /// of 1 to 3 to be, instead, 0.
        /// It might require several rolls to obtain 1-3 values in the d6 dice. So we will repeat this test up to 10 times, always checking
        /// that no d6 dice are having 1-3 values, but either 0 or 4, 5, or the rune for 6.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static void Test10RollDiceWithMastery(IWebDriver driver)
        {
            // Go to site.
            driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");

            // Await until the "Mastery" dropdown is present, then select 3.
            var materyDiceDD = driver.AwaitForElement(By.Id("rollerMasteryDiceCount"));
            // Often, it's not enough to wait for the element to be present. You need to wait a while for it to be interactable.
            Thread.Sleep(1000);
            // Press "Arrow down" three times to select 3 masteries.
            materyDiceDD.SendKeys(Keys.Down);
            materyDiceDD.SendKeys(Keys.Down);
            materyDiceDD.SendKeys(Keys.Down);

            // Reuse the test code from previous tests to roll 3 success dice.            
            Roll3SuccessDice(driver);

            // Count the dice results that appear. It should be 7 results: one d12 dice, plus 6 d6 dice, with 3 of them discarded.
            var resultContainer = driver.FindElement(By.Id("rollerResultsDiv"));
            var diceResults = resultContainer.FindElements(By.ClassName("dieDiv"));

            var d6Discarded = diceResults
                .Count(d => d.GetAttribute("class").Contains("discarded"));

            Assert.That(diceResults.Count == 7 && d6Discarded == 3);
        }

    }
}
