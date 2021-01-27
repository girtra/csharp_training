using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {        
        public LoginHelper(ApplicationManager manager) 
            : base (manager)
        {
        }
        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                    return;
            }
            EnterUserName(account);
            EnterUserPassword(account);
        }

        private void EnterUserPassword(AccountData account)
        {
            Type(By.Id("password"), account.Password);
            driver.FindElement(By.CssSelector("input[type=submit]")).Click();
        }

        public void EnterUserName(AccountData account)
        {
            Type(By.Id("username"), account.Name);
            driver.FindElement(By.CssSelector("input[type=submit]")).Click();
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.ClassName("user-info"));
        }
      
        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.CssSelector("span.user-info")).Click();
                driver.FindElement(By.CssSelector("ul.user-menu.dropdown-menu.dropdown-menu-right")).FindElements(By.TagName("li"))[3].Click();           
            }            
        }
    }
}
