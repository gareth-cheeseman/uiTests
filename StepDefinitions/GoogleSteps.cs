using System;
using FunctionalTests.Helpers;
using FunctionalTests.Pages;
using TechTalk.SpecFlow;

namespace FunctionalTests.StepDefinitions
{
    [Binding]
    public class GoogleSteps
    {
        [Given(@"I am on google")]
        public void GivenIAmOnGoogle()
        {
            _googlePage.Load();
        }
        
        [When(@"I enter the search term '(.*)' and submit the search")]
        public void WhenIEnterTheSearchTermAndSubmitTheSearch(string searchTerm)
        {
            _googlePage.EnterSearchTerm(searchTerm).SubmitSearch();
        }
        
        [Then(@"I should be navigated to the results page")]
        public void ThenIShouldBeNavigatedToTheResultsPage()
        {
            _resultsPage.Load();
        }
        
        [Then(@"the result should contain '(.*)'")]
        public void ThenTheResultShouldContain(string result)
        {
            Asserts.AssertEquals(_resultsPage.ReturnFirstEmphasis(), result, "Is correct");
        }

        private GooglePage _googlePage;
        private ResultsPage _resultsPage;

        public GoogleSteps()
        {
            _googlePage = new GooglePage();
            _resultsPage = new ResultsPage();
        }
    }
}
