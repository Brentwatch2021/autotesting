using NUnit.Framework; // or the testing framework you prefer
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome; // or other browser driver namespaces if you're using a different browser
using System;
using System.Collections.Generic;
using System.Threading;
using Webtests.Agent;

[TestFixture]
public class HelloWorldTest
{
    [Test, TestCaseSource(typeof(CsvDataHelper) ,
        nameof(CsvDataHelper.GetCsvData), new object[] { "testdata.csv" })]
    public void SayHelloToWorld()
    {
        var testdataLines = System.IO.File.ReadAllLines("testdata.csv");
        List<TestDataItem> testdata = new List<TestDataItem>();
        foreach (var line in testdataLines)
        {
            if(!line.Contains("controlKey,keystoSend"))
            {
                TestDataItem testDataItem = new TestDataItem();
                string[] parts = line.Split(',');
                if(parts.Length == 2)
                {
                    testDataItem.ControlKey = parts[0];
                    testDataItem.KeysToSend = parts[1];
                    testdata.Add(testDataItem);
                }
            }
        }


        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless");
        // Create a new instance of the ChromeDriver (or other WebDriver) to open the browser
        using (IWebDriver driver = new ChromeDriver(options))
        {
            // Navigate to a webpage
            driver.Navigate().GoToUrl("http://localhost:56873/");

            

            string xpathExpression = "//a[@href='/Home/Contact']";
            string xpathExpression2 = "//input[@id='TodoName']";
            string xpathExpression3 = "//select[@id='IssueType']";
            string xpathExpression4 = "//option[@id='ToProduction']";

            IWebElement element = driver.FindElement(By.XPath(xpathExpression));

            element.Click();



            
            // Find the input element using the XPath expression
            IWebElement inputElement = driver.FindElement(By.XPath(xpathExpression2));
            inputElement.SendKeys("howzit pal");


            IWebElement selectElement = driver.FindElement(By.XPath(xpathExpression3));
            


            selectElement.Click();

            IWebElement ToUnitOption = driver.FindElement(By.XPath(xpathExpression4));
            //Thread.Sleep(3000);
            ToUnitOption.Click();
            inputElement.SendKeys(Keys.Enter);


            Screenshot scr = ((ITakesScreenshot)driver).GetScreenshot();

            string screenShot = "screenshot.png";
            scr.SaveAsFile(screenShot);

            





            driver.Quit();


            // Perform actions on the webpage
            // For this example, we'll just print "Hello, World!" to the console
            Console.WriteLine("We are done yipee");
            Console.ReadLine();


            // You can add more complex interactions here, such as finding elements, clicking buttons, etc.
        }
    }
}
