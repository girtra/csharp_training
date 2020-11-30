using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            app.Auth.Logout(); 
            
            AccountData data = new AccountData("admin", "secret");            
            app.Auth.Login(data);

            Assert.IsTrue(app.Auth.IsLoggedIn(data));
        }
        [Test]
        public void LoginWithInvalidCredentials()
        {
            app.Auth.Logout();

            AccountData data = new AccountData("admin", "123456");
            app.Auth.Login(data);

            Assert.IsFalse(app.Auth.IsLoggedIn(data));
        }
    }
}
