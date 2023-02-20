using TestRun;

// Gets a list of the supported browsers with name as key, and config and driver as value
var supportedBrowsers = BrowserHelper.SupportedBrowsers
    .ToList();

// Prompt the user to select one of the supported browsers
Console.WriteLine("Type the number for the browser to use, then press ENTER:");
var index = 1;
foreach (var browser in supportedBrowsers)
{
    Console.WriteLine($"[{index}] {browser.Key}");
    index++;
}

while (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index > supportedBrowsers.Count)
{
    Console.WriteLine($"Invalid selection. Please enter a value between 1 and {supportedBrowsers.Count}!\n");
}
var selectedBrowser = supportedBrowsers[index - 1];
Console.WriteLine($"Selected '{selectedBrowser.Key}' browser!\nPerforming tests...\n");

// Start the test collection with the selected browser's config and driver.
var (lazyConfig, lazyDriver) = selectedBrowser.Value;

TestsCollection.Start(lazyConfig, lazyDriver);