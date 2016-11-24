using System;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace CalculatorTest
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
            // Launch the calculator app
            var appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            CalculatorSession = new RemoteWebDriver(new Uri(WindowsApplicationDriverUrl), appCapabilities);
            Assert.IsNotNull(CalculatorSession);
            CalculatorSession.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));

            // Make sure we're in standard mode
            CalculatorSession.FindElementByXPath("//Button[starts-with(@Name, \"Menu\")]").Click();
            OriginalCalculatorMode =
                CalculatorSession.FindElementByXPath(
                    "//List[@AutomationId=\"FlyoutNav\"]//ListItem[@IsSelected=\"True\"]").Text;
            CalculatorSession.FindElementByXPath("//ListItem[@Name=\"Standard Calculator\"]").Click();

            CalculatorResult = CalculatorSession.FindElementByName("Display is  0 ") as RemoteWebElement;
            Assert.IsNotNull(CalculatorResult);
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            // Restore original mode before closing down
            CalculatorSession.FindElementByXPath("//Button[starts-with(@Name, \"Menu\")]").Click();
            CalculatorSession.FindElementByXPath("//ListItem[@Name=\"" + OriginalCalculatorMode + "\"]").Click();

            CalculatorResult = null;
            CalculatorSession.Dispose();
            CalculatorSession = null;
        }

        [TearDown]
        public void Clear()
        {
            CalculatorSession.FindElementByName("Clear").Click();
            Assert.AreEqual("Display is  0 ", CalculatorResult.Text);
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