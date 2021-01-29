using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddingContactToGroupTest()
        {
            if (!app.Groups.IsGroupPresent())
            {
                GroupData defData = new GroupData("def");
                app.Groups.Create(defData);
            }
            GroupData group = GroupData.GetAll().FirstOrDefault();

            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).FirstOrDefault();

            if(contact == null)
            {
                contact = new ContactData { Firstname = "defTestFirstName2" };
                app.Contacts.Create(contact);
                contact = ContactData.GetAll().FirstOrDefault();
            }

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
