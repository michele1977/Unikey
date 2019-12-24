using System;
using System.Collections.Generic;
using MasterDetail.Business;
using MasterDetail.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MAsterDetail.Business.Test
{
    [TestClass]
    public class AuthorServiceTest
    {
        [TestMethod]
        public void AuthorService_Create_OK()
        {
            var auth = new Author()
            {
                Books = new List<Book>(){new Book(){Title = "MyTitle of my book",Year = 2019}},
                LastName = "Marinelli",
                Name = "Mike",
            };
            var service = new AuthorService();
            service.AddNewAuthor(auth);

            var authRead = service.GetAuthor(auth.Id);
            //Assert.AreEqual(authRead, auth);
            Assert.IsTrue(authRead.Equals(auth));

        }
    }
}
