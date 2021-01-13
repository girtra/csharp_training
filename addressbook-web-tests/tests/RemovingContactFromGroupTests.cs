using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemovingContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void RemovingContactFromGroupTest()
        {
            GroupData group = new GroupData();
            group = group.GetGroupWithContacts()[0];
            List<ContactData> oldContactList = group.GetContacts();
            app.Contacts.RemoveContactFromGroup(group, oldContactList[0]);
            List<ContactData> newContactList = group.GetContacts();

            oldContactList.Remove(oldContactList[0]);

            oldContactList.Sort();
            newContactList.Sort();

            Assert.AreEqual(oldContactList, newContactList);
        }
    }
}
