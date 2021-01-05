using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 4; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(15), GenerateRandomString(15))
                {
                    Address = GenerateRandomString(100),
                    WorkPhone = GenerateRandomNumber(),
                    HomePhone = GenerateRandomPhone(),
                    MobilePhone = GenerateRandomString(13),
                    Email = GenerateRandomEmail(20),
                });
            }
            return contacts;
        }
        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>))
                    .Deserialize(new StreamReader(@"contacts.xml"));
        }
        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json"));
        }

        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTest(ContactData contact)
        {         
            
            List<ContactData> oldContacts = app.Contacts.GetContactList();
          
            app.Contacts.Create(contact);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);
            oldContacts.Add(contact);

            newContacts.Sort();
            oldContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }       
    }
}
