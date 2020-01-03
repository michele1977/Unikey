using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnikeyFactoryTest.IRepository
{
    public interface IAdministratedTestRepository
    {
        void Update_Save(Domain.AdministratedTest adTest);
        void Add(Domain.AdministratedTest adTest);
        Domain.AdministratedTest GetAdministratedTestById(int adTestId);

    }
}
