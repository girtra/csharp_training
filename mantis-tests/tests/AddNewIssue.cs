using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssue : TestBase
    {
        [Test]
        public void AddIssue()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root123"
            };
            ProjectData project = new ProjectData()
            {
                Id = "28"
            };
            IssueData issueData = new IssueData()
            {
                Category = "General",
                Summary = "summary short tex",
                Description = "some long description"
            };


            app.API.CreateNewIssue(issueData, project);

        }
    }
}
