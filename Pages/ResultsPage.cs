using FunctionalTests.Helpers;
using OpenQA.Selenium;

namespace FunctionalTests.Pages
{
    public class ResultsPage : BasePage<ResultsPage>
    {
        public ResultsPage() : base("search?")
        {
        }

        private readonly By _results = By.Id("ires");

        protected override By DefineSearchField()
        {
            return base.DefineSearchFieldCore(_results);
        }

        public bool IsAt()
        {
            return Driver.CheckElementIsVisible(_results);
        }


        private readonly By _firstEmphasis = By.TagName("em");

        public string ReturnFirstEmphasis()
        {
            return Driver.GetText(_firstEmphasis);
        }
    }
}