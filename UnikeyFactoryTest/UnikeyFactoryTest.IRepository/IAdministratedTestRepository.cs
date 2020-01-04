using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;

namespace UnikeyFactoryTest.IRepository
{
    public interface IAdministratedTestRepository
    {
        void Update_Save(AdministratedTest adTest);
        void Add(AdministratedTest adTest);
        AdministratedTest GetAdministratedTestById(int adTestId);

    }
}
