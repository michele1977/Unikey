using System.Linq;
using ClassLibrary1.DAL.DAO;
using ClassLibrary1.DAL.Mapper;

namespace ClassLibrary1.DAL.Repositories
{
    public class TestRepository
    {
        readonly SingletonContext _context = new SingletonContext();

        public void AddTest(ref Business.Entities.Test test)
        {
            var dbContext = _context.GetInstance();
            var testToAdd = TestMapper.MapEntityToDal(test);
            dbContext.Tests.Add(testToAdd);
            dbContext.SaveChanges();
            test= TestMapper.MapDalToEntity(testToAdd);
        }

        public Business.Entities.Test GetTest(int testId)
        {
            var dbContext = _context.GetInstance();
            var test = dbContext.Tests.FirstOrDefault(x => x.Id == testId);
            return TestMapper.MapDalToEntity(test);
        }

        public void UpdateTest(ref Business.Entities.Test test)
        {
            var dbContext = _context.GetInstance();
            var newTest = TestMapper.MapEntityToDal(test);
            var testFromDb = dbContext.Tests.FirstOrDefault(x => x.Id == newTest.Id);
            testFromDb = (Test)UpdateEntity(testFromDb, newTest);
            dbContext.SaveChanges();
            test = TestMapper.MapDalToEntity(testFromDb);
        }



        public BaseEntity  UpdateEntity(DAO.BaseEntity oldValue, DAO.BaseEntity newValue)
        {
            var valueFromDb = oldValue;
            valueFromDb.SetFlatProperties(newValue);
            var childsToRemove = valueFromDb.Childs.Where(x => newValue.Childs.All(y => y.InternalId != x.InternalId)).ToArray();
            var childsToAdd = newValue.Childs.Where(x => valueFromDb.Childs.All(y => y.InternalId != x.InternalId)).ToArray();
            var childsToUpdate = valueFromDb.Childs.Where(x => newValue.Childs.Any(y => y.InternalId == x.InternalId)).ToArray();
            foreach (var child in childsToRemove) valueFromDb.RemoveChild(_context.GetInstance(), child);
            foreach (var child in childsToAdd) valueFromDb.AddChild(child);
            foreach (var child in childsToUpdate)
            {
                var itemToUpdate = valueFromDb.Childs.FirstOrDefault(x => x.InternalId == child.InternalId);
                UpdateEntity( itemToUpdate, child);
            }
            return valueFromDb;
        }
  }
}
