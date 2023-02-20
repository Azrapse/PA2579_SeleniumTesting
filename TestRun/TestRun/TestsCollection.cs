using NUnit.Framework;
using OpenQA.Selenium;
using WebDriverManager;
using WebDriverManager.DriverConfigs;

namespace TestRun
{
    [TestFixture]
    public static partial class TestsCollection
    {
        /// <summary>
        /// Gets a collection with all the methods marked with the [Test] attribute in this class.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Action<IWebDriver>> Tests =>
            typeof(TestsCollection)
                .GetMethods()
                .Where(m => m.CustomAttributes.Any(a => a.AttributeType == typeof(TestAttribute)))
                .Select(m => m.CreateDelegate<Action<IWebDriver>>());
        

        /// <summary>
        /// This method runs all tests sequentially.
        /// </summary>
        /// <param name="configGetter">A config creator for the selected browser</param>
        /// <param name="driverGetter">A driver creator for the selected browser</param>
        public static void Start(Func<IDriverConfig> configGetter, Func<IWebDriver> driverGetter)
        {
            // Connect to the selected browser.
            var config = configGetter();
            new DriverManager().SetUpDriver(config);
                         
            // A list where to store the results for each test.
            var testResults = new List<(string Name, bool Result, string ErrorMessage)>();
                        
            // Sequentially run each test.
            foreach (var test in Tests)
            {
                Console.WriteLine($"Testing now '{test.Method.Name}':");
                try
                {
                    // Making sure the driver is released at the end of execution of each test.
                    // 'using' will close the browser window after each test for a clean slate.
                    using var driver = driverGetter();
                    test(driver);                    
                    // If it reaches here, the test passes.
                    testResults.Add((test.Method.Name, true, ""));
                }
                catch (Exception ex)
                {
                    // Any exception causes the test to fail.
                    testResults.Add((test.Method.Name, false, ex.Message));
                }
                Console.Clear();
                Console.WriteLine($"The result of the previous test was {(testResults.Last().Result ? "Pass" : "Fail")}");
            }

            // Display the final results.
            Console.Clear();
            Console.WriteLine("Test results:");
            foreach(var (Name, Result, ErrorMessage) in testResults)
            {
                Console.WriteLine($"{Name}: {(Result ? "Pass" : "Fail")}. {ErrorMessage}");
            }
        }

    }
}
