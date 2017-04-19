using System;
using OpenQA.Selenium;
using NUnit.Framework;
using ToolsQA.Utilities;
using ToolsQA.PageObjects;
using NLog;

namespace ToolsQA.Tests
{
    class NUnitTests
    {
        //private static Logger logger = LogManager.GetCurrentClassLogger(); //
        Logger logger = LogManager.GetLogger("MyClassName");
        private IWebDriver _driver;
        [SetUp]
        public void SetupTest() { }

        [TearDown]
        public void TearDownTest() { }       
           
        [Test]
        //Test #1. Open Google. Search for “automation”. Open the first link on search results page.
        //Verify that title contains searched word.
        public void GoogleTest1()
        {
            String sPattern = TestConfiguration.Get<String>("PatternToSearch");
            logger.Info("Executed testcase is: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            logger.Info("Open Browser.");
            LogManager.GetLogger("MyClassName");
            _driver = new DriverRoutine().Create();
            // Load Google and search for needed text;
            logger.Info("Open Google. Search for \"" + sPattern + "\"");
            new FirstPage(_driver).EnterSearchPattern(sPattern);
            logger.Info("Open the first link on search results page.");
            String sActualTitleOfFirstLink =  new FirstPage(_driver).GetTitleOfFirstLink();
            logger.Info("The title of first page is \"" + sActualTitleOfFirstLink + "\"");
            logger.Info("Close instance of driver.");
            new DriverRoutine().Dismiss(_driver);            
            Boolean bPASSED = sActualTitleOfFirstLink.Contains(sPattern);
            logger.Info("Verify got result: EXPECTED \"" + sPattern + "\" must be in ACTUAL \"" + sActualTitleOfFirstLink + "\"");
            if (!bPASSED)
            {
                logger.Error("TEST FAILED!!!");
            }
            else
            {
                logger.Info("TEST PASSED!!!");
            }
            Assert.True(bPASSED);
        }

        [Test]
        //Test #2. Open Google. Search for “automation”.
        //Verify that there is expected domain (“testautomationday.com”) on search results  pages (page: 1-5).
        public void GoogleTest2()
        {
            logger.Info("Executed testcase is: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
            String sPattern = TestConfiguration.Get<String>("PatternToSearch");
            String sDomainTosaerch = TestConfiguration.Get<String>("sDomainTosaerch");
            Byte iPages = TestConfiguration.Get<Byte>("iPagesToSearchAt");
            logger.Info("Test parameters:   PatternToSearch = " + sPattern);
            logger.Info("                   sDomainTosaerch = " + sDomainTosaerch);
            logger.Info("                   iPagesToSearchAt = " + iPages);
            logger.Info("Open Browser.");
            _driver = new DriverRoutine().Create();
            // Load Google and search for needed text;
            logger.Info("Open Google. Search for \"" + sPattern + "\"");
            new FirstPage(_driver).EnterSearchPattern(sPattern);
            //List<FoundElement> lResults = new FirstPage(_driver).GetResults(iPages);           

            logger.Info("Get all found results form first "+ iPages + " result pages");
            String sFound = new FirstPage(_driver).SearchForDomain(sDomainTosaerch, iPages);
            logger.Info("Close instance of driver.");
            new DriverRoutine().Dismiss(_driver);            
            Boolean bPASSED = sFound != "";
            logger.Info("Searched domain \""+ sDomainTosaerch + " was expected on first " + iPages + " result pages...");
            if (!bPASSED)
            {
                logger.Error("...but it was not! TEST FAILED!!!");
            }
            else
            {
                logger.Info("...and was found! TEST PASSED!!!");
            }
            Assert.True(bPASSED);
        }
    }
}
