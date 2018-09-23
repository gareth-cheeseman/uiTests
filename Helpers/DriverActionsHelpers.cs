using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using FunctionalTests.Globals;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FunctionalTests.Helpers
{
    public static class DriverActionsHelpers
    {
        private static  WebDriverWait Wait(this IWebDriver driver, int? timeout = null)
        {
            if (timeout == null) timeout = Configuration.DefaultTimeout;
            return new WebDriverWait(driver, TimeSpan.FromSeconds((double)timeout));
        }

        private static void Condition(IWebDriver driver, Func<IWebDriver, IWebElement> condition, int? timeout = null)
        {
            driver.Wait(timeout).Until(condition);
        }

        private static void Condition(IWebDriver driver, Func<IWebDriver, ReadOnlyCollection<IWebElement>> condition, int? timeout = null)
        {
            driver.Wait(timeout).Until(condition);
        }

        private static void ExceptionMessage(By by, Exception exception, [CallerMemberName] string memberName = "", int? timeout = null)
        {
            if (timeout == null) timeout = Configuration.DefaultTimeout;
            Console.WriteLine(
                $"Could not perform {memberName} on element identified by {by} after {timeout} seconds. Exception: {exception.Message}");
        }

        private static void ExceptionMessage(IWebElement element, Exception exception, [CallerMemberName] string memberName = "", int? timeout = null)
        {
            if (timeout == null) timeout = Configuration.DefaultTimeout;
            Console.WriteLine(
                $"Could not perform {memberName} on element identified by {element} after {timeout} seconds. Exception: {exception.Message}");
        }

        public static void Interact(this IWebDriver driver, By by, Func<IWebDriver, IWebElement> condition,
            Action<IWebElement> actionBody, [CallerMemberName] string memberName = "", int? timeout = null, int retries = 0)
        {
            try
            {
                Condition(driver, condition, timeout);
                var element = driver.FindElement(by);
                actionBody(element);
            }
            catch (Exception exception) when (exception is StaleElementReferenceException && retries < 1)
            {
                retries++;
                Interact(driver, by, condition, actionBody, memberName, retries: retries);
            }
            catch (Exception exception)
            {
                ExceptionMessage(by, exception, memberName, timeout);
                Assert.Fail();
            }

        }

        public static void Interact(this IWebDriver driver, IWebElement element, Func<IWebDriver, IWebElement> condition,
            Action<IWebElement> actionBody, [CallerMemberName] string memberName = "", int? timeout = null, int retries = 0)
        {
            try
            {
                Condition(driver, condition, timeout);
                actionBody(element);
            }
            catch (Exception exception) when (exception is StaleElementReferenceException && retries < 1)
            {
                retries++;
                Interact(driver, element, condition, actionBody, memberName, retries: retries);
            }
            catch (Exception exception)
            {
                ExceptionMessage(element, exception, memberName);
                Assert.Fail(exception.Message);
            }
        }
        public static void Interact(this IWebDriver driver, IWebElement element,
            Action<IWebElement> actionBody, [CallerMemberName] string memberName = "", int retries = 0)
        {
            try
            {
                actionBody(element);
            }
            catch (Exception exception) when (exception is StaleElementReferenceException && retries < 1)
            {
                retries++;
                Interact(driver, element, actionBody, memberName, retries: retries);
            }
            catch (Exception exception)
            {
                ExceptionMessage(element, exception, memberName);
                Assert.Fail();
            }
        }

        public static void Interact(this IWebDriver driver, By by, Func<IWebDriver, ReadOnlyCollection<IWebElement>> condition,
            Action<IReadOnlyCollection<IWebElement>> actionBody, [CallerMemberName] string memberName = "", int? timeout = null, int retries = 0)
        {
            try
            {
                Condition(driver, condition, timeout);
                var elements = driver.FindElements(by);
                actionBody(elements);
            }
            catch (Exception exception) when (exception is StaleElementReferenceException && retries < 1)
            {
                retries++;
                Interact(driver, by, condition, actionBody, memberName, retries: retries);
            }
            catch (Exception exception)
            {
                ExceptionMessage(by, exception, memberName, timeout);
                Assert.Fail();
            }
        }

        public static bool Check(this IWebDriver driver, By by, Func<IWebDriver, IWebElement> condition,
            [CallerMemberName] string memberName = "", int? timeout = null)
        {
            try
            {
                Condition(driver, condition, timeout);
            }
            catch (Exception exception)
            {
                ExceptionMessage(by, exception, memberName, timeout);
                return false;
            }
            return true;

        }
    }
}
