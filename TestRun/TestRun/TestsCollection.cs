using OpenQA.Selenium;
using System.Text.Json.Nodes;
using WebDriverManager;
using WebDriverManager.DriverConfigs;

namespace TestRun
{
    public static partial class TestsCollection
    {        

        public static void Start(Func<IDriverConfig> configGetter, Func<IWebDriver> driverGetter)
        {
            // Connect to the selected browser.
            var config = configGetter();
            new DriverManager().SetUpDriver(config);
            
            IWebDriver driver;
            try 
            { 
                driver = driverGetter();
            }
            catch (Exception ex) 
            {
                throw new Exception("Problems initializing browser driver.", ex);
            }

            var testList = new List<Func<IWebDriver, bool>>()
            { 
                //Test1About,
                //Test2CharacterCreation,
                //Test3CharacterExport,
                //Test4CharacterImport,
                //Test5UnequippingLowersEncumbrance,
                //Test6OnlineCharacterServer,
                Test7RollDice,
            };
            var testResults = new List<(string Name, bool Result)>();

            // Making sure the driver is released at the end of execution.
            using (driver)
            {
                foreach (var test in testList)
                {
                    var result = test(driver);
                    testResults.Add((test.Method.Name, result));
                }
            }

            Console.Clear();
            Console.WriteLine("Test results:");
            foreach(var test in testResults)
            {
                Console.WriteLine($"{test.Name}: {test.Result}");
            }
        }

    }
}
