﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Mapper;

namespace UnikeyFactoryTest.Repository
{
    public class AdministratedTestRepository : IAdministratedTestRepository
    {
        private readonly TestPlatformDBEntities _ctx;

        public AdministratedTestRepository()
        {
            _ctx = new TestPlatformDBEntities();
        }

        public void Add(AdministratedTestBusiness adTest)
        {
            try
            {
                var newAdTestDB = AdministratedTestMapper.MapDomainToDao(adTest);
                _ctx.AdministratedTests.Add(newAdTestDB);
                _ctx.SaveChanges();
            }
            catch 
            {
                throw new Exception("Save Failed");
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

        public IEnumerable<AdministratedTest> GetAdministratedTests()
        {
            return _ctx.AdministratedTests;
        }

        public void DeleteAdministratedTest(int administratedTestId)
        {
            AdministratedTest administratedTest = _ctx.AdministratedTests
                .FirstOrDefault(t => t.Id == administratedTestId);

            if (administratedTest == null)
            {
                throw new NullReferenceException("AdministratedTest not found at specified id");
            }

            _ctx.AdministratedTests.Remove(administratedTest);
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
