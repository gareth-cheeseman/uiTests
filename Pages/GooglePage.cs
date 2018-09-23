using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionalTests.Helpers;
using OpenQA.Selenium;

namespace FunctionalTests.Pages
{
    public class GooglePage : BasePage<GooglePage>
    {
        public GooglePage() : base("/")
        {
        }

        private readonly By _searchBox = By.Name("q");
        private readonly By _searchButton = By.Name("btnK");

        protected override By DefineSearchField()
        {
            return DefineSearchFieldCore(_searchBox);
        }

        public GooglePage EnterSearchTerm(string searchTerm)
        {
            Driver.EnterText(_searchBox, searchTerm);


            return this;
        }

        public ResultsPage SubmitSearch()
        {
            Driver.Click(_searchButton);
            return new ResultsPage();
        }
    }
}
