using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
           if (!app.Contacts.IsContactPresent())
            {
                ContactData defContactData = new ContactData("def");
                app.Contacts.Create(defContactData);
            }

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            ContactData newContactData = new ContactData("NewName");
            newContactData.Lastname = "newLastname";
            newContactData.Middlename = "newMiddlename";
            app.Contacts.Modify(0, newContactData);
            List<ContactData> newContacts = app.Contacts.GetContactList();

            oldContacts[0] = newContactData;
            newContacts.Sort();
            oldContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
