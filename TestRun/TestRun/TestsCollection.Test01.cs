using NUnit.Framework;
using OpenQA.Selenium;

namespace TestRun
{
    public static partial class TestsCollection
    {

        [Test]
        /// <summary>
        /// Test that the site loads by checking the About window and finding the author's name in it.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static void Test1About(IWebDriver driver)
        {
            // Go to site.
            driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");

            // Await until the about button is available, then click it.
            var aboutButton = driver.AwaitForElement(By.Id("aboutButton"));
            // Often, it's not enough to wait for the element to be present. You need to wait a while for it to be interactable.
            Thread.Sleep(1000);
            aboutButton.Click();

            // Find the about window
            var aboutWindow = driver.AwaitForElement(By.Id("aboutDiv"));
            var span = aboutWindow.FindElement(By.TagName("span"));
            var text = span.Text;
            
            // Check that the About text contains my name.
            Assert.That(text.Contains("David Esparza Guerrero"));
        }
    }
}