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
        public void TheContactsTest()
        {
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contacts.InitContactCreation();
            ContactData contactData = new ContactData("Name3");
            contactData.Lastname = "lastname3";
            app.Contacts.FillContactForm(contactData);
            app.Contacts.SubmitContactCreation();
            app.Contacts.ReturnToHomePage();
            app.Auth.Logout();
        }       
    }
}
