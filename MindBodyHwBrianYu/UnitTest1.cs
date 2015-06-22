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
        IWebDriver driver;

        [TestInitialize()]
        public void Setup()
        {
            driver = new FirefoxDriver();
            System.Console.WriteLine("created the page!");
        }
        [TestCleanup()]
        public void Teardown()
        {
            driver.Quit();
        }

        [TestMethod]
        public void FirstTest()
        {
            Assert.AreEqual(0, 1);
        }

        [TestMethod]
        public void Sometest()
        {
            Assert.IsTrue(true);
        }
    }
    
}
