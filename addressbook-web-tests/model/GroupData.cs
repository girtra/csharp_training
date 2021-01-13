﻿using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        private string header = "";
        private string footer = "";
               
        public GroupData ()
        {
        }
        
        public GroupData (string name)
        {
            Name = name;
        }
        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return " name= " + Name + "\n heder= " + Header + "\n footer= " + Footer;
        }
        [Column (Name= "group_name"), NotNull]
        public string Name { get; set; }

        [Column(Name = "group_header")]
        public string Header { get; set; }

        [Column(Name = "group_footer")]
        public string Footer { get; set; }

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; }

        public static List<GroupData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups.Where(x => x.Deprecated == "0000-00-00 00:00:00") select g).ToList();
            }
        }

        public List<ContactData> GetContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.Id && c.Deprecated == "0000-00-00 00:00:00")
                        select c).Distinct().ToList();
            }
        }

        public List<GroupData> GetGroupWithContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return db.Groups.Where(g => db.GCR.Any(gcr => gcr.GroupId == g.Id) && g.Deprecated == "0000-00-00 00:00:00").Distinct().ToList();
            }
        }
    }
 }


