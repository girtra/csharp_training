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
            GroupData newData = new GroupData("eee5");
            newData.Header = null;
            newData.Footer = "new5";          

            if (!app.Groups.IsGroupPresent()) 
            {
                GroupData defData = new GroupData("def");
                app.Groups.Create(defData);
            }
            app.Groups.Modify(1, newData);
        }
    }
}
