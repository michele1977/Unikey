using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Modules;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Mapper.AutoMappers;
using UnikeyFactoryTest.NinjectConfiguration;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;

namespace UpdateTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public async Task NewUpdateTest_KO()
        {
            bool result;
            var Kernel = new StandardKernel();

            Kernel.Load(new List<INinjectModule>()
            {
                new AutoMapperBindingsService(),
                new UnikeyFactoryTestBindings()
            });

            var ctx = new TestPlatformDBEntities();
            var repo = new TestRepository(ctx, Kernel);

            try
            {
                var prova = await repo.GetTest(520);
                var answertoremove = prova.Questions.FirstOrDefault(q => q.Id == 1120)
                    .Answers.FirstOrDefault();

                prova.Questions.FirstOrDefault(q => q.Id == 1120).Text = "Prova modifica testo domanda";

                prova.Questions.FirstOrDefault(q => q.Id == 1120).Answers.Remove(answertoremove);
                await repo.UpdateTest(prova);

                var verifica = await repo.GetTest(520);
                
                result = true;
            }
            catch(Exception e)
            {
                result = false;
            }
            
            Assert.AreEqual(false, result);

        }
    }
}
 