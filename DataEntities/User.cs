using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace FunctionalTests.DataEntities
{
    public class User
    {
        public string Email { get; set; } 
        public string Password { get; set; }


        public User()
        {
            var testContext = FeatureContext.Current.Get<TestContext>();
            Email = testContext.Properties["email"].ToString();
            Password = testContext.Properties["password"].ToString();
        }
    }



}
