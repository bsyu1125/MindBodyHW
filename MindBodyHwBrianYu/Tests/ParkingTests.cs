using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
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
            //PageProperty.driver.Quit();
        }

        [TestMethod]
        public void case1()
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

            string expected = "​(0 Days, 1 Hours, 0 Minutes)".Replace(((char)(8203)).ToString(), "");
            Assert.AreEqual("$ 2.00", page.getCost());
            Assert.AreEqual(page.getDuration(), expected);
        }

        [TestMethod]
        public void case2()
        {
            HomePage page = new HomePage();
            page.selectLot("Long-Term Surface Parking");
            page.setEntryCalendar("January", "1", "2014");
            page.setExitCalendar("February", "1", "2014");

            page.calculate();
            string expected = "​(31 Days, 0 Hours, 0 Minutes)".Replace(((char)(8203)).ToString(), "");

            Assert.AreEqual("$ 270.00", page.getCost());
            Assert.AreEqual(page.getDuration(), expected);
        }

        [TestMethod]
        public void case3()
        {
            HomePage page = new HomePage();
            page.selectLot("Short-Term Parking");
            page.setEntryCalendar("January", "2", "2014");
            page.setExitCalendar("January", "1", "2014");

            page.calculate();

            string expected = "E​RROR! YOUR EXIT DATE OR TIME IS BEFORE YOUR ENTRY DATE OR TIME".Replace(((char)(8203)).ToString(), "");

            Assert.AreEqual(page.getCost(), expected);        
        }

        [TestMethod]
        // Entry time is after exit time
        // Fails
        public void case4()
        {
            HomePage page = new HomePage();
            page.selectLot("Short-Term Parking");
            page.setEntryTime("5:00");
            page.setEntryAMPM("AM");
            page.setEntryCalendar("January", "1", "2014");

            page.setExitTime("4:00");
            page.setExitAMPM("AM");
            page.setExitCalendar("January", "1", "2014");
            page.calculate();

            string expected = "E​RROR! YOUR EXIT DATE OR TIME IS BEFORE YOUR ENTRY DATE OR TIME".Replace(((char)(8203)).ToString(), "");
            Assert.AreEqual(page.getCost(), expected);
        }

        [TestMethod]
        // Same time and date calcualtes correctly
        public void case5()
        {
            HomePage page = new HomePage();
            page.selectLot("Short-Term Parking");
            page.setEntryTime("5:00");
            page.setEntryAMPM("AM");
            page.setEntryCalendar("January", "1", "2014");

            page.setExitTime("5:00");
            page.setExitAMPM("AM");
            page.setExitCalendar("January", "1", "2014");
            page.calculate();

            string expected = "​(0 Days, 0 Hours, 0 Minutes)".Replace(((char)(8203)).ToString(), "");
            Assert.AreEqual("$ 2.00", page.getCost());
            Assert.AreEqual(page.getDuration(), expected);
        }

        [TestMethod]
        // Negative values for time
        // FAILS
        public void case6()
        {
            HomePage page = new HomePage();
            page.selectLot("Short-Term Parking");
            page.setEntryTime("-1:00");
            page.setEntryAMPM("AM");
            page.setEntryCalendar("January", "1", "2014");

            page.setExitAMPM("AM");
            page.setExitCalendar("January", "1", "2014");
            page.calculate();

            string expected = "E​RROR! YOUR EXIT DATE OR TIME IS INVALID!".Replace(((char)(8203)).ToString(), "");
            Assert.AreEqual(page.getCost(), expected);
        }

        [TestMethod]
        public void case7()
        {
            HomePage page = new HomePage();
            page.selectLot("Short-Term Parking");
            page.setEntryTime("1:00");
            page.setEntryDate("-1/1/2014");

            page.setExitDate("1/1/2014");
            page.calculate();

            string expected = "E​RROR! YOUR EXIT DATE OR TIME IS INVALID!".Replace(((char)(8203)).ToString(), "");
            Assert.AreEqual(page.getCost(), expected); 
        }


        [TestMethod]
        // Blank Fields for Time
        //Fails
        public void case8()
        {
            HomePage page = new HomePage();
            page.selectLot("Short-Term Parking");
            page.setEntryTime("");
            page.setEntryAMPM("AM");
            page.setEntryCalendar("January", "1", "2014");

            page.setExitAMPM("AM");
            page.setExitCalendar("January", "1", "2014");
            page.calculate();

            string expected = "E​RROR! YOUR EXIT DATE OR TIME IS INVALID!".Replace(((char)(8203)).ToString(), "");
            Assert.AreEqual(page.getCost(), expected);
        }

        
    }
}
