using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MindBodyHwBrianYu
{
    enum PropertyType
    {
        Id,
        Name,
        Value,
        LinkText,
        CssName,
        ClassName
    }

    class PageProperty
    {
        public static IWebDriver driver { get; set; }
        public static string baseUrl = "http://adam.goucher.ca/parkcalc/";          
    }
}
