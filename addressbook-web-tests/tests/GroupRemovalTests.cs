using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            if (!app.Groups.IsGroupPresent())
            {
                GroupData defData = new GroupData("def");
                app.Groups.Create(defData);
            }
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(0);

            List<GroupData> newGroups = app.Groups.GetGroupList(); 
            oldGroups.RemoveAt(0);
            
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
