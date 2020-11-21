using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        protected LoginHelper loginHelper;
        protected NavigationHelper navigator;
        protected GroupHalper groupHalper;
        protected ContactHelper contactHelper;

        public ApplicationManager()
        {
            loginHelper = new LoginHelper(driver);
            navigator = new NavigationHelper(driver, baseURL);
            groupHalper = new GroupHalper(driver);
            contactHelper = new ContactHelper(driver);

            driver = new ChromeDriver();
            baseURL = "http://localhost/addressbook/";
        }

        public void Stop()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }

        public NavigationHelper Navigator
        {
            get
            {
                return navigator;
            }
        }

        public GroupHalper Groups
        {
            get
            {
                return groupHalper;
            }
        }

        public ContactHelper Contacts
        {
            get
            {
                return contactHelper;
            }
        }
    }
}
