using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    public class AuthTestBase : TestBase
    {
        [SetUp]
        public void SetupAuthTest()
        {
           app.LoginHelper.Login(new AccountData() 
           {
               Name= "administrator", Password= "root123"
           });
        }

        [TearDown]
        public void Stop()
        {
            ApplicationManager.GetInstance().LoginHelper.Logout();
        }
    }
}

