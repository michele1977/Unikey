﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class AdministratedTestService
    {
        private  IAdministratedTestRepository _repo;

        public AdministratedTestService()
        {
            _repo = new AdministratedTestRepository();
        }

        public AdministratedTestService(AdministratedTestRepository repo)
        {
            if(repo is null)
                _repo = new AdministratedTestRepository();
            else
                _repo = repo;
        }

        public AdministratedTestBusiness AdministratedTest_Builder(TestBusiness test, string subject)
        {
            var newAdTest = new AdministratedTestBusiness();

            newAdTest.Date = DateTime.Today;
            newAdTest.URL = test.URL;
            newAdTest.TestId = test.Id;
            newAdTest.TestSubject = subject;
            newAdTest.AdministratedQuestions = new List<AdministratedQuestionBusiness>();
            foreach (var q in test.Questions)
            {
                newAdTest.AdministratedQuestions.Add(new AdministratedQuestionBusiness()
                {
                    Text = q.Text,
                    AdministratedTestId = q.TestId,
                    AdministratedAnswers = q.Answers.Select(a => new AdministratedAnswerBusiness() { Text = a.Text, Score = a.Score, AdministratedQuestionId = a.QuestionId, isCorrect = a.IsCorrect, isSelected = false }).ToList()
                });
            }

            return newAdTest;
        }

        public AdministratedTestBusiness Add(AdministratedTestBusiness adTest)
        {
           return _repo.Add(adTest);
        }

        public void Update_Save(AdministratedTestBusiness adTest)
        {
            _repo.Update_Save(adTest);
        }

        public AdministratedTestBusiness GetAdministratedTestById (int adTestId)
        {
           return _repo.GetAdministratedTestById(adTestId);
        }

        public AdministratedTestBusiness GetAdministratedTest(int administratedTestId)
        {
            _repo = new AdministratedTestRepository();

            AdministratedTestBusiness administratedTest = null;

            try
            {
                administratedTest = _repo.GetAdministratedTestById(administratedTestId);
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
            _repo = new AdministratedTestRepository();
            var administratedTests = _repo.GetAdministratedTests().Select(AdministratedTestMapper.MapDaoToDomain);
            return administratedTests;
        }


        public void DeleteAdministratedTest(int administratedTestId)
        {
            using (_repo = new AdministratedTestRepository())
            {
                try
                {
                    _repo.DeleteAdministratedTest(administratedTestId);
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
