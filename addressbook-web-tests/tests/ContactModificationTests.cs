using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
           if (!app.Contacts.IsContactPresent())
            {
                ContactData defContactData = new ContactData("def");
                app.Contacts.Create(defContactData);
            }

            List<ContactData> oldContacts = ContactData.GetAll();

            ContactData newContactData = new ContactData("NewName");
            newContactData.Lastname = "newLastname";
            newContactData.Middlename = "newMiddlename";

            ContactData oldContactData = oldContacts[0];

            app.Contacts.Modify(oldContactData.Id, newContactData);
            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts[0] = newContactData;
            newContacts.Sort();
            oldContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
