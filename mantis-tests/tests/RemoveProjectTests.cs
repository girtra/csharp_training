using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class RemoveProjectTests : AuthTestBase
    {
        [Test]
        public void RemoveProjectTest()
        {
            app.ManagMenu.OpenProjectsListPage();
            if (!app.ManagMenu.CheckProjectAvailability())
            {
                Random rnd = new Random();
                ProjectData project = new ProjectData()
                {
                    Name = "testProjectName" + rnd.Next(1000),
                    Description = "testDescription" + rnd.Next(1000)
                };
                app.ManagMenu.InitProjectCreation();
                app.PMHelper.CreateNewProject(project);
                app.ManagMenu.OpenProjectsListPage();
            }
            
            List<ProjectData> oldList = app.ManagMenu.GetProjectsList();
            app.ManagMenu.OpenProjectPage(0);
            app.PMHelper.RemoveProject();
            app.ManagMenu.OpenProjectsListPage();
            Task.Delay(1000).Wait();
            List<ProjectData> newList = app.ManagMenu.GetProjectsList();
            oldList.RemoveAt(0);
            Assert.AreEqual(oldList, newList);


        }

    }
}
