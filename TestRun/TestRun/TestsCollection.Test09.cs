using NUnit.Framework;
using OpenQA.Selenium;

namespace TestRun
{
    public static partial class TestsCollection
    {
        [Test]
        /// <summary>
        /// Test that when rolling dice, if the character is weary (the Weary option is marked), it causes success dice with results
        /// of 1 to 3 to be, instead, 0.
        /// It might require several rolls to obtain 1-3 values in the d6 dice. So we will repeat this test up to 10 times, always checking
        /// that no d6 dice are having 1-3 values, but either 0 or 4, 5, or the rune for 6.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static void Test9RollDiceWhileBeingWeary(IWebDriver driver)
        {
            // Go to site.
            driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");

            // Await until the "Weary" input is present, then check it.
            var wearyInput = driver.AwaitForElement(By.Id("rollerWearyCheckbox"));
            // Often, it's not enough to wait for the element to be present. You need to wait a while for it to be interactable.
            Thread.Sleep(1000);
            wearyInput.Click();

            // We will perform 10 roll tests, to increase the chance that any of the d6 (success dice) roll a value 1-3.
            var checksOut = true;
            for (var i = 0; i < 10; i++)
            {
                // Reuse the test code from previous tests to roll 3 success dice.            
                Roll3SuccessDice(driver);

                // Count the dice results that appear. It should be 4 results: one d12 dice, plus 3 d6 dice, with values 1-3 replaced with 0s
                var resultContainer = driver.FindElement(By.Id("rollerResultsDiv"));
                var diceResults = resultContainer.FindElements(By.ClassName("d6DieDiv"));

                var d6WithValues123Rolled = diceResults
                    .Count(d => d.Text == "1" || d.Text == "2" || d.Text == "3");

                if (d6WithValues123Rolled > 0)
                {
                    checksOut = false;
                    break;
                }
            }
            Assert.That(checksOut, Is.True);
        }

    }
}
