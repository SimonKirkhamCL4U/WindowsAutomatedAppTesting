using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace AppiumSandbox.HorizonTest
{
    public class HorizonObjectModelFactory
    {
        private static HorizonObjectModel _horizonObjectModel;

        public static HorizonObjectModel GetHorizonObjectModel()
        {
            if (_horizonObjectModel == null)
            {
                var capabilities = new DesiredCapabilities();

                //const string appId = @"C:\Windows\System32\notepad.exe";
                const string appId = @"C:\Code\Zuto.Horizon.Client\HorizonDesktop\bin\Debug\HorizonDesktop.exe";
                //const string appId = @"C:\Code\TestProjects\MvvmIOCTests\SimpleIOC\WindowWithoutParameterViaEvent\bin\Debug\MvvmIOCTests.exe";
                const string webDriverUrl = @"http://127.0.0.1:4723";

                capabilities.SetCapability("app", appId);
                var session = new RemoteWebDriver(new Uri(webDriverUrl), capabilities);
                Thread.Sleep(3000);
                _horizonObjectModel = new HorizonObjectModel(session);
                session.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            }

            return _horizonObjectModel;
        }
    }

    public class HorizonObjectModel
    {
        public IWebDriver _webDriver;
        private IDictionary<string, IWebElement> _controlCache;
        private string _mainWindowHandle;

        public HorizonObjectModel(IWebDriver webDriver)
        {
            _webDriver = webDriver;

            _controlCache = new Dictionary<string, IWebElement>();
        }

        public IWebElement SearchButton { get { return GetByName("Search"); } }
        //public IWebElement Lastname { get { return GetByAutomationId("txtLastName"); } }

        public IWebElement Lastname { get { return GetByXPath("//Edit[@AutomationId=\"txtLastName\"]"); } }
        public IWebElement ReferenceType { get { return GetByAutomationId("cboReference"); } }

        public IWebElement SearchButton2 { get { return GetByXPath("//MenuItem[@AutomationId=\"mnuSearch\"]"); } }


        public void OpenSearchWindow()
        {
            _mainWindowHandle = _webDriver.CurrentWindowHandle;
            Debug.WriteLine($"MainWindowHandle = {_mainWindowHandle}");
            var finder = new PopupWindowFinder(_webDriver);
            var popupWindowHandle = finder.Click(SearchButton);
            Debug.WriteLine($"popupWindowHandle = {popupWindowHandle}");
            Debug.WriteLine($"MainWindow.pagesource = {_webDriver.PageSource}");
            _webDriver.SwitchTo().Window(popupWindowHandle);
            Debug.WriteLine($"PopUp.pagesource = {_webDriver.PageSource}");
        }

        public void Dispose()
        {
            _webDriver.Dispose();
            _webDriver = null;
        }

        private IWebElement GetByAutomationId(string automationId, bool noCache = false)
        {
            if (_controlCache.ContainsKey(automationId) && !noCache)
            {
                return _controlCache[automationId];
            }
            else
            {
                var control = _webDriver.FindElement(By.XPath($"[@AutomationId=\"{automationId}\"]"));
                if (!noCache) _controlCache.Add(automationId, control);

                return control;
            }
        }
        private IWebElement GetByXPath(string xPath, bool noCache = false)
        {
            if (_controlCache.ContainsKey(xPath) && !noCache)
            {
                return _controlCache[xPath];
            }
            else
            {
                var control = _webDriver.FindElement(By.XPath(xPath));
                if (!noCache) _controlCache.Add(xPath, control);

                return control;
            }
        }

        private IWebElement GetByName(string name, bool noCache = false)
        {
            if (_controlCache.ContainsKey(name) && !noCache)
            {
                return _controlCache[name];
            }
            else
            {
                var control = _webDriver.FindElement(By.Name(name));
                if (!noCache) _controlCache.Add(name, control);

                return control;
            }
        }
    }

    public class HorizonTests
    {
        private HorizonObjectModel _horizonObjectModel;

        [OneTimeSetUp]
        public void SetUp()
        {
            _horizonObjectModel = HorizonObjectModelFactory.GetHorizonObjectModel();
        }

        [Test]
        public void CanOpenHorizon()
        {

            _horizonObjectModel.OpenSearchWindow();
            _horizonObjectModel.Lastname.SendKeys("Smith");
            _horizonObjectModel.SearchButton2.Click();
            Thread.Sleep(10000);
            //Assert.IsTrue(false);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _horizonObjectModel.Dispose();
        }
    }
}