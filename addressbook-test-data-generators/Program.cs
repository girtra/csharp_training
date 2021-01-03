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
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string datatype = args[0];
            int count = Convert.ToInt32(args[1]);
            string format = args[3];



            if (datatype == "groups")
            {
               var groups = CreateGroups(count);
                WriteGroupsToFile(groups, args[2], format);
                
            }
            else if (datatype == "contacts")
            {
               var contacts = CreateContacts(count);
               WriteContactsToFile(contacts, args[2], format);
            }
            else
            {
                System.Console.Out.Write("unrecognized datatype" + datatype);
            }
        }

        private static void WriteContactsToFile(List<ContactData> contacts, string fileName, string format)
        {
            if (format == "excel")
            {
                writeContactsToExcelFile(contacts, fileName);
            }
            else
            {
                StreamWriter writer = new StreamWriter(fileName);
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
                writer.Close();
            }
            
        }
        
        private static void WriteGroupsToFile(List<GroupData> groups, string fileName, string format)
        {
            if (format == "excel")
            {
                writeGroupsToExcelFile(groups, fileName);
            }
            else
            {
                StreamWriter writer = new StreamWriter(fileName);
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
                writer.Close();
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
            new XmlSerializer(items.GetType()).Serialize(writer, items);
        }
        static void writeToJsonFile<Titem>(List<Titem> items, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented));
        }
        static void writeGroupsToExcelFile(List<GroupData> groups, string fileName)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;
            int row = 1;
            foreach (var group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }
        static void writeContactsToExcelFile(List<ContactData> contacts, string fileName)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;
            int row = 1;
            foreach (var contact in contacts)
            {
                sheet.Cells[row, 1] = contact.Firstname;
                sheet.Cells[row, 2] = contact.Lastname;
                sheet.Cells[row, 3] = contact.Address;
                sheet.Cells[row, 4] = contact.HomePhone;
                sheet.Cells[row, 5] = contact.WorkPhone;
                sheet.Cells[row, 6] = contact.MobilePhone;
                sheet.Cells[row, 7] = contact.Email;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }
    }
}
