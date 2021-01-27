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
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void CreateNewProject(ProjectData project)
        {
            FillNewProjectForm(project);
            SubmitCreation();
        }

        public void FillNewProjectForm(ProjectData project)
        {
            Type(By.Id("project-name"), project.Name);
            Type(By.Id("project-description"), project.Description);           
        }
        public void SubmitCreation()
        {
            driver.FindElement(By.TagName("input[type=submit]")).Click();
        }
        public void RemoveProject()
        {
            driver.FindElement(By.CssSelector("#project-delete-div  input[type=submit]")).Click();
            Task.Delay(1000).Wait();
            driver.FindElement(By.CssSelector("input[type=submit]")).Click();
        }

        public bool IsCreationError()
        {
            string errorMessege = driver.FindElement(By.CssSelector("div.alert.alert-danger")).Text;
            return errorMessege.Contains("APPLICATION ERROR");
        }
    }
}
