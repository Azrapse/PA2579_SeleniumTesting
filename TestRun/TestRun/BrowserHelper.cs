using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;

namespace TestRun
{
    public static class BrowserHelper
    {
        public readonly static Dictionary<string, (Func<IDriverConfig> Config, Func<IWebDriver> Driver)> SupportedBrowsers = new Dictionary<string, (Func<IDriverConfig> Config, Func<IWebDriver> Driver)>
        {
            { "Chrome", (() => new ChromeConfig(), () => new ChromeDriver()) },
            { "Firefox", (() => new FirefoxConfig(), () => new FirefoxDriver()) },
            { "Edge", (() => new EdgeConfig(), () => new EdgeDriver()) },
            { "Internet Explorer", (() => new InternetExplorerConfig(), () => new InternetExplorerDriver()) },
        };

        public static IWebElement AwaitForElement(this IWebDriver driver, By elementMatch)
        {
            var wait = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(30))
            {
                PollingInterval = TimeSpan.FromSeconds(5),         
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait.Until(drv => drv.FindElement(elementMatch));
        }
    }
}
