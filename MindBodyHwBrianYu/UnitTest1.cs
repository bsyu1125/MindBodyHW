using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

// Requires reference to WebDriver.Support.dll
using OpenQA.Selenium.Support.UI;

namespace MindBodyHwBrianYu
{
    [TestClass()]
    public class Driver
    {
        
        [TestInitialize()]
        public void Setup()
        {

            string baseUrl = "http://adam.goucher.ca/parkcalc/";
            PageProperty.driver = new FirefoxDriver();
            PageProperty.driver.Navigate().GoToUrl(baseUrl);
        }
        [TestCleanup()]
        public void Teardown()
        {
            PageProperty.driver.Quit();
        }

        [TestMethod]
        public void ShortTermOneHourTest()
        {
            HomePage page = new HomePage();
            page.selectLot("Short-Term Parking");
            page.setEntryTime("10:00");
            page.setEntryAMPM("PM");
            page.setEntryCalendar("January", "1", "2014");

            page.setExitTime("11:00");
            page.setExitAMPM("PM");
            page.setExitCalendar("January", "1", "2014");
            page.calculate();

            Assert.AreEqual("$ 2.00", page.getCost());
            Assert.IsTrue(page.getDuration().Equals("​(0 Days, 1 Hours, 0 Minutes)"));
        }

        [TestMethod]
        public void LongTermMonth()
        {
            HomePage page = new HomePage();
            page.selectLot("Long-Term Surface Parking");
            page.setEntryCalendar("January", "1", "2014");

            page.setExitCalendar("February", "1", "2014");
            page.calculate();

            Assert.AreEqual("$ 270.00", page.getCost());
            Assert.IsTrue(page.getDuration().Equals("​(31 Days, 0 Hours, 0 Minutes)"));
        }

        [TestMethod]
        public void exitBeforeEntryError()
        {
            HomePage page = new HomePage();
            page.selectLot("Short-Term Parking");
            page.setEntryCalendar("January", "2", "2014");

            page.setExitCalendar("January", "1", "2014");
            page.calculate();

            Assert.IsTrue(page.getCost().Equals("E​RROR! YOUR EXIT DATE OR TIME IS BEFORE YOUR ENTRY DATE OR TIME"));        
        }


    }
}
