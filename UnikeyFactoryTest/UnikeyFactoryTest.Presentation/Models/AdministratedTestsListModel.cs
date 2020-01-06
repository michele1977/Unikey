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
        public bool LastPage { get; set; }
        public bool IsAjaxCall { get; set; }
        public string TextFilter { get; set; }

        public AdministratedTestsListModel()
        {
            Tests = new List<AdministratedTestDto>();

        }
        public List<AdministratedTestDto> Tests { get; set; }

        public List<AdministratedTestDto> Paginate(List<AdministratedTestBusiness> tests)
        {
            List<AdministratedTestDto> filteredList = tests.Select(t => new AdministratedTestDto(t))
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize).ToList();

            if ((tests.Skip((PageNumber) * PageSize).Take(PageSize).ToList().Count) == 0)
            {
                LastPage = true;
            }

            return filteredList;
        }
    }
}