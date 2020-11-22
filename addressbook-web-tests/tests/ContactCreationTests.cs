using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {            
            ContactData contactData = new ContactData("Name3");
            contactData.Lastname = "lastname3";

            app.Contacts.Create(contactData);
        }       
    }
}
