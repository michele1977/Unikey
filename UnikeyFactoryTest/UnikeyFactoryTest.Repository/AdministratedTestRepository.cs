using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Mapper;

namespace UnikeyFactoryTest.Repository
{
    public class AdministratedTestRepository : IAdministratedTestRepository, IDisposable
    {
        private readonly TestPlatformDBEntities _ctx;

        public AdministratedTestRepository()
        {
            _ctx = new TestPlatformDBEntities();
        }

        public AdministratedTestRepository(TestPlatformDBEntities ctx)
        {
            _ctx = ctx;
        }

        public AdministratedTestBusiness Add(AdministratedTestBusiness adTest)
        {
            using (_ctx)
            {
                try
                {
                    var newAdTestDB = AdministratedTestMapper.MapDomainToDao(adTest);
                    _ctx.AdministratedTests.Add(newAdTestDB);
                    _ctx.SaveChanges();
                    adTest = AdministratedTestMapper.MapDaoToDomain(newAdTestDB);
                    return adTest;
                }
                catch(Exception ex)
                {
                    throw new Exception("Save Failed");
                }
                finally
                {
                    _ctx.AdministratedTests.Find(1);
                }
            }

        }

        public AdministratedTestBusiness GetAdministratedTestById(int adTestId)
        {
            var adTestDB = _ctx.AdministratedTests.FirstOrDefault(x => x.Id.Equals(adTestId));
            if (adTestDB == null)
            {
                throw new Exception("Not valid id");
            }
            else
            {
                return AdministratedTestMapper.MapDaoToDomain(adTestDB);
            }
        }

        public void Update_Save(AdministratedTestBusiness adTest)
        {
            try
            {
                var upTestDB =  AdministratedTestMapper.MapDomainToDao(adTest);
                _ctx.AdministratedTests.Attach(upTestDB);
                _ctx.Entry(upTestDB).State = System.Data.Entity.EntityState.Modified;

                var AdminisratedTest = _ctx.AdministratedTests.FirstOrDefault(t => t.Id == adTest.Id);

                AdminisratedTest.Test = upTestDB.Test;
                AdminisratedTest.AdministratedQuestions = upTestDB.AdministratedQuestions;
                AdminisratedTest.URL = upTestDB.URL;
                AdminisratedTest.TestSubject = upTestDB.TestSubject;
                AdminisratedTest.TestId = upTestDB.TestId;
                AdminisratedTest.Date = upTestDB.Date;
                AdminisratedTest.TotalScore = upTestDB.TotalScore;
                
                //_ctx.AdministratedTests.AddOrUpdate(upTestDB);
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw  new Exception("Update failed");
            }

        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
