using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using ToolsQA.Utilities;

namespace ToolsQA.PageObjects
{
    class FoundElement
    {
        public IWebElement hRef;
        public String Header;
        public String Url;
        public String Text;
    }
    class FirstPage
    {
        const String RESULTS_DIV_xPATH = "//*[@id='rso']/div";
        const String Delimiter = "==========================================================================================================================================";
        const String xPathToSearch = RESULTS_DIV_xPATH + "/div/div";
        const String xPathToSearchResultHeader = ".//div/h3/a";
        const String xPathToSearchResultUrl = ".//cite[@class='_Rm']";
        const String xPathToSearchResultText = ".//span[@class='st']";
        const String xPathCurrentPage = "//td[@class='cur']";
        const String xPAthToNextResulPage = xPathCurrentPage + "/following-sibling::td[1]/a";
        const Byte iTries = 5;

        public String Url = TestConfiguration.Get<String>("URL");
        Int32 ExplicitlyWait = TestConfiguration.Get<Int32>("ExplicitlyWait");

        private readonly IWebDriver _driver;

        public FirstPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.Id, Using = "lst-ib")]
        public IWebElement SearchTextField { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@id='_fZl']/span")]
        public IWebElement SearchButton { get; set; }

        [FindsBy(How = How.XPath, Using = RESULTS_DIV_xPATH)]
        public IWebElement AnyResults { get; set; }

        [FindsBy(How = How.XPath, Using = xPathCurrentPage)]
        public IWebElement CurrentPage { get; set; }
        
        [FindsBy(How = How.XPath, Using = xPAthToNextResulPage)]
        public IWebElement NextPage { get; set; }

        public void EnterSearchPattern(String toSearch)
        {
            _driver.Url = Url;
            SearchTextField.SendKeys(toSearch);
            SearchButton.Click();                       
        }

        public List<FoundElement> GetResults(byte iPages)
        { 
            List<FoundElement> lFULL = new List<FoundElement>();
            if (AnyResults.Displayed && AnyResults.Enabled)
            {
                Int32 iPage = Convert.ToByte(CurrentPage.Text); ;
                do
                {  
                    List < FoundElement > lOut = new List<FoundElement>();
                    ReadOnlyCollection<IWebElement> lFoundResults = _driver.FindElements(By.XPath(xPathToSearch));
                    Int32 liItems = lFoundResults.Count;
                    Byte iItem = 1;                    
                    Console.WriteLine(Delimiter);
                    Console.WriteLine("Working with Page " + iPage + " of " + iPages);
                    Console.WriteLine(Delimiter);
                    foreach (IWebElement Res in lFoundResults)
                    {
                        FoundElement Current = new FoundElement();
                        Console.Write("Item: " + iItem++ + " of " + liItems);
                        Current.hRef = Res.FindElement(By.XPath(xPathToSearchResultHeader));
                        Current.Header = Current.hRef.Text;
                        Current.Url = Res.FindElement(By.XPath(xPathToSearchResultUrl)).Text;
                        Current.Text = Res.FindElement(By.XPath(xPathToSearchResultText)).Text;                                                
                    }
                    LogResults(lOut);
                    lFULL.AddRange(lOut);
                    if (iPage < iPages)
                    {
                        NextPage.Click();                        
                        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(ExplicitlyWait));
                        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath(xPathCurrentPage), (iPage + 1).ToString()));
                    }
                } while (++iPage <= iPages);
            }
            return lFULL;
        }

        public string GetTitleOfFirstLink()
        {
            IWebElement FirstLink = _driver.FindElement(By.XPath(xPathToSearchResultHeader));            
            FirstLink.Click();
            return _driver.Title.ToString();
        }

        public String SearchForDomain(String sDomainTosaerch, byte iPages)
        {
            String sOut = "";
            {
                Int32 iPage = Convert.ToByte(CurrentPage.Text);
                do
                {
                    List<FoundElement> lOut = new List<FoundElement>();
                    ReadOnlyCollection<IWebElement> lFoundResults = _driver.FindElements(By.XPath(xPathToSearchResultUrl));                    
                    foreach (IWebElement Res in lFoundResults)
                    {
                        String sCurrent = Res.Text;
                        if (sCurrent.Contains(sDomainTosaerch))
                        {
                            sOut = sCurrent;
                            break;
                        }
                    }
                    if (iPage < iPages)
                    {
                        NextPage.Click();
                        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(ExplicitlyWait));
                        wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath(xPathCurrentPage), (iPage + 1).ToString()));
                    }
                } while (++iPage <= iPages);
                
            }
            return sOut;
        }

        public void LogResults(List<FoundElement> lToPrint)
        {
            Int32 iCount = lToPrint.Count;
            Console.WriteLine(Delimiter);
            for (Int32 i = 0; i <= iCount-1; i++)
            {
                Console.Write("Elemet " + Convert.ToString(i + 1));
                Console.Write(";    Header " + lToPrint[i].Header);
                Console.Write(";    Url " + lToPrint[i].Url);
                Console.WriteLine(";    Text " + lToPrint[i].Text);
            }
            Console.WriteLine(Delimiter);
            Console.WriteLine("");
        }
    }
}
