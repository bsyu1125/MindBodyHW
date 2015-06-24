using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;


namespace MindBodyHwBrianYu
{
    public class HomePage
    {
        public HomePage()
        {
            PageFactory.InitElements(PageProperty.driver, this);
        }

        [FindsBy(How = How.Name, Using = "Lot")]
        private IWebElement lot = null;

        [FindsBy(How = How.Name, Using = "EntryTime")]
        public IWebElement entryTime = null;

        [FindsBy(How = How.Name, Using = "EntryDate")]
        private IWebElement entryDate = null;

        [FindsBy(How = How.Name, Using = "ExitTime")]
        private IWebElement exitTime = null;

        [FindsBy(How = How.Name, Using = "ExitDate")]
        private IWebElement exitDate = null;

        [FindsBy(How = How.Name, Using = "EntryTimeAMPM")]
        private IList<IWebElement> entryTimeAMPM = null;

        [FindsBy(How = How.Name, Using = "ExitTimeAMPM")]
        private IList<IWebElement> exitTimeAMPM = null;

        [FindsBy(How = How.CssSelector, Using = "img[alt=\"Pick a date\"]")]
        private IList<IWebElement> calendarIcon = null;

        [FindsBy(How = How.Name, Using = "Submit")]
        private IWebElement submit = null;

        [FindsBy(How = How.TagName, Using = "b")]
        private IWebElement result = null;

        [FindsBy(How = How.CssSelector, Using = "span.BodyCopy > font > b")]
        private IWebElement duration = null;

        public string getEntryTime()
        {
            return entryTime.GetAttribute("value");
        }

        public string getEntryDate()
        {
            return entryDate.GetAttribute("value");
        }

        public string getExitTime()
        {
            return exitTime.GetAttribute("value");
        }

        public string getExitDate()
        {
            return exitDate.GetAttribute("value");
        }

        public string getCost()
        {
            return result.Text.Trim();
        }

        public string getDuration()
        {
            return duration.Text.Trim();
        }

        public void selectLot(string value)
        {
            new SelectElement(lot).SelectByText(value);
        }

        public void setEntryTime(string value)
        {
            entryTime.Clear();
            entryTime.SendKeys(value);
        }

        public void setExitTime(string value)
        {
            exitTime.Clear();
            exitTime.SendKeys(value);
        }

        public void calculate()
        {
            submit.Click();
        }

        public void setEntryAMPM(string ampm)
        {
            if (ampm.Equals("AM"))
            {
                entryTimeAMPM[0].Click();
            }
            else
            {
                entryTimeAMPM[1].Click();
            }
        }

        public void setExitAMPM(string ampm)
        {
            if (ampm.Equals("AM"))
            {
                exitTimeAMPM[0].Click();
            }
            else
            {
                exitTimeAMPM[1].Click();
            }
        }

        public void setEntryDate(string date)
        {
            entryDate.Clear();
            entryDate.SendKeys(date);
        }

        public void setExitDate(string date)
        {
            exitDate.Clear();
            exitDate.SendKeys(date);
        }

        public void setEntryCalendar(string month, string day, string year)
        {
            calendarIcon[0].Click();
            selectCalendarDate(month, day, year);
        }

        public void setExitCalendar(string month, string day, string year)
        {
            calendarIcon[1].Click();
            selectCalendarDate(month, day, year);
        }

        public void selectCalendarDate(string month, string day, string year)
        {
            string currentWindow = PageProperty.driver.CurrentWindowHandle;
            foreach (string handle in PageProperty.driver.WindowHandles)
            {
                if (handle != PageProperty.driver.CurrentWindowHandle)
                {
                    PageProperty.driver.SwitchTo().Window(handle);
                    break;
                }
            }

            CalendarPage calendarPage = new CalendarPage();
            calendarPage.select(month, day, year);

            PageProperty.driver.SwitchTo().Window(currentWindow);
        }
    }
}
