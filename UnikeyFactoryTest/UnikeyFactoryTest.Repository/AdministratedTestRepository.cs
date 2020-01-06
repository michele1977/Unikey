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
            var newTest = Mapper.AdministratedTestMapper.MapDomainToDao(adTest);
            try
            {
                foreach (var q in newTest.AdministratedQuestions)
                {
                    foreach (var a in q.AdministratedAnswers)
                    {
                        if (a.isSelected == true)
                        {
                            _ctx.AdministratedAnswers.FirstOrDefault(x => x.Id == a.Id).isSelected = true;
                        }   
                    }
                }

                decimal score = 0;

                foreach (var q in newTest.AdministratedQuestions)
                {
                    if ((q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true)) != null)
                        score = score + q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true).Score ??0;
                }

                _ctx.AdministratedTests.FirstOrDefault(x => x.Id == newTest.Id).TotalScore = decimal.ToInt32(score);
                _ctx.AdministratedTests.FirstOrDefault(x => x.Id == newTest.Id).Date = DateTime.Today;

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
