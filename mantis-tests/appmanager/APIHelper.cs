using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        private AccountData AdminData = new AccountData()
        {
            Name = "administrator",
            Password = "root123"
        };
        public APIHelper(ApplicationManager manager) : base(manager) { }


        public void CreateNewIssue(IssueData issueData, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.category = issueData.Category;
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(AdminData.Name, AdminData.Password, issue);
        }
        public List<ProjectData> GetProjestsList ()
        {
            List<ProjectData> projestList = new List<ProjectData>();
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] projects = client.mc_projects_get_user_accessible(AdminData.Name, AdminData.Password);
            foreach(var project in projects)
            {
                ProjectData l_project = new ProjectData()
                {
                    Id = project.id,
                    Name = project.name
                };
                projestList.Add(l_project);
            }
            return projestList;
        }
        public void CreateNewProject(ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData apiProject = new Mantis.ProjectData();
            apiProject.name = project.Name;
            apiProject.description = project.Description;
            client.mc_project_add(AdminData.Name, AdminData.Password, apiProject);

        }

    }
}
