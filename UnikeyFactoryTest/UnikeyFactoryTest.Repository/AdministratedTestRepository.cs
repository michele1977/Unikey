using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<AdministratedTestBusiness> Add(AdministratedTestBusiness adTest)
        {
            var addTask = Task.Run(() =>
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
                    catch (Exception ex)
                    {
                        throw new Exception("Save Failed");
                    }
                    finally
                    {
                        _ctx.AdministratedTests.Find(1);
                    }
                }
            });
            return await addTask;
        }

        public async Task<AdministratedTestBusiness> GetAdministratedTestById(int adTestId)
        {
            var _task = Task.Run(() =>
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

            });

            return await _task;
        }

        public async Task<List<AdministratedTest>> GetAdministratedTests()
        {
            var myTask = Task.Run(() => _ctx.AdministratedTests.ToListAsync());
            return await myTask;
        }

        public async Task DeleteAdministratedTest(int administratedTestId)
        {
            AdministratedTest administratedTest = _ctx.AdministratedTests
                .FirstOrDefault(t => t.Id == administratedTestId);

            if (administratedTest == null)
            {
                throw new NullReferenceException("AdministratedTest not found at specified id");
            }

            var myTask = Task.Run(() =>
            {
                _ctx.AdministratedTests.Remove(administratedTest);
                _ctx.SaveChanges();
            });

            await myTask;
        }

        public async Task Update_Save(AdministratedTestBusiness adTest)
        {
            var newTest = AdministratedTestMapper.MapDomainToDao(adTest);
            try
            {
                foreach (var q in newTest.AdministratedQuestions)
                {
                    foreach (var a in q.AdministratedAnswers)
                    {
                        var myTask2 = Task.Run(() =>
                        {
                            if (a.isSelected == true)
                            {
                                _ctx.AdministratedAnswers.FirstOrDefault(x => x.Id == a.Id).isSelected = true;
                            }
                        });
                        await myTask2;
                    }
                }

                decimal score = 0;

                var myTask3 = Task.Run(() =>
                {
                    foreach (var q in newTest.AdministratedQuestions)
                    {
                        if ((q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true)) != null)
                            score = score + q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true).Score ?? 0;
                    }

                    _ctx.AdministratedTests.FirstOrDefault(x => x.Id == newTest.Id).TotalScore = decimal.ToInt32(score);
                    _ctx.AdministratedTests.FirstOrDefault(x => x.Id == newTest.Id).Date = DateTime.Today;
                });
                await myTask3;

                _ctx.SaveChanges();
                
            }
            catch (Exception ex)
            {
                throw new Exception("Update failed");
            }

        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
