using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace TestRun
{
    public static partial class TestsCollection
    {
        public static bool Test2CharacterCreation(IWebDriver driver)
        {
            // Go to site.
            driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");

            // Let's use the character creation wizard to create a Took-blood hobbit which specializes in short swords, travel and lore
            CreateTookHobbitCharacter(driver);

            // Check that values for Body, Heart, and Wits are correct for this creation
            var bodyScore = driver.AwaitForElement(By.Id("bodyScoreInput"));
            var heartScore = driver.AwaitForElement(By.Id("heartScoreInput"));
            var witsScore = driver.AwaitForElement(By.Id("witsScoreInput"));
            var result = bodyScore.GetAttribute("value") == "2"
                && heartScore.GetAttribute("value") == "7"
                && witsScore.GetAttribute("value") == "5";
            return result;
        }

        private static void CreateTookHobbitCharacter(IWebDriver driver)
        {
            // Click the button to start the Create New Character tutorial
            var startButton = driver.AwaitForElement(By.Id("startButton"));
            startButton?.Click();

            // Click the button to select the Shire Hobbit race.
            driver.AwaitForElement(By.Id("hobbitSelectionButton")).Click();

            // Click next
            driver.AwaitForElement(By.Id("cultureNextButton")).Click();

            // Select first weapon package
            driver.AwaitForElement(By.Id("weaponSkillsPackage0")).Click();

            // Click next
            driver.AwaitForElement(By.Id("weaponSkillsPackageNextButton")).Click();

            // Select Cooking and Gardener specialties
            driver.AwaitForElement(By.CssSelector("[specialty=cooking]")).Click();
            driver.AwaitForElement(By.CssSelector("[specialty=gardener]")).Click();
            // Click next
            driver.AwaitForElement(By.Id("SpecialtiesNextButton")).Click();

            // Select Took Blood background
            var bg = driver.AwaitForElement(By.Id("background6"));
            // Within that background, select the first two features
            var features = bg.FindElements(By.ClassName("featureButtonDiv"))
                .Take(2);
            foreach (var feature in features)
            {
                feature.Click();
            }
            // Click next
            driver.AwaitForElement(By.Id("BackgroundNextButton")).Click();

            // Select the first encouraged calling for this race
            var callingContainer = driver.AwaitForElement(By.Id("wizardCallingButtonsDiv"));
            callingContainer.FindElement(By.ClassName("encouragedButton")).Click();

            // Click next
            driver.AwaitForElement(By.Id("callingNextButton")).Click();

            // Select two favorite skills
            var favoriteContainer = driver.AwaitForElement(By.Id("wizardFavouredSkillGroupsButtonsDiv"));
            var favorites = favoriteContainer.FindElements(By.ClassName("favouredSkillButton"))
                .Take(2);
            foreach (var favorite in favorites)
            {
                favorite.Click();
            }

            // Click next
            driver.AwaitForElement(By.Id("favouredSkillGroupsNextButton")).Click();

            // Select Body, Heart, and Wits, in that order, as favoured attributes.
            driver.AwaitForElement(By.CssSelector(".favouredAttribute3[attribute=body]")).Click();
            driver.AwaitForElement(By.CssSelector(".favouredAttribute2[attribute=heart]")).Click();
            driver.AwaitForElement(By.CssSelector(".favouredAttribute1[attribute=wits]")).Click();

            // Click next
            driver.AwaitForElement(By.Id("favouredAttributeNextButton")).Click();

            // Select "Valor over Wisdom"
            driver.AwaitForElement(By.ClassName("valourWisdomSelector")).Click();

            // Click next
            driver.AwaitForElement(By.Id("valourWisdomNextButton")).Click();

            // Select "Lucky Armour" reward.
            driver.AwaitForElement(By.CssSelector(".rewardButton[name=luckyArmour]")).Click();

            // Click next
            driver.AwaitForElement(By.Id("rewardsNextButton")).Click();

            // Spend 6 XP in Short Sword
            driver.AwaitForElement(By.CssSelector(".rankUpButton[skill=shortSword]")).Click();
            // Spend 2 XP in Travel
            driver.AwaitForElement(By.CssSelector(".rankUpButton[skill=travel]")).Click();
            // Spend 1 XP in Craft
            driver.AwaitForElement(By.CssSelector(".rankUpButton[skill=craft]")).Click();
            // Spend 1 XP in Lore
            driver.AwaitForElement(By.CssSelector(".rankUpButton[skill=lore]")).Click();

            // Click next
            driver.AwaitForElement(By.Id("previousExperienceNextButton")).Click();

            // Click finish
            driver.AwaitForElement(By.Id("finishCloseButton")).Click();
        }

    }
}
