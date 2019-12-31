namespace ClassLibrary1.DAL.DAO
{
    public class SingletonContext
    {
        private static AssestmentContext _dbcontext;

        public AssestmentContext GetInstance()
        {
            return _dbcontext ?? (_dbcontext = new AssestmentContext());
        }
    }
}
