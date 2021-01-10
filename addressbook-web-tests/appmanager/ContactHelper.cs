using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        private List<ContactData> contactCache = null;
        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> sells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = sells[1].Text;
            string firstName = sells[2].Text;
            string address = sells[3].Text;
            string emails = sells[4].Text;
            string phones = sells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = emails,
                AllPhones = phones
            };

        }
              

        public Dictionary<int, string> ConvertContactDataToDictionary(ContactData fromForm)
        {
            var dictionary = new Dictionary<int, string>();
            dictionary.Add(1, fromForm.FullName);
            dictionary.Add(2, fromForm.Address);

            if (!string.IsNullOrEmpty(fromForm.HomePhone))
            {
                dictionary.Add(3, ("H: " + fromForm.HomePhone));
            }
            if (!string.IsNullOrEmpty(fromForm.MobilePhone))
            {
                dictionary.Add(4, ("M: " + fromForm.MobilePhone));
            }
            if (!string.IsNullOrEmpty(fromForm.WorkPhone))
            {
                dictionary.Add(5, ("W: " + fromForm.WorkPhone));
            }
            return dictionary;
        }


        public string[] GetContactInfoFromDetailsCard(int index)
        {
            manager.Navigator.GoToHomePage();
            OpenContactDetailsCard(index);
            string contactInfo = driver.FindElement(By.Id("content")).Text;
            return contactInfo.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);             
        }

        public ContactHelper OpenContactDetailsCard(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
               .FindElements(By.TagName("td"))[6]
               .FindElement(By.TagName("a")).Click();
            return this;
        }    

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value").Trim();
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value").Trim();
            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value").Trim();

            string address = driver.FindElement(By.Name("address")).GetAttribute("value").Trim();

            string email = driver.FindElement(By.Name("email")).GetAttribute("value").Trim();
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value").Trim();
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value").Trim();
            
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value").Trim();
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value").Trim();
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value").Trim();

            return new ContactData(firstName, lastName)
            {
                Middlename = middlename,
                Address = address,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }

        public ContactHelper Create(ContactData contactData)
        {
            InitContactCreation();
            FillContactForm(contactData);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }

        public List<ContactData> GetContactList()
        {
           if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();

                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name=entry] td~td:nth-child(-n+3)"));

                for (int i = 0; i < elements.Count(); i++)
                {
                    ContactData contact = new ContactData(elements.ElementAt(i + 1).Text);
                    contact.Lastname = elements.ElementAt(i).Text;
                    contactCache.Add(contact);
                    i++;
                }
            }
            
            return new List<ContactData>(contactCache);
        }

        public ContactData GetContact(int index)
        {
            manager.Navigator.GoToHomePage();
            ContactData contact = new ContactData(driver.FindElement(By.CssSelector("tr[name=entry] td:nth-child(index + 1)")).Text);
            return contact;
        }

        public ContactHelper Modify(int index, ContactData newContactData)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(index + 1);
            InitContactModification(index + 1);
            FillContactForm(newContactData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(string id, ContactData newContactData)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(id);
            InitContactModification(id);
            FillContactForm(newContactData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int index)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(index + 1);
            InitContactDeletion();
            SubmitContactDeletion();
            return this;
        }

        public ContactHelper Remove(ContactData toBeRemoved)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(toBeRemoved.Id);
            InitContactDeletion();
            SubmitContactDeletion();
            return this;
        }

        public ContactHelper RemoveFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(index + 1);
            InitContactModification(index + 1);
            InitContactDeletion();
            return this;
        }


        public ContactHelper RemoveFromEditForm(ContactData toBeRemoved)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(toBeRemoved.Id);
            InitContactModification(toBeRemoved.Id);
            InitContactDeletion();
            return this;
        }

        private ContactHelper SubmitContactDeletion()
        {
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactDeletion()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();           
            contactCache = null;
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this; ;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();               
            //driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper InitContactModification(string id)
        {
            driver.FindElements(By.CssSelector("a[href *= '"+ id +"']"))[1].Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }


        public ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value = '" + id + "'])")).Click();
            return this;
        }

        public bool IsContactPresent()
        {
            manager.Navigator.GoToHomePage();
            return IsElementPresent(By.XPath("(//input[@name='selected[]'])"));
        }
        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contactData)
        {
            Type(By.Name("firstname"), contactData.Firstname);
            Type(By.Name("middlename"), contactData.Middlename);
            Type(By.Name("lastname"), contactData.Lastname);
            Type(By.Name("address"), contactData.Address);
            Type(By.Name("home"), contactData.HomePhone);
            Type(By.Name("mobile"), contactData.MobilePhone);
            Type(By.Name("work"), contactData.WorkPhone);
            Type(By.Name("email"), contactData.Email);
            Type(By.Name("email2"), contactData.Email2);
            Type(By.Name("email3"), contactData.Email3);
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }
        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }


        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match t = new Regex(@"\d+").Match(text);
            return Int32.Parse(t.Value);
        }

    }
}
