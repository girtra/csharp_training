using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {  
            if (!app.Groups.IsGroupPresent()) 
            {
                GroupData defData = new GroupData("def");
                app.Groups.Create(defData);
            }

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            GroupData newData = new GroupData("ee99e5");
            newData.Header = null;
            newData.Footer = "new5";
            app.Groups.Modify(0, newData);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0] = newData;
            newGroups.Sort();
            oldGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
