﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            if (!app.Groups.IsGroupPresent())
            {
                GroupData defData = new GroupData("def");
                app.Groups.Create(defData);
            }
            app.Groups.Remove(1);
        }
    }
}
