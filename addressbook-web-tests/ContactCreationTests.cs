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
            navigator.GoToHomePage();
            loginHelper.Login(new AccountData("admin", "secret"));
            contactHelper.InitContactCreation();
            ContactData contactData = new ContactData("Name3");
            contactData.Lastname = "lastname3";
            contactHelper.FillContactForm(contactData);
            contactHelper.SubmitContactCreation();
            contactHelper.ReturnToHomePage();
            loginHelper.Logout();
        }       
    }
}
