using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
//using OpenQA.Selenium.Support.UI;

namespace ToolsQA.PageObjects
{
    class YandexMainPage
    {
        private readonly IWebDriver _driver;
        public YandexMainPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How= How.XPath,Using = "//div[@class='mail-User-Name']")]
        private IWebElement UserName;

        [FindsBy(How = How.XPath, Using = "//a[@data-metric='Меню сервисов:Выход']")]
        private IWebElement LogOut;

        [FindsBy(How = How.XPath, Using = "//span[@class='mail-ComposeButton-Text']")]
        private IWebElement WriteEmail;

        public void HandleAlerts()
        {
            try
            {
                IAlert Alert = _driver.SwitchTo().Alert();
                String AletrText = Alert.Text;
                Console.WriteLine("Alert text is: " + AletrText);
                Alert.Dismiss();
            }
            catch (Exception) { }
        }
        public void onWriteEmail()
        {
            HandleAlerts();
            WriteEmail.Click();            
        }
        public void OnLogOut()
        {
            UserName.Click();
            LogOut.Click();
        }
    }
}
