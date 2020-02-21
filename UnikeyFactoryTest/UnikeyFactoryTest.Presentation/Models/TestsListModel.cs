using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Ajax.Utilities;
using Ninject;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.NinjectConfiguration;
using UnikeyFactoryTest.Presentation.Models.DTO;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.Presentation.Models
{
    public class TestsListModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int LastPage { get; set; }
        public string TextFilter { get; set; }
        [Inject]
        private ITestService service;

        public Dictionary<int, int> ClosedTestsNumberPerTest { get; set; }

        private IKernel kernel;
        public Dictionary<int, int> AdministratedTestOpen;
        public TestsListModel()
        {
            kernel = new StandardKernel();
            kernel.Load(new UnikeyFactoryTestBindings());
            service = kernel.Get<ITestService>();
        }

        public TestsListModel(ITestService value)
        {
            Tests = new List<TestDto>();
            service = value;
            AdministratedTestOpen = new Dictionary<int, int>();
        }

        public List<TestDto> Tests { get; set; }

        public List<TestDto> Paginate(List<TestBusiness> tests)
        {
            LastPage = (int)Math.Ceiling((float)tests.Count / PageSize);

            if (PageNumber > LastPage)
            {
                PageNumber = LastPage;
            }

            List<TestDto> filteredList = tests.Select(t => new TestDto(t, service))
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize).ToList();

            return filteredList;
        }

        public async Task<List<TestDto>> Paginate(List<TestDto> tests)
        {
            LastPage = (int)Math.Ceiling((float)tests.Count / PageSize);

            List<TestDto> filteredList = await  Task.Run(() =>tests
                            .Skip((PageNumber - 1) * PageSize)
                            .Take(PageSize).ToList());

            PageNumber = PageNumber > LastPage ? LastPage : PageNumber;

            return filteredList;
        }

    }
}