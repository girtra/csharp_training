using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string datatype = args[0];
            int count = Convert.ToInt32(args[1]);
            StreamWriter writer = new StreamWriter(args[2]);
            string format = args[3];



            if (datatype == "group")
            {
               var groups = CreateGroups(count);
                WriteGroupsToFile(writer, groups, format);
                
            }
            else if (datatype == "contacts")
            {
               var contacts = CreateContacts(count);
               WriteContactsToFile(writer, contacts, format);
            }
            else
            {
                System.Console.Out.Write("unrecognized datatype" + datatype);
            }

            writer.Close();
        }

        private static void WriteContactsToFile(StreamWriter writer, List<ContactData> contacts, string format)
        {
            if (format == "csv")
            {
                writeContactsToCsvFile(contacts, writer);
            }
            else if (format == "xml")
            {
                writeToXmlFile<ContactData>(contacts, writer);
            }
            else if (format == "json")
            {
                writeToJsonFile<ContactData>(contacts, writer);
            }
            else
            {
                System.Console.Out.Write("unrecognized format" + format);
            }
        }

        private static void WriteGroupsToFile(StreamWriter writer, List<GroupData> groups, string format)
        {
            if (format == "csv")
            {
                writeGroupsToCsvFile(groups, writer);
            }
            else if (format == "xml")
            {
                writeToXmlFile<GroupData>(groups, writer);
            }
            else if (format == "json")
            {
                writeToJsonFile<GroupData>(groups, writer);
            }
            else
            {
                System.Console.Out.Write("unrecognized format" + format);
            }
        }

        private static List<ContactData> CreateContacts(int count)
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(15), TestBase.GenerateRandomString(15))
                {
                    Address = TestBase.GenerateRandomString(100),
                    WorkPhone = TestBase.GenerateRandomNumber(),
                    HomePhone = TestBase.GenerateRandomPhone(),
                    MobilePhone = TestBase.GenerateRandomString(13),
                    Email = TestBase.GenerateRandomEmail(20),
                });
            }
            return contacts;
        }

        private static List<GroupData> CreateGroups(int count)
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(100),
                    Footer = TestBase.GenerateRandomString(100)
                });
            }
            return groups;
            
        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer) 
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void writeContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                //writer.WriteLine(String.Format("${0},${1},${2},${3},${4},${5},${6}",
                //    contact.Firstname, contact.Lastname, contact.Address, 
                //    contact.HomePhone, contact.WorkPhone, contact.MobilePhone, 
                //    contact.Email));

                writer.WriteLine($"{contact.Firstname},{contact.Lastname},{contact.Address}," +
                    $"{contact.HomePhone},{contact.WorkPhone},{contact.MobilePhone},{contact.Email}");
            }
        }

        static void writeToXmlFile<Titem>(List<Titem> items, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, items);
        }
        


        static void writeToJsonFile<Titem>(List<Titem> items, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
