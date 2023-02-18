using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;

namespace TestRun
{    
    public class TestsCollection
    {
        public static void Start(Func<IDriverConfig> configGetter, Func<IWebDriver> driverGetter)
        {
            var config = configGetter();
            var manager = new DriverManager().SetUpDriver(config);
            
            IWebDriver driver;
            try 
            { 
                driver = driverGetter();
            }
            catch (Exception ex) 
            {
                throw new Exception("Problems initializing browser driver.", ex);
            }
            // Making sure the driver is released at the end of execution.
            //using (driver)
            {
                driver.Navigate().GoToUrl("https://azrapse.es/tor/sheet.html");
                driver.AwaitForElement("nameInput");
                var nameBox = driver.FindElement(By.Id("nameInput"));
                nameBox.SendKeys("Frodo Baggins");
            }
        }

        public static bool Test1(IWebDriver driver)
        {
            return false;
        }
    }
}
