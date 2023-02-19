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
        public static bool Test5UnequippingLowersEncumbrance(IWebDriver driver)
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
            firstGear?.Click();

            Thread.Sleep(100);

            var afterValue = fatigueInput?.GetAttribute("value");

            return previousValue != afterValue;
        }
    }
}
