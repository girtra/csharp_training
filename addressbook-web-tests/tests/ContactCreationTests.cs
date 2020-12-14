using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {         
            List<ContactData> oldContacts = app.Contacts.GetContactList();
            
            ContactData contactData = new ContactData("Name4");
            contactData.Lastname = "lastname4";
            app.Contacts.Create(contactData);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);
            oldContacts.Add(contactData);

            newContacts.Sort();
            oldContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }       
    }
}
