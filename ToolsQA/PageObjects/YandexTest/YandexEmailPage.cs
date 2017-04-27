using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using ToolsQA.Utilities;
using System.Threading;

namespace ToolsQA.PageObjects
{
    class YandexEmailPage
    {
        private readonly IWebDriver _driver;
        Int32 ExplicitlyWait = TestConfiguration.Get<Int32>("ExplicitlyWait");
        public YandexEmailPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How= How.XPath,Using = "//div[@name='to']")]
        private IWebElement WriteTo;

        [FindsBy(How = How.XPath, Using = "//input[@name='subj']")]
        private IWebElement EmailSubject;

        public const String BodyxPath = "//div[@id='cke_1_contents']/textarea";        
        [FindsBy(How = How.XPath, Using = BodyxPath)]
        private IWebElement EmailBody;

        [FindsBy(How =How.XPath,Using = BodyxPath+ "/ancestor-or-self::label/../div//button")]
        private IWebElement SendEmail;
        public void OnSend()
        {
            SendEmail.Click();
        }
        private void FillBody(String Body)
        {            
            EmailBody.Clear();
            EmailBody.SendKeys(Body);
        }
        public void CreateEmail(String EmailTo,String Subject, String Body)
        {
            WriteTo.SendKeys(EmailTo);
            EmailSubject.SendKeys(Subject);
            FillBody(Body);            
            OnSend();
        }
    }
}
