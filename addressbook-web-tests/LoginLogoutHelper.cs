using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class LoginLogoutHelper
    {
        public  IWebDriver driver = new ChromeDriver();
        public string baseURL;

        public  void Login(AccountData accountData)
        {
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(accountData.Username);
            driver.FindElement(By.Name("pass")).Click();
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(accountData.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }
        public void OpenHomePage()
        {
            driver.Navigate().GoToUrl(baseURL);
        }
        public void Logout()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }
    }
}
