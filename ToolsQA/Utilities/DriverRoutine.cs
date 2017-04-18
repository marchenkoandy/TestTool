using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Threading;

namespace ToolsQA.Utilities
{
    class DriverRoutine
    {
        public enum DriverToUSe { Firefox, Chrome, InternetExplorer }        
        public IWebDriver Create()
        {
            DriverToUSe eDriver = TestConfiguration.Get<DriverToUSe>("DriverToUse");
            IWebDriver driver;
            switch (eDriver)
            {
                case DriverToUSe.Chrome:
                    driver = new ChromeDriver();
                    break;
                case DriverToUSe.Firefox:
                    driver = new FirefoxDriver();
                    break;
                case DriverToUSe.InternetExplorer:
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Int32 ImplicitlyWait = TestConfiguration.Get<Int32>("ImplicitlyWait");
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(ImplicitlyWait));
            return driver;                            
        }
        public void Dismiss(IWebDriver driver)
        {
            driver.Close();
            if (driver != null)
            {
                try
                {
                    driver.Quit();
                }
                catch (Exception)
                {
                    //Nothing could be done if browser did not closed
                }
            }
        }
    }
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, byte iTries = 10)
        {
            IWebElement FirstElement = driver.FindElement(By.XPath("//*"));
            IWebElement Needed = FirstElement.FindElement(by, iTries);
            return Needed;
        }
        public static IWebElement FindElement(this IWebElement Parent, By by, Byte iTries=10)
        {
            if (iTries == 0)
            {
                return null;
            }                    
            try
            {
                return Parent.FindElement(by);
            }
            catch
            {
                Console.WriteLine("Attempting to recover from StaleElementReferenceException..."+by);
                Thread.Sleep(100);
                return Parent.FindElement(by, --iTries);
            }            
        }
    }
}
