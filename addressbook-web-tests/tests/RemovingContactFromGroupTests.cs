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
            var groupData = new GroupData();
            var groups = groupData.GetGroupWithContacts();
            if (groups.Count == 0 || groupData == null) 
            {
                if (!app.Groups.IsGroupPresent())
                {
                    GroupData defData = new GroupData("def1");
                    app.Groups.Create(defData);
                }
                groupData = GroupData.GetAll().FirstOrDefault();

                ContactData contact = ContactData.GetAll().FirstOrDefault();
                if (contact == null)
                {
                    app.Contacts.Create((new ContactData() { Firstname = "testName1" }));
                }                
                app.Contacts.AddContactToGroup((ContactData.GetAll().FirstOrDefault()), groupData);
            }

            
            List <ContactData> oldContactList = groupData.GetContacts();
            app.Contacts.RemoveContactFromGroup(groupData, oldContactList[0]);
            List<ContactData> newContactList = groupData.GetContacts();

            oldContactList.Remove(oldContactList[0]);

            oldContactList.Sort();
            newContactList.Sort();

            Assert.AreEqual(oldContactList, newContactList);
        }
    }
}
