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
            ContactData newContactData = new ContactData("NewName");
            newContactData.Lastname = "newLastname";
            newContactData.Middlename = "newMiddlename";


            if(!app.Contacts.IsContactPresent())
            {
                ContactData defContactData = new ContactData("def");
                app.Contacts.Create(defContactData);
            }
            app.Contacts.Modify(1, newContactData);
        }
    }
}
