using NUnit.Framework;
using OpenQA.Selenium;

namespace TestRun
{
    public static partial class TestsCollection
    {
        [Test]
        /// <summary>
        /// Import "Aragorn" again, then test that the character is suffering a certain amount of fatigue due to the weight of
        /// his gear. Then unequip his long sword, and check that he is not suffering the same amount of fatigue after that.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static void Test5UnequippingLowersEncumbrance(IWebDriver driver)
        {
            // Go to site.
            driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");

            // Let's use Test4 as foundation for this test. We import the Aragorn character.
            Test4CharacterImport(driver);

            // We test now that unequipping Aragorn's long sword correctly lowers the fatigue amount due to encumbrance.
            var fatigueInput = driver.FindElements(By.ClassName("subRoundStatBox"))
                .Where(e => e.GetAttribute("stat") == "fatigue")
                .FirstOrDefault()
                ?.FindElement(By.TagName("input"));

            var previousValue = fatigueInput?.GetAttribute("value");

            // Get the first gear slot, the sword, and uncheck it.
            var firstGear = driver.AwaitForElement(By.Id("weaponGearTable"))
                .FindElements(By.ClassName("weaponGearCarriedStatus"))
                .FirstOrDefault();
            // This click unequips the sword.
            firstGear?.Click();
            // It might take a while for the javascript to recalculate the fatigue.
            Thread.Sleep(1000);

            var afterValue = fatigueInput?.GetAttribute("value");

            Assert.That(previousValue != afterValue);
        }
    }
}
