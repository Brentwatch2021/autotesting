using NUnit.Framework; // or the testing framework you prefer
using OpenCvSharp;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome; // or other browser driver namespaces if you're using a different browser
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using Webtests.Agent;
using WebTests;
using OpenCvSharp;


[TestFixture]
public class HelloWorldTest
{
    [Test, TestCaseSource(typeof(CsvDataHelper),
        nameof(CsvDataHelper.GetCsvData), new object[] { "testdata.csv" })]
    public void SayHelloToWorld()
    {
        var testdataLines = System.IO.File.ReadAllLines("testdata.csv");
        List<TestDataItem> testdata = new List<TestDataItem>();
        foreach (var line in testdataLines)
        {
            if (!line.Contains("controlKey,keystoSend"))
            {
                TestDataItem testDataItem = new TestDataItem();
                string[] parts = line.Split(',');
                if (parts.Length == 2)
                {
                    testDataItem.ControlKey = parts[0];
                    testDataItem.KeysToSend = parts[1];
                    testdata.Add(testDataItem);
                }
            }
        }


        ChromeOptions options = new ChromeOptions();
        //options.AddArgument("--headless");
        //options.AddArgument("");
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

            // Make sure the element is accessible before trying to interact the control might not
            // be ready for user input.

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathExpression2)));




            IWebElement todoNameInput = driver.FindElement(By.XPath(xpathExpression2));
            Console.WriteLine(todoNameInput.Text);
            //Console.ReadLine();
            Assert.IsTrue(todoNameInput.Text.Contains(""));
            todoNameInput.SendKeys("Todo3");

            //wait.Until(ExpectedConditions.ElementTextContains(driver, By.XPath(xpathExpression2), "To"));
            wait.Until(ExpectedConditions.ElementTextEquals(driver, By.XPath(xpathExpression2), "Todo3"));


            Screenshot scr = ((ITakesScreenshot)driver).GetScreenshot();

            string screenShot = "screenshot.png";
            scr.SaveAsFile(screenShot);




            Mat image1 = Cv2.ImRead("screenshot.png", ImreadModes.Grayscale);
            Mat image2 = Cv2.ImRead("screenshot.png", ImreadModes.Grayscale);

            //// Compute Structural Similarity Index (SSIM)
            //Scalar ssimScore = Cv2.CompareSsim(image1, image2);
            Scalar ssimScore = CalculateSsim(image1, image2);

            //// Display SSIM score
            Console.WriteLine("SSIM: " + ssimScore.Val0);

            //// If SSIM score is close to 1, images are similar; otherwise, there are differences
            //if (ssimScore.Val0 == 1)
            //{
            //    Console.WriteLine("Images are identical.");
            //}
            //else
            //{
            //    Console.WriteLine("Images are different.");
            //}




            Console.WriteLine();


            //Assert.IsTrue(element.Text == "f.C65189A16DB7DC7D7095EE0FE00975CF.d.D34B762DBA0DACE8ABEB8640C4D84E44.e.2");



            // Find the input element using the XPath expression



            IWebElement selectElement = driver.FindElement(By.XPath(xpathExpression3));



            selectElement.Click();

            IWebElement ToUnitOption = driver.FindElement(By.XPath(xpathExpression4));
            //Thread.Sleep(3000);
            ToUnitOption.Click();
            //ToUnitOption.SendKeys(Keys.Enter);






            // Assertions








            driver.Quit();


            // Perform actions on the webpage
            // For this example, we'll just print "Hello, World!" to the console
            Console.WriteLine("We are done yipee");
            Console.ReadLine();


            // You can add more complex interactions here, such as finding elements, clicking buttons, etc.
        }
    }

    private bool ElementExists(IWebDriver driver, By by)
    {
        IWebElement elementToWaitFor = driver.FindElement(by);
        return elementToWaitFor.Displayed;
    }


    private static double CalculateSsim(Mat img1, Mat img2)
    {
        const double C1 = 6.5025, C2 = 58.5225;

        Mat img1f = new Mat();
        img1.ConvertTo(img1f, MatType.CV_32F);
        Mat img2f = new Mat();
        img2.ConvertTo(img2f, MatType.CV_32F);

        Mat img1Sq = img1f.Mul(img1f);
        Mat img2Sq = img2f.Mul(img2f);
        Mat img1Img2 = img1f.Mul(img2f);

        Mat mu1 = new Mat(), mu2 = new Mat();
        Cv2.GaussianBlur(img1f, mu1, new Size(11, 11), 1.5);
        Cv2.GaussianBlur(img2f, mu2, new Size(11, 11), 1.5);

        Mat mu1Sq = mu1.Mul(mu1);
        Mat mu2Sq = mu2.Mul(mu2);
        Mat mu1Mu2 = mu1.Mul(mu2);

        Mat sigma1Sq = new Mat(), sigma2Sq = new Mat(), sigma12 = new Mat();
        Cv2.GaussianBlur(img1Sq, sigma1Sq, new Size(11, 11), 1.5);
        sigma1Sq = sigma1Sq - mu1Sq;

        Cv2.GaussianBlur(img2Sq, sigma2Sq, new Size(11, 11), 1.5);
        sigma2Sq = sigma2Sq - mu2Sq;

        Cv2.GaussianBlur(img1Img2, sigma12, new Size(11, 11), 1.5);
        sigma12 = sigma12 - mu1Mu2;

        Mat t1 = new Mat(), t2 = new Mat(), t3 = new Mat();

        t1 = 2 * mu1Mu2 + C1;
        t2 = 2 * sigma12 + C2;
        t3 = t1.Mul(t2);

        t1 = mu1Sq + mu2Sq + C1;
        t2 = sigma1Sq + sigma2Sq + C2;
        t1 = t1.Mul(t2);

        Mat ssimMap = new Mat();
        Cv2.Divide(t3, t1, ssimMap);
        Scalar mssim = Cv2.Mean(ssimMap);

        return (mssim.Val0 + mssim.Val1 + mssim.Val2) / 3;
    }
}
