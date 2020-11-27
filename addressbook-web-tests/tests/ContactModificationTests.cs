using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newContactData = new ContactData("NewName");
            newContactData.Lastname = "newLastname";
            newContactData.Middlename = "newMiddlename";

            app.Contacts.Modify(1, newContactData);
        }
    }
}
