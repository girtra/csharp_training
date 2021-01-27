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
    public class ManagementMenuHelper : HelperBase
    {
        public ManagementMenuHelper(ApplicationManager manager) : base(manager)
        {
        }

      

        public ManagementMenuHelper OpenManageOverviewPage()
        {
            if (driver.Url == "http://localhost/mantisbt-2.24.4/manage_overview_page.php")
            {
                return this;
            }
                driver.FindElement(By.CssSelector("i.menu-icon.fa.fa-gears")).Click();
            return this;
        }

        public void OpenProjectsListPage()
        {
            OpenManageOverviewPage();
            OpenManageProjectPage();
        }

        public List<ProjectData> GetProjectsList()
        {
            List<ProjectData> list = new List<ProjectData>();
            ICollection<IWebElement> elements = driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr>td:first-child"));
            foreach(var element in elements)
            {
                ProjectData project = new ProjectData()
                {
                    Name = element.Text
                };
                list.Add(project);
            }
            return list;
        }

        public ManagementMenuHelper OpenManageProjectPage()
        {
            if (driver.Url == "http://localhost/mantisbt-2.24.4/manage_proj_page.php")
            {
                return this;
            }
            driver.FindElements(By.CssSelector(".nav-tabs>li"))[2].Click();
            return this;
        }

        public void OpenProjectCreationPage()
        {
            driver.FindElement(By.TagName("input[name=manage_proj_create_page_token] + button")).Click();
        }
        public void InitProjectCreation()
        {
            if (driver.Url == "http://localhost/mantisbt-2.24.4/manage_proj_create_page.php")
            {
                return;
            }
            OpenManageOverviewPage();
            OpenManageProjectPage();
            OpenProjectCreationPage();
        }

        public void OpenProjectPage(int index)
        {
            driver.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr>td:first-child"))[index].FindElement(By.TagName("a")).Click();
        }

        public bool CheckProjectAvailability()
        {
            List<ProjectData> list = GetProjectsList();
            return list.Count != 0;
        }

    }
}
