using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        //public RegistrationHelper Registration { get;  set; }
        //public FtpHelper Ftp { get;  set; }
        public LoginHelper LoginHelper { get;  set; }
        public ProjectManagementHelper PMHelper { get; set; }
        public ManagementMenuHelper ManagMenu { get; set; }

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            baseURL = "http://localhost/mantisbt-2.24.4/";
           // Registration = new RegistrationHelper(this);
            //Ftp = new FtpHelper(this);
            LoginHelper = new LoginHelper(this);
            PMHelper = new ProjectManagementHelper(this);
            ManagMenu = new ManagementMenuHelper(this);
        }
        ~ApplicationManager()
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

        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = "http://localhost/mantisbt-2.24.4/";
                app.Value = newInstance;                
            }
            return app.Value;
        }

        public IWebDriver Driver 
        {
            get
            {
                return driver;
            } 
        }

    }
}
