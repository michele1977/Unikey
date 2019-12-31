using System.Data.Entity;
using System.Linq;
using ClassLibrary1.DAL.DAO;
using ClassLibrary1.DAL.Mapper;

namespace ClassLibrary1.DAL.Repositories
{
    public class TestExRepository
    {
        readonly DAO.SingletonContext _context = new DAO.SingletonContext();

        public void AddTestEx(ref Business.Entities.ExTest exTest)
        {
            var dbContext = _context.GetInstance();
            var testToAdd = TestExMapper.MapEntityToDal(exTest);
            dbContext.ExTests.Add(testToAdd);
            dbContext.SaveChanges();
            exTest = TestExMapper.MapDalToEntity(testToAdd);
        }

        public Business.Entities.ExTest GetTestEx(int exTestId)
        {
            var dbContext = _context.GetInstance();
            var exTest = dbContext.ExTests.FirstOrDefault(x => x.Id == exTestId);
            return TestExMapper.MapDalToEntity(exTest);
        }
        public void UpdateTestEx(ref Business.Entities.ExTest test)
        {
            var dbContext = _context.GetInstance();
            var newTest = TestExMapper.MapEntityToDal(test);
            var testFromDb = dbContext.ExTests.Where(x => x.Id == newTest.Id).Include(x => x.ExTest_Question).FirstOrDefault();
            testFromDb = (ExTest)UpdateEntity(testFromDb, newTest);
            dbContext.SaveChanges();
            test = TestExMapper.MapDalToEntity(testFromDb);
        }

        BaseEntity UpdateEntity(DAO.BaseEntity oldValue, DAO.BaseEntity newValue)
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
                UpdateEntity(itemToUpdate, child);
            }
            return valueFromDb;
        }
    }
}
