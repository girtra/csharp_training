﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests 
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            navigator.GoToHomePage();
            loginHelper.Login(new AccountData("admin", "secret"));
            navigator.GoToGroupsPage();
            groupHalper.InitGroupCreation();
            GroupData group = new GroupData("test3");
            group.Header = "3";
            group.Footer = "33";
            groupHalper.FillGroupForm(group);
            groupHalper.SubmitGroupCreation();
            groupHalper.ReturnToGroupsPage();
            loginHelper.Logout();
        }       
    }
}

