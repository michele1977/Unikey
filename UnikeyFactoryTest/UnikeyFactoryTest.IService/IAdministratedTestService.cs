using System.Collections.Generic;
using System.Threading.Tasks;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.IService
{
    public interface IAdministratedTestService
    {
        AdministratedTestBusiness AdministratedTest_Builder(TestBusiness test, string subject);
        AdministratedTestBusiness Add(AdministratedTestBusiness adTest);
        Task Update_Save(AdministratedTestBusiness adTest);
        Task<AdministratedTestBusiness> GetAdministratedTestById(int adTestId);
        Task<IEnumerable<AdministratedTestBusiness>> GetAdministratedTests();
        Task<List<AdministratedTestBusiness>> GetAdministratedTestsByTestId(int pageNum, int pageSize, string filter, int testId);
        Task DeleteAdministratedTest(int administratedTestId);
        AdministratedQuestionBusiness Next(AdministratedTestBusiness administratedTest, int position);
        Task Update_Save_Question(AdministratedQuestionBusiness adQuestion);
        AdministratedQuestionBusiness Previous(AdministratedTestBusiness administratedTest, int position);
        Task<List<AdministratedTestBusiness>> GetAdministratedTestsByFilter(string filter);
        void Dispose();
        Task<int> CountExTests(string filter);
    }
}