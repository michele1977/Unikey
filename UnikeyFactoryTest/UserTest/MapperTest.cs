using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Mapper;

namespace UnikeyFactoryTestDAL.Test
{
    [TestClass]
    public class MapperTest
    {
        [TestMethod]
        public void AutoMapperConfigTest_OK()
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<AdministratedTest, AdministratedTest>());
            //config.AssertConfigurationIsValid();
        }
        [TestMethod]
        public void GetStateEnum_Ok()
        {
            AdministratedTest test = new AdministratedTest();
            test.StateEnum = (State) 1;
            var x = AdministratedTestMapper.MapDaoToDomain(test);
            Assert.AreEqual(1, (int)x.StateEnum);
        }
    }
}
