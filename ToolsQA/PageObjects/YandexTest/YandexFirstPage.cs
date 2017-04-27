using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ToolsQA.PageObjects
{
    class YandexFirstPage
    {
        private readonly IWebDriver _driver;
        public YandexFirstPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How= How.XPath,Using = "//div[@class='b-inline']")]
        private IWebElement EnterToEmail;
        public void GoToLoginPage()
        {
            _driver.Url = "https://ya.ru/";
            EnterToEmail.Click();
        }
    }
}
