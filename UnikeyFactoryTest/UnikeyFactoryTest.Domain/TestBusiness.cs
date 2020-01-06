using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnikeyFactoryTest.Domain
{
    public class TestBusiness
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public DateTime? Date { get; set; }
        public int UserId { get; set; }

        public IEnumerable<AdministratedTestBusiness> AdministratedTests { get; set; }
        public IEnumerable<QuestionBusiness> Questions { get; set; }
        public UserBusiness User { get; set; }
    }
}