using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FunctionalTests.Helpers
{
    public static class Asserts
    {
        public static void AssertTrue(bool assertedValue, string reportingMessage)
        {
            TryCatch(() => Assert.IsTrue(assertedValue), reportingMessage);
        }

        public static void AssertEquals(string actualValue, string expectedValue, string reportingMessage)
        {
            TryCatch(() => Assert.AreEqual(expectedValue, actualValue), reportingMessage);
        }

        public static void AssertEquals(int actualValue, int expectedValue, string reportingMessage)
        {
            TryCatch(() => Assert.AreEqual(expectedValue, actualValue), reportingMessage);
        }

        public static void AssertEquals(decimal actualValue, decimal expectedValue, string reportingMessage)
        {
            TryCatch(() => Assert.AreEqual(expectedValue, actualValue), reportingMessage);
        }

        public static void AssertFalse(bool assertedValue, string reportingMessage)
        {
            TryCatch(() => Assert.IsFalse(assertedValue), reportingMessage);
        }

        private static void TryCatch(Action actionBody, string reportingMessage)
        {
            try
            {
                actionBody();
                Console.WriteLine(reportingMessage);
            }
            //todo investigage mstest as to whether this exception handling is correct
            catch (UnitTestAssertException)
            {
                Console.WriteLine($"Failure occurred when executing check '{reportingMessage}'");
                throw;
            }
        }
    }
}
