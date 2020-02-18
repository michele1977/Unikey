using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Presentation.Models.DTO;

namespace UnikeyFactoryTest.Presentation.Models
{
    public class AdministratedTestsListModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string TextFilter { get; set; }
        public int LastPage { get; set; }

        public AdministratedTestsListModel()
        {
            Tests = new List<AdministratedTestDto>();

        }
        public List<AdministratedTestDto> Tests { get; set; }

        public List<AdministratedTestDto> Paginate(List<AdministratedTestBusiness> tests)
        {
            LastPage = (int)Math.Ceiling((float)tests.Count / PageSize);

            if (PageNumber > LastPage)
            {
                PageNumber = LastPage;
            }

            List<AdministratedTestDto> filteredList = tests.Select(t => new AdministratedTestDto(t))
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize).ToList();


            return filteredList;
        }

        public List<AdministratedTestDto> Paginate(List<AdministratedTestDto> tests)
        {
            LastPage = (int)Math.Ceiling((float)tests.Count / PageSize);

            if (PageNumber > LastPage)
            {
                PageNumber = LastPage;
            }

            List<AdministratedTestDto> filteredList = tests
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize).ToList();


            return filteredList;
        }
    }
}