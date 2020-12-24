using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager app;

        [SetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();            
        }
        public static Random rnd = new Random();
        public static string GenerateRandomString(int max)
        {
              int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 223)));
            }
            return builder.ToString();
        }

        public static string GenerateRandomNumber()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 13; i++)
            {
                builder.Append(rnd.Next(10).ToString());
            }
            return builder.ToString();
        }

        public static string GenerateRandomPhone()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 13; i++)
            {
                builder.Append(rnd.Next(10).ToString());
            }
            return builder.Append("+()-").ToString();
        }

        public static string GenerateRandomEmail(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            string [] domains= { "hotmail.com", "gmail.com", "mail.com", "mail.kz", "yahoo.com" };
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 223)));
            }
            return builder.Append('@' + domains[rnd.Next(4)]).ToString();
        }
    }
}
