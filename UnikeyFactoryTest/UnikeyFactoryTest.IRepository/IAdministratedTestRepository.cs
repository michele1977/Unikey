using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.IRepository
{
    public interface IAdministratedTestRepository : IDisposable
    {
        Task Update_Save(AdministratedTestBusiness adTest);
        Task<AdministratedTestBusiness> Add(AdministratedTestBusiness adTest);
        Task<AdministratedTestBusiness> GetAdministratedTestById(int adTestId);
        Task<DbSet<AdministratedTest>> GetAdministratedTests();
        Task DeleteAdministratedTest(int administratedTestId);
    }
}
