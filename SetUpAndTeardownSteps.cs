using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using FunctionalTests.Helpers;

namespace FunctionalTests
{
    [Binding]
    public class SetUpAndTeardownSteps
    {
        private static IWebDriver _driver;

        [BeforeScenario]
        public static void InitializeDriver()
        {
            _driver = DriverMethods.GetDriver();

            ScenarioContext.Current.Set(_driver);
        }


        [AfterScenario]
        public static void WrapUpReport()
        {
            //todo add in call to reporting methods
            ScenarioContext.Current.Clear();
            _driver.Quit();
        }
    }
}
