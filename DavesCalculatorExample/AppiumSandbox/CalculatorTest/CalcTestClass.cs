using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AppiumSandbox
{
    [TestFixture]
    public class CalcTestClass
    {
        private CalcObjectModel _calcWrapper;

        [OneTimeSetUp]
        public void SetUp()
        {
            _calcWrapper = CalcFactory.GetCalcObjectModel();
        }

        [Test]
        public void EightPlusTwoEqualsTen()
        {
            _calcWrapper.EightButton.Click();
            _calcWrapper.PlusButton.Click();
            _calcWrapper.TwoButton.Click();
            _calcWrapper.EqualsButton.Click();

            Assert.That(_calcWrapper.GetResult(), Is.EqualTo(10m));
        }

        [Test]
        public void NineTimesFourEqualsThirtySix()
        {
            _calcWrapper.NineButton.Click();
            _calcWrapper.MultiplyButton.Click();
            _calcWrapper.FourButton.Click();
            _calcWrapper.EqualsButton.Click();

            Assert.That(_calcWrapper.GetResult(), Is.EqualTo(36m));            
        }

        [Test]
        public void SixDividedByThreeEqualsTwo()
        {
            _calcWrapper.SixButton.Click();
            _calcWrapper.DivideButton.Click();
            _calcWrapper.ThreeButton.Click();
            _calcWrapper.EqualsButton.Click();
            
            Assert.That(_calcWrapper.GetResult(), Is.EqualTo(2m));
        }

        [Test]
        public void SevenMinusOneEqualsSix()
        {
            _calcWrapper.SevenButton.Click();
            _calcWrapper.MinusButton.Click();
            _calcWrapper.OneButton.Click();
            _calcWrapper.EqualsButton.Click();
            
            Assert.That(_calcWrapper.GetResult(), Is.EqualTo(6m));
        }
        
        [OneTimeTearDown]
        public void TearDown()
        {
            _calcWrapper.Dispose();
        }
    }
}