using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace FunctionalTests.Helpers
{
    public static class DriverActions
    {
        /// <summary>
        /// Tries to send the given input string to the element specified taking into account the predefined timeout
        /// Catches and handles exceptions that might occur
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="textToEnter"></param>
        public static void EnterText(this IWebDriver driver, By by, string textToEnter)
        {
            driver.Interact(by, ElementIsVisible(by), element =>
            {
                element.Clear();
                element.SendKeys(textToEnter);
            });

        }

        /// <summary>
        /// Tries to send the given non-text key to the element specified taking into account the predefined timeout
        /// Catches and handles exceptions that might occur
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="keyToSend">Use OpenQA.Selenium.Keys</param>
        public static void KeyStroke(this IWebDriver driver, By by, string keyToSend)
        {
            driver.Interact(by, ElementIsVisible(by), element =>
            {
                element.SendKeys(keyToSend);
            });
        }



        /// <summary>
        /// Tries to click on the element specified, taking into account the predefined timeout
        /// Catches and handles exceptions that might occur
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        public static void Click(this IWebDriver driver, By by)
        {

            driver.Interact(by, ElementToBeClickable(by), element =>
            {
                element.Click();
            });

        }

        /// <summary>
        /// Tries to click on the element specified, taking into account the predefined timeout
        /// Catches and handles exceptions that might occur
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        public static void Click(this IWebDriver driver, IWebElement element)
        {
            driver.Interact(element, ElementToBeClickable(element), webElement =>
            {
                webElement.Click();
            });
        }

        /// <summary>
        /// Tries to select value from list specified, taking into account the predefined timeout
        /// Catches and handles exceptions that might occur
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="textToSelect"></param>
        public static void SelectFromList(this IWebDriver driver, By by, string textToSelect)
        {
            driver.Interact(by, ElementIsVisible(by), element =>
            {
                new SelectElement(element).SelectByText(textToSelect);
            });
        }


        /// <summary>
        ///     Waits for an element to be visible
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeout">Optional default timeout override</param>
        /// <returns></returns>
        public static bool CheckElementIsVisible(this IWebDriver driver, By by, int? timeout = null)
        {
            return driver.Check(by, ElementIsVisible(by), timeout: timeout);

        }

        /// <summary>
        ///     Waits for an element to be clickable(visible AND enabled)
        ///     Takes into account a predefined timeout
        ///     Preferred method to be used for determining whether a page has been loaded
        ///     See for example all Page Object constructors
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="timeout">Optional default timeout override</param>
        /// <returns></returns>
        public static bool WaitForElementOnPageLoad(this IWebDriver driver, By by, int? timeout = null)
        {
            return driver.Check(by, ElementToBeClickable(by), timeout: timeout);
        }

        /// <summary>
        ///     Returns the value of the text property for the specified element
        ///     Mostly used for retrieving values for input elements(text boxes)
        ///     Catches and handles exceptions that might occur
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <returns></returns>
        public static string GetText(this IWebDriver driver, By by)
        {
            var text = string.Empty;

            driver.Interact(by, ElementIsVisible(by), element =>
            {
                text = element.Text;
            });

            return text;
        }



        /// <summary>
        ///     Returns the value of the selected text property for the specified element
        ///     Mostly used for retrieving values for dropdowns
        ///     Catches and handles exceptions that might occur
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <returns></returns>
        public static string GetSelectedText(this IWebDriver driver, By by)
        {
            var text = string.Empty;

            driver.Interact(by, ElementIsVisible(by), element =>
            {
                text = new SelectElement(element).SelectedOption.Text;
            });

            return text;
        }


        /// <summary>
        ///     Returns the value of the attribute for the specified element
        ///     Catches and handles exceptions that might occur
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static string GetElementAttributeText(this IWebDriver driver, By by, string attribute)
        {
            var text = string.Empty;

            driver.Interact(by, ElementIsVisible(by), element =>
            {
                text = element.GetAttribute(attribute);
            });

            return text;
        }


        /// <summary>
        ///     Returns the value of the attribute for the specified element
        ///     Catches and handles exceptions that might occur
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static string GetElementAttributeText(this IWebDriver driver, IWebElement element,
            string attribute)
        {
            var text = string.Empty;

            driver.Interact(element, webElement =>
            {
                text = element.GetAttribute(attribute);
            });

            return text;
        }

        /// <summary>
        /// Returns list of elements
        /// Takes into account a predefined timeout
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="by"></param>
        public static IReadOnlyCollection<IWebElement> GetElements(this IWebDriver driver, By by)
        {
            IReadOnlyCollection<IWebElement> elementsList = new List<IWebElement>();

            driver.Interact(by, PresenceOfAllElementsLocatedBy(by), elements =>
            {
                elementsList = elements;
            });

            return elementsList;
        }



        // Tries to click an element at offset taking into account a predefined timeout
        // This can generate a variety of exception that are all handled in this method
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void ClickAtOffSet(this IWebDriver driver, By by, int x, int y)
        {
            driver.Interact(by, ElementToBeClickable(by), element =>
            {
                var actions = new OpenQA.Selenium.Interactions.Actions(driver);
                actions.MoveToElement(element, x, y).Click().Build().Perform();

            });
        }

        // Checks element is visible and moves to it, taking into account a predefined timeout
        // This can generate a variety of exception that are all handled in this method
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>

        public static void MoveToElement(this IWebDriver driver, By by, int offsetX = 0, int offsetY = 0)
        {
            driver.Interact(by, ElementIsVisible(by), element =>
            {
                var actions = new OpenQA.Selenium.Interactions.Actions(driver);
                actions.MoveToElement(element, offsetX: offsetX, offsetY: offsetY).Perform();
            });
        }

        // Tries to click an element using a by and innertext to locate the object,
        //taking into account a predefined timeout
        // This can generate a variety of exception that are all handled in this method
        /// <param name="driver"></param>
        /// <param name="by"></param>
        /// <param name="innerText"></param>
        public static void ClickByInnerText(this IWebDriver driver, By by, string innerText)
        {
            driver.Interact(by, VisibilityOfAllElementsLocatedBy(by), elements =>
            {
                elements.FirstOrDefault(e => e.Text == innerText)?.Click();
            });

        }

    }
}
