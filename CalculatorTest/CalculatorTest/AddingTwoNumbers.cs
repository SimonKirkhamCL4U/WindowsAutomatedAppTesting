using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CalculatorTest
{
    public class CalculatorTests
    {
        public class BasicScenarios
        {
            protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
            protected static RemoteWebDriver CalculatorSession;
            protected static RemoteWebElement CalculatorResult;
            protected static string OriginalCalculatorMode;

            [OneTimeSetUp]
            public static void Setup()
            {
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
                CalculatorSession = new RemoteWebDriver(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Assert.IsNotNull(CalculatorSession);
                CalculatorSession.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));

                CalculatorResult = CalculatorSession.FindElement(By.XPath("//Text[@AutomationId=\"CalculatorResults\"]")) as RemoteWebElement;
                Assert.That(CalculatorResult, Is.Not.Null);
            }

            [OneTimeTearDown]
            public static void TearDown()
            {
                CalculatorSession.FindElementByName("Clear").Click();
                Assert.AreEqual("Display is  0 ", CalculatorResult.Text);

                CalculatorResult = null;
                CalculatorSession.Dispose();
                CalculatorSession = null;
            }

            [Test]
            public void Addition()
            {
                CalculatorSession.FindElementByName("One").Click();
                CalculatorSession.FindElementByName("Plus").Click();
                CalculatorSession.FindElementByName("Seven").Click();
                CalculatorSession.FindElementByName("Equals").Click();
                Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
            }

            [Test]
            public void Combination()
            {
                CalculatorSession.FindElementByXPath("//Button[@Name=\"Seven\"]").Click();
                CalculatorSession.FindElementByXPath("//Button[@Name=\"Multiply by\"]").Click();
                CalculatorSession.FindElementByXPath("//Button[@Name=\"Nine\"]").Click();
                CalculatorSession.FindElementByXPath("//Button[@Name=\"Plus\"]").Click();
                CalculatorSession.FindElementByXPath("//Button[@Name=\"One\"]").Click();
                CalculatorSession.FindElementByXPath("//Button[@Name=\"Equals\"]").Click();
                CalculatorSession.FindElementByXPath("//Button[@Name=\"Divide by\"]").Click();
                CalculatorSession.FindElementByXPath("//Button[@Name=\"Eight\"]").Click();
                CalculatorSession.FindElementByXPath("//Button[@Name=\"Equals\"]").Click();
                Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
            }

            [Test]
            public void Division()
            {
                CalculatorSession.FindElementByName("Eight").Click();
                CalculatorSession.FindElementByName("Eight").Click();
                CalculatorSession.FindElementByName("Divide by").Click();
                CalculatorSession.FindElementByName("One").Click();
                CalculatorSession.FindElementByName("One").Click();
                CalculatorSession.FindElementByName("Equals").Click();
                Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
            }

            [Test]
            public void Multiplication()
            {
                CalculatorSession.FindElementByName("Nine").Click();
                CalculatorSession.FindElementByName("Multiply by").Click();
                CalculatorSession.FindElementByName("Nine").Click();
                CalculatorSession.FindElementByName("Equals").Click();
                Assert.AreEqual("Display is  81 ", CalculatorResult.Text);
            }

            [Test]
            public void Subtraction()
            {
                CalculatorSession.FindElementByName("Nine").Click();
                CalculatorSession.FindElementByName("Minus").Click();
                CalculatorSession.FindElementByName("One").Click();
                CalculatorSession.FindElementByName("Equals").Click();
                Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
            }
        }
    }
}
