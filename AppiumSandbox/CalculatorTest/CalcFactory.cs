using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumSandbox
{
    public class CalcFactory
    {
        private static CalcObjectModel _calcWrapper;

        public static CalcObjectModel GetCalcObjectModel()
        {
            if (_calcWrapper == null)
            { 
                var capabilities = new DesiredCapabilities();

                const string appId = @"Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";
                const string webDriverUrl = @"http://127.0.0.1:4723";

                capabilities.SetCapability("app", appId);
                var calcSession = new RemoteWebDriver(new Uri(webDriverUrl), capabilities);
                calcSession.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
                calcSession.Manage().Window.Position = new Point(0,0);                
                _calcWrapper = new CalcObjectModel(calcSession);
            }

            return _calcWrapper;
        }
    }
}
