using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using OpenQA.Selenium.Appium;

namespace OpenQA.Selenium.Support.PageObjects
{
    //
    // 摘要:
    //     Finds element when the id or the name attribute has the specified value.
    public class ByAutomationIdOrName : By
    {
        private string elementIdentifier = string.Empty;

        private By automationIdFinder;

        private By nameFinder;

        //
        // 摘要:
        //     Initializes a new instance of the OpenQA.Selenium.Support.PageObjects.ByAutomationIdOrName
        //     class.
        //
        // 參數:
        //   elementIdentifier:
        //     The AutomationId or Name to use in finding the element.
        public ByAutomationIdOrName(string elementIdentifier)
        {
            if (string.IsNullOrEmpty(elementIdentifier))
            {
                throw new ArgumentException("element identifier cannot be null or the empty string", "elementIdentifier");
            }

            this.elementIdentifier = elementIdentifier;
            automationIdFinder = MobileBy.AccessibilityId(this.elementIdentifier);
            nameFinder = By.Name(this.elementIdentifier);
        }

        //
        // 摘要:
        //     Find a single element.
        //
        // 參數:
        //   context:
        //     Context used to find the element.
        //
        // 傳回:
        //     The element that matches
        public override IWebElement FindElement(ISearchContext context)
        {
            try
            {
                return automationIdFinder.FindElement(context);
            }
            catch (NoSuchElementException)
            {
                return nameFinder.FindElement(context);
            }
        }

        //
        // 摘要:
        //     Finds many elements
        //
        // 參數:
        //   context:
        //     Context used to find the element.
        //
        // 傳回:
        //     A readonly collection of elements that match.
        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            List<IWebElement> list = new List<IWebElement>();
            list.AddRange(automationIdFinder.FindElements(context));
            list.AddRange(nameFinder.FindElements(context));
            return list.AsReadOnly();
        }

        //
        // 摘要:
        //     Writes out a description of this By object.
        //
        // 傳回:
        //     Converts the value of this instance to a System.String
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "ByAutomationIdOrName([{0}])", elementIdentifier);
        }
    }
}
