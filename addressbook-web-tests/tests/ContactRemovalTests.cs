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
            app.Contacts.Remove(1);
        }

        [Test]
        public void ContactRemovalTestFromEditForm()
        {
            if (!app.Contacts.IsContactPresent())
            {
                ContactData defContactData = new ContactData("def");
                app.Contacts.Create(defContactData);
            }
            app.Contacts.RemoveFromEditForm(1);
        }
    }
}
