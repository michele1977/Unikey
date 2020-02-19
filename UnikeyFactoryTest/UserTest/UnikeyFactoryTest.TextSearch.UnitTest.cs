using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTest
    {
        [TestMethod]
        public void TextSearch_Ok()
        {
            string testName = "RandomTitleToMakeThisTestRunOk";
            using (TestPlatformDBEntities ctx = new TestPlatformDBEntities())
            {
                using (DbContextTransaction transaction = ctx.Database.BeginTransaction())
                {
                    Test test = new Test()
                    {
                        AdministratedTests = new List<AdministratedTest>(),
                        Date = DateTime.Now,
                        Questions = new List<Question>(),
                        Title = testName,
                        URL = "randomUrl",
                        User = null,
                        UserId = 0
                    }

                    transaction.Rollback();
                }
            }
        }
    }
}
