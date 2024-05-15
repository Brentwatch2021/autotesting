# Selenium with C# Examples

This repository showcases examples of using Selenium WebDriver with C# for automated web testing.

## Setting up Selenium Environment

- Install Selenium WebDriver NuGet package for C#.
- Download appropriate web browser driver (e.g., ChromeDriver).
- Configure driver path in code.

```csharp
using OpenQA.Selenium.Chrome;













1. Setting up Selenium Environment:

Install Selenium WebDriver NuGet package for C#.
Download appropriate web browser driver (e.g., ChromeDriver).
Configure driver path in code.
csharp
Copy code
using OpenQA.Selenium.Chrome;
2. Launching a Browser:

Initialize WebDriver object for desired browser.
Open a URL in the browser window.
csharp
Copy code
IWebDriver driver = new ChromeDriver();
driver.Navigate().GoToUrl("https://example.com");
3. Locating Web Elements:

Use various locators (ID, class, XPath) to find elements.
Store element references for interaction.
csharp
Copy code
IWebElement element = driver.FindElement(By.Id("elementId"));
4. Interacting with Elements:

Perform actions like clicking, typing, selecting.
Simulate user interactions with web page elements.
csharp
Copy code
element.Click();
element.SendKeys("Text to type");
5. Handling Dropdowns:

Locate dropdown element.
Use SelectElement class for dropdown interaction.
csharp
Copy code
SelectElement dropdown = new SelectElement(driver.FindElement(By.Id("dropdownId")));
dropdown.SelectByText("Option");
6. Handling Alerts and Pop-ups:

Switch driver focus to alert.
Accept, dismiss, or interact with the alert.
csharp
Copy code
IAlert alert = driver.SwitchTo().Alert();
alert.Accept();
7. Navigating Between Pages:

Use methods like Navigate().GoToUrl(), Navigate().Back(), Navigate().Forward().
Handle navigation between different pages.
csharp
Copy code
driver.Navigate().Back();
8. Working with Frames and IFrames:

Switch driver focus to frame or iframe.
Interact with elements within frames.
csharp
Copy code
driver.SwitchTo().Frame("frameName");
9. Handling Cookies:

Get, set, or delete cookies.
Manage session information.
csharp
Copy code
driver.Manage().Cookies.AddCookie(new Cookie("cookieName", "cookieValue"));
10. Executing JavaScript:

Execute custom JavaScript code.
Enhance testing capabilities by manipulating page elements dynamically.
csharp
Copy code
((IJavaScriptExecutor)driver).ExecuteScript("alert('Hello Selenium!')");
11. Capturing Screenshots:

Take screenshots of web pages.
Useful for debugging and reporting.
csharp
Copy code
((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("screenshot.png");
12. Waits and Timeouts:

Implicit and explicit waits.
Ensure synchronization between WebDriver and web page.
csharp
Copy code
driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
13. Handling Multiple Windows:

Switch driver focus between multiple browser windows.
Interact with elements in different windows.
csharp
Copy code
string mainWindow = driver.WindowHandles[0];
string popupWindow = driver.WindowHandles[1];
driver.SwitchTo().Window(popupWindow);
14. Maximizing Browser Window:

Maximize browser window for better visibility.
Ensure consistent testing environment.
csharp
Copy code
driver.Manage().Window.Maximize();
15. Reading Text from Elements:

Retrieve text content from web elements.
Validate displayed text.
csharp
Copy code
string text = element.Text;
16. Verifying Element Presence:

Check if an element is present on the page.
Ensure expected elements are displayed.
csharp
Copy code
bool isPresent = driver.FindElement(By.Id("elementId")).Displayed;
17. Handling Checkbox and Radio Button:

Select or deselect checkboxes.
Choose radio buttons.
csharp
Copy code
driver.FindElement(By.Id("checkboxId")).Click();
18. Performing Drag and Drop:

Simulate dragging an element and dropping it elsewhere.
Useful for testing user interactions.
csharp
Copy code
Actions builder = new Actions(driver);
builder.DragAndDrop(element, targetElement).Perform();
19. Cleaning up Resources:

Close browser window.
Release WebDriver resources.
csharp
Copy code
driver.Quit();
20. Writing Assertions for Verification:

Verify expected outcomes using assertions.
Ensure application behaves as expected.
csharp
Copy code
Assert.AreEqual("Expected Text", actualText);





