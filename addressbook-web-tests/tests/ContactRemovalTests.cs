using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {

            if (!app.Contacts.IsContactPresent())
            {
                ContactData defContactData = new ContactData("def");
                app.Contacts.Create(defContactData);
            }
            List<ContactData> oldContacts = app.Contacts.GetContactList();
            app.Contacts.Remove(0);
            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.RemoveAt(0);
            newContacts.Sort();
            oldContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void ContactRemovalTestFromEditForm()
        {
            if (!app.Contacts.IsContactPresent())
            {
                ContactData defContactData = new ContactData("def");
                app.Contacts.Create(defContactData);
            }

            List<ContactData> oldContacts = app.Contacts.GetContactList();
            app.Contacts.RemoveFromEditForm(0);
            app.Navigator.GoToHomePage();
            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.RemoveAt(0);
            newContacts.Sort();
            oldContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
