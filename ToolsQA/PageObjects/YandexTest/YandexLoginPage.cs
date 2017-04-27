using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

//using OpenQA.Selenium.Support.UI;

namespace ToolsQA.PageObjects
{
    class YandexLoginPage
    {
        private readonly IWebDriver _driver;
        public YandexLoginPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How= How.XPath,Using = "//input[@name='login']")]
        private IWebElement LoginName;

        [FindsBy(How = How.XPath, Using = "//input[@name='passwd']")]
        private IWebElement LoginPassword;

        [FindsBy(How = How.XPath, Using = "//span[@class='_nb-checkbox-flag _nb-checkbox-normal-flag']")]
        private IWebElement NotMyPC;

        [FindsBy(How = How.XPath, Using = "//button[@class=' nb-button _nb-action-button nb-group-start']")]
        private IWebElement SubmitButton;

        [FindsBy(How = How.XPath, Using = "//button[@class=' nb-button _nb-normal-button new-auth-form-button']")]
        private IWebElement Registerbutton;
        
        public void EnterCredentials()
        {

            LoginName.SendKeys("mar-ua-ua@yandex.ru");
            LoginPassword.SendKeys("mar-ua-ua123");
            Console.WriteLine(NotMyPC.GetAttribute("value"));
            NotMyPC.Click();
            SubmitButton.Submit();                        
        }
        public void RegisterNewAccount()
        {            
            Registerbutton.Submit();
           
        }
    }
}
