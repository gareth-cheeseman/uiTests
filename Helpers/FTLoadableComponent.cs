using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace FunctionalTests.Helpers
{
    public abstract class FTLoadableComponent<T> : ILoadableComponent where T : FTLoadableComponent<T>
    {
        /// <summary>
        ///     Gets or sets the message for the exception thrown when a component cannot be loaded
        /// </summary>
        public virtual string UnableToLoadMessage { get; set; }

        /// <summary>
        ///     Gets a value indicating whether the component is fully loaded.
        /// </summary>
        /// <remarks>
        ///     When the component is loaded, this property will return true or false depending on
        ///     the execution of <see cref="EvaluateLoadedStatus" /> to indicate the not loaded state.
        /// </remarks>
        protected bool IsLoaded
        {
            get
            {
                var isLoaded = false;
                try
                {
                    isLoaded = EvaluateLoadedStatus();
                }
                catch (WebDriverException)
                {
                    return false;
                }

                return isLoaded;
            }
        }

        /// <summary>
        ///     Ensure that the component is currently loaded.
        /// </summary>
        /// <returns>The loaded <see cref="ILoadableComponent" /> instance.</returns>
        ILoadableComponent ILoadableComponent.Load()
        {
            return Load();
        }

        /// <summary>
        ///     Ensure that the component is currently loaded.
        /// </summary>
        /// <returns>The loaded component.</returns>
        /// <remarks>This is equivalent to the Get() method in Java version.</remarks>
        public virtual T Load()
        {
            if (IsLoaded)
                return (T)this;
            TryLoad();

            if (!IsLoaded)
            {
                //ExtentTest test = ScenarioContext.Current.Get<ExtentTest>();
                var driver = ScenarioContext.Current.Get<IWebDriver>();
                //test.Error("LoadableComponentException caught during page load: " + this.UnableToLoadMessage, MediaEntityBuilder.CreateScreenCaptureFromPath(ReportingMethods.CreateScreenshot(driver)).Build());
                throw new LoadableComponentException(UnableToLoadMessage);
            }

            return (T)this;
        }

        /// <summary>
        ///     HandleLoadError gives a subclass the opportunity to handle a <see cref="WebDriverException" /> that occurred
        ///     during the execution of <see cref="ExecuteLoad" />.
        /// </summary>
        /// <param name="ex">The exception which occurs on load.</param>
        protected virtual void HandleLoadError(WebDriverException ex)
        {
        }

        /// <summary>
        ///     When this method returns, the component modeled by the subclass should be fully loaded. This
        ///     subclass is expected to navigate to an appropriate page or trigger loading the correct HTML
        ///     should this be necessary.
        /// </summary>
        protected abstract void ExecuteLoad();

        /// <summary>
        ///     Determine whether or not the component is loaded. Subclasses are expected to provide the details
        ///     to determine if the page or component is loaded.
        /// </summary>
        /// <returns>A boolean value indicating if the component is loaded.</returns>
        protected abstract bool EvaluateLoadedStatus();

        /// <summary>
        ///     Attempts to load this component, providing an opportunity for the user to handle any errors encountered
        ///     during the load process.
        /// </summary>
        /// <returns>A self-reference to this <see cref="LoadableComponent{T}" /></returns>
        protected T TryLoad()
        {
            try
            {
                ExecuteLoad();
            }
            catch (WebDriverException e)
            {
                HandleLoadError(e);
            }

            return (T)this;
        }
    }
}
