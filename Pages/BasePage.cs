using System;
using FunctionalTests.Globals;
using FunctionalTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace FunctionalTests.Pages
{
    public class BasePage<T> : FTLoadableComponent<T> where T : FTLoadableComponent<T>
    {
        protected readonly IWebDriver Driver;
        private readonly string _url;
        private readonly string _stemUrl;

        private readonly TestContext _testContext = ScenarioContext.Current.ScenarioContainer
            .Resolve<Microsoft.VisualStudio.TestTools.UnitTesting.TestContext>();

        protected BasePage(string url = null, string stemUrl = null)
        {
            var _baseUrl = _testContext.Properties["baseURL"].ToString();

            Driver = ScenarioContext.Current.Get<IWebDriver>();

            if (url == null)
            {
                this._url = _baseUrl;
            }
            else if (url.Contains("http"))
            {
                this._url = url;
            }
            else
            {
                this._url = _baseUrl + url;
            }
            this._stemUrl = stemUrl;
            Console.WriteLine("The url is...  " + _url);

        }

        protected override void ExecuteLoad()
        {
            Driver.Navigate().GoToUrl(_url);
        }

        protected override bool EvaluateLoadedStatus()
        {
            if (!Driver.Url.Contains("http")) return false;

            if ((_stemUrl != null && Driver.Url.Contains(_stemUrl)) || Driver.Url.Contains(_url))
            {
                if (Driver.WaitForElementOnPageLoad(DefineSearchField()))
                {
                    return true;
                }

                UnableToLoadMessage = $"Could not load {GetType().Name} within the designated time period";
                return false;
            }

            UnableToLoadMessage = $"Could not load {GetType().Name}, url incorrect";
            return false;
        }

        protected virtual By DefineSearchField()
        {
            return By.Id(string.Empty);
        }

        protected virtual By DefineSearchFieldCore(By by)
        {
            return by;
        }

        private readonly By _buttonCookieBanner = By.XPath("//button[text()='Do not show this message again']");
        public TPage AcceptCookies<TPage>(TPage page)
        {
            Driver.Click(_buttonCookieBanner);
            return page;
        }


    }
}
