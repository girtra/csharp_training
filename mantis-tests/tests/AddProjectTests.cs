using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace mantis_tests 
{
    [TestFixture]
    public class AddProjectTests : AuthTestBase
    {
        [Test]
        public void AddProjectTest()
        {
            Random rnd = new Random();
            ProjectData project = new ProjectData()
            {
                Name = "testProjectName" + rnd.Next(1000),
                Description = "testDescription"
            };

            app.ManagMenu.OpenProjectsListPage();

            List<ProjectData> oldList =  app.ManagMenu.GetProjectsList();

            app.ManagMenu.InitProjectCreation();
            app.PMHelper.CreateNewProject(project);
            app.ManagMenu.OpenProjectsListPage();
            List<ProjectData> newList = app.ManagMenu.GetProjectsList();
            oldList.Add(project);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);

        }
        [Test]
        public void AddSameNameProjectTest()
        {
            ProjectData project = new ProjectData()
            {
                Name = "testProjectSameName2"
            };

            app.ManagMenu.OpenProjectsListPage();
            app.ManagMenu.InitProjectCreation();
            app.PMHelper.CreateNewProject(project);
            app.ManagMenu.OpenProjectsListPage();
            List<ProjectData> listAfterFirstCreation = app.ManagMenu.GetProjectsList();

            app.ManagMenu.OpenProjectsListPage();
            app.ManagMenu.InitProjectCreation();
            app.PMHelper.CreateNewProject(project);


            Assert.IsTrue(app.PMHelper.IsCreationError());

            app.ManagMenu.OpenProjectsListPage();
            List<ProjectData> listAfterSecondCreation = app.ManagMenu.GetProjectsList();
            Assert.AreEqual(listAfterFirstCreation, listAfterSecondCreation);


        }

    }
}
