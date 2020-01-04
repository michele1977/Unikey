using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.IRepository
{
    public interface IAdministratedTestRepository
    {
        void Update_Save(AdministratedTestBusiness adTest);
        void Add(AdministratedTestBusiness adTest);
        AdministratedTestBusiness GetAdministratedTestById(int adTestId);

    }
}
