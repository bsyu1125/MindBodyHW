using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindBodyHwBrianYu
{
    class CalendarPage
    {
        public CalendarPage()
        {
            PageFactory.InitElements(PageProperty.driver, this);
        }

        [FindsBy(How = How.Name, Using = "MonthSelector")]
        private IWebElement monthSelector = null;

        [FindsBy(How = How.XPath, Using = "//font[text()=\"<\"]")]
        private IWebElement backYear = null;

        [FindsBy(How = How.XPath, Using = "//font[text()=\">\"]")]
        private IWebElement forwardYear = null;

        public void select(string month, string day, string year)
        {
            new SelectElement(monthSelector).SelectByText(month);

            string cur_year = DateTime.Now.Year.ToString();
            int diff = Int32.Parse(cur_year) - Int32.Parse(year);

            while (diff < 0)
            {
                backYear.Click();
                diff++;
            }

            while (diff > 0)
            {
                forwardYear.Click();
                diff--;
            }

            PageProperty.driver.FindElement(By.LinkText(day)).Click();
        }
    }
}
