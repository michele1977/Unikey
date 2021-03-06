﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Presentation.Models.Dto;

namespace UnikeyFactoryTest.Presentation.Models
{
    public class TestsListModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool LastPage { get; set; }
        public bool IsAjaxCall { get; set; }
        public string TextFilter { get; set; }

        public TestsListModel()
        {
            Tests = new List<TestDto>();

        }
        public List<TestDto> Tests { get; set; }

        public List<TestDto> Paginate(List<TestBusiness> tests)
        {
            List<TestDto> filteredList = tests.Select(t => new TestDto(t))
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