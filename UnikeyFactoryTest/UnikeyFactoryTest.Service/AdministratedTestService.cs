using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class AdministratedTestService
    {
        public IAdministratedTestRepository Repo { get; set; }

        public AdministratedTestService()
        {
            Repo = new AdministratedTestRepository();
        }

        public AdministratedTestBusiness AdministratedTest_Builder(TestBusiness test, string subject )
        {
            var newAdTest = new AdministratedTestBusiness();

            newAdTest.Date = DateTime.Today;
            newAdTest.URL = test.URL;
            newAdTest.TestId = test.Id;
            newAdTest.TestSubject = subject;
            foreach (var q in test.Questions)
            {
                newAdTest.AdministratedQuestions.Add(new AdministratedQuestionBusiness()
                {
                    Text = q.Text,
                    AdministratedTestId = q.TestId,
                    AdministratedAnswers = q.Answers.Select(a=> new AdministratedAnswerBusiness(){Text = a.Text, Score = a.Score, AdministratedQuestionId = a.QuestionId, isCorrect = a.IsCorrect, isSelected = false}).ToList()
                });
            }

            return newAdTest;
        }

        public void Add(AdministratedTestBusiness adTest)
        {
            Repo.Add(adTest);
        }

        public void Update_Save(AdministratedTestBusiness adTest)
        {
            decimal score = 0;

            foreach (var q in adTest.AdministratedQuestions)
            {
                if ((q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true)) != null)
                    score = score + q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true).Score??0;
            }

            adTest.TotalScore = decimal.ToInt32(score);

            Repo.Update_Save(adTest);
        }

        public AdministratedTestBusiness GetAdministratedTestById (int adTestId)
        {
           return Repo.GetAdministratedTestById(adTestId);
        }

        public AdministratedTestBusiness GetAdministratedTest(int administratedTestId)
        {
            Repo = new AdministratedTestRepository();

            AdministratedTestBusiness administratedTest = null;

            try
            {
                administratedTest = Repo.GetAdministratedTestById(administratedTestId);
            }
            catch (NullReferenceException ex)
            {
                //TODO
            }
            catch (ArgumentNullException ex)
            {
                //TODO
            }
            catch (Exception ex)
            {
                //TODO
            }

            return administratedTest;
        }

        public IEnumerable<AdministratedTestBusiness> GetAdministratedTests()
        {
            Repo = new AdministratedTestRepository();
            var administratedTests = Repo.GetAdministratedTests().Select(AdministratedTestMapper.MapDaoToDomain);
            return administratedTests;
        }


        public void DeleteAdministratedTest(int administratedTestId)
        {
            using (Repo = new AdministratedTestRepository())
            {
                try
                {
                    Repo.DeleteAdministratedTest(administratedTestId);
                }
                catch (NotSupportedException ex)
                {

                }
                catch (ObjectDisposedException ex)
                {

                }
                catch (InvalidOperationException ex)
                {

                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
