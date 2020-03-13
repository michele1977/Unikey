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
        AdministratedTestBusiness Add(AdministratedTestBusiness adTest);
        Task<AdministratedTestBusiness> GetAdministratedTestById(int adTestId);
        Task<List<AdministratedTestBusiness>> GetAdministratedTests();
        Task<List<AdministratedTestBusiness>> GetAdministratedTestsByTestId(int pageNum, int pageSize, int testId);
        Task<List<AdministratedTestBusiness>> GetAdministratedTestsByTestId(int pageNum, int pageSize, string filter, int testId);
        Task DeleteAdministratedTest(int administratedTestId);
        Task Update_Save_Question(AdministratedQuestionBusiness adQuestion);
        bool IsContextNull { get; }
        Task<int> CountExTests(string filter);
        Task<int> CountExTests();
    }
}
