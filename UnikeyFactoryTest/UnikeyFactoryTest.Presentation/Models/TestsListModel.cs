using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Ajax.Utilities;
using UnikeyFactoryTest.Domain;
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

        public TestsListModel()
        {
            Tests = new List<TestDto>();

        }
        public List<TestDto> Tests { get; set; }

        public List<TestDto> Paginate(List<TestBusiness> tests)
        {
            LastPage = (int) Math.Ceiling((float) tests.Count / PageSize);

            if (PageNumber > LastPage)
            {
                PageNumber = LastPage;
            }

            List<TestDto> filteredList = tests.Select(t => new TestDto(t))
                                .Skip((PageNumber - 1) * PageSize)
                                .Take(PageSize).ToList();

            return filteredList;
        }

        public async Task<List<TestDto>> Paginate(List<TestDto> tests)
        {
            LastPage = (int)Math.Ceiling((float)tests.Count / PageSize);

            if (PageNumber > LastPage)
            {
                PageNumber = LastPage;
            }

            List<TestDto> filteredList = tests
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize).ToList();

            return filteredList;
        }
    }
}