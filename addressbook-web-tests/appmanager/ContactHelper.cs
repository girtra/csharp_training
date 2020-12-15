using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        private List<ContactData> contactCache = null;
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
        public ContactHelper Remove(int index)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(index + 1);
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
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
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
    }
}
