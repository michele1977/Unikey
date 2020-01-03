using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClassLibrary1.DAL.DAO;
using ClassLibrary1.Business.Entities;

namespace ClassLibrary1.DAL.Mapper
{
    public class ExQuestionAutoMapper
    {
        public void MapDaoToEntity()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ExTest_Question,ExQuestion>());
            var mapper = config.CreateMapper();

        }
    }
}
