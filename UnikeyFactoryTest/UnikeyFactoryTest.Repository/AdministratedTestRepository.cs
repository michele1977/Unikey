using System;
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

        public void Add(AdministratedTestBusiness adTest)
        {
            using (_ctx)
            {
                try
                {
                    var newAdTestDB = AdministratedTestMapper.MapDomainToDao(adTest);
                    _ctx.AdministratedTests.Add(newAdTestDB);
                    _ctx.SaveChanges();

                    adTest.Id = newAdTestDB.Id;
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
                _ctx.SaveChanges();
            }
            catch
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
