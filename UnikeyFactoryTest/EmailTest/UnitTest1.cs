using System;
using Email;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmailTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var provider = new AdditionalService();
            try
            {
                provider.SendMail("elisa_ruotolo@live.it");

            }
            catch (Exception e)
            {
                throw new Exception("Error");
            }
            
            
        }
    }
}
