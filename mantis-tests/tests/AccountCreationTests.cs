using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        /*[TestFixtureSetUp]
        public void SetupConfig()
        {
            app.Ftp.BackupFile("/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localFile);
            }
           
        }


        [Test]
        public void AccountCreationTest()
        {
            AccountData account = new AccountData() 
            {
                Name = "testUser",
                Password = "passwort",
                Email = "testuser@localhost.localdomain"
            };
            app.Registration.Register(account);
        }
        [TestFixtureTearDown]

        public void RestoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }*/
    }
}
