using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;

namespace UnikeyFactoryTest.AutoMapper
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly UnitTestBindings _bindings = new UnitTestBindings();
        private readonly IKernel _kernel = new StandardKernel(_bindings);
        [TestMethod]
        public void TestMethod1()
        {
            var repo = _kernel.Get<ITestRepository>();
            var mapper = _kernel.Get<IMapper>();
            var service = _kernel.Get<ITestService>();
            //repo.DeleteTest(20);
            var x = repo.GetTest(20);

            var test = new Test() {Title = "d"};

            var byztest = mapper.Map<Test, TestBusiness>(test);

            var test1 = mapper.Map<TestBusiness, Test>(byztest);

        }
    }
}
