using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void ContactInformationTestFromEditFormAndTable()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(1);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(1);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void ContactInformationTestFromEditFormAndDetailsCard()
        {
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(3);
            string[] infofromDetailsCard = app.Contacts.GetContactInfoFromDetailsCard(3);
            var contactDictionaryFromEditForm = app.Contacts.ConvertContactDataToDictionary(fromForm);
            
            int i = 0;
            foreach (var element in contactDictionaryFromEditForm)
            {
                if (!string.IsNullOrEmpty(element.Value))
                {                    
                    Assert.AreEqual(element.Value, infofromDetailsCard[i]);
                    i++;
                }
            }
            //check if there is additional information in contact details card
            Assert.IsTrue(i == infofromDetailsCard.Length);
        }
    }
}
