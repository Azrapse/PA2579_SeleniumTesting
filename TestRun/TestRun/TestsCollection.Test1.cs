using OpenQA.Selenium;

namespace TestRun
{
    public static partial class TestsCollection
    {

        /// <summary>
        /// Test that the site loads by checking that the name box can be typed in, and you can roll dice and dice are rolled.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool Test1RollDice(IWebDriver driver)
        {
            // Go to site.
            driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");

            // Await until the dice roller is available.
            var diceRoller = driver.AwaitForElement(By.Id("rollerControlsDiv"));

            // Click the button to roll 3 dice.
            // (This would be much more simpler by using CSS selector 
            //  #rollerControlsDiv .successDiv .action[number=3]
            // but apparently, Selenium finds it too complex.            
            var button = diceRoller.FindElements(By.ClassName("action"))
                .Where(e => e.Text == "3")
                .FirstOrDefault();
            // Click the button.
            button?.Click();

            // Count the dice results that appear. It should be 4 results: the d12 die, plus 3 d6 dice
            var resultContainer = driver.FindElement(By.Id("rollerResultsDiv"));
            var diceResults = resultContainer.FindElements(By.ClassName("dieDiv"));
            Console.WriteLine(string.Join(", ", diceResults.Select(x => x.Text)));

            return diceResults.Count == 4;
        }
    }
}