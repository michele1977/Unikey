

using ClassLibrary1.Business.Entities;

namespace ClassLibrary1.DAL.Mapper
{
    public static class PossibleAnswerMapper
    {
        public static PossibleAnswer MapDalToEntity(DAL.DAO.PossibleAnswer arg)
        {
            var returned = new PossibleAnswer()
            {
                Text = arg.Text,
                Id = arg.Id,
                IsCorrect = arg.IsCorrect
            };
            return returned;
        }

        public static DAL.DAO.PossibleAnswer MapEntityToDal(PossibleAnswer arg)
        {
            var returned = new DAL.DAO.PossibleAnswer()
            {
                Text = arg.Text,
                Id = arg.Id,
                IsCorrect = arg.IsCorrect
            };
            return returned;
        }
    }
}
