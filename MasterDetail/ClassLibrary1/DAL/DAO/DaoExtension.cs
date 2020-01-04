using System.Collections.Generic;
using System.Linq;
using ClassLibrary1.DAL.Mapper;

namespace ClassLibrary1.DAL.DAO
{
    public abstract class BaseEntity
    {
        public abstract int InternalId { get;  }
        public abstract List<BaseEntity> Childs { get;  }
        public abstract void SetFlatProperties(BaseEntity newValue);
        public abstract void RemoveChild(AssestmentContext dbContext, BaseEntity childToRemove);
        public abstract void AddChild(BaseEntity childToRemove);

    }
    public partial class Question : BaseEntity
    {
        public override int InternalId => Id;
        public override List<BaseEntity> Childs =>  PossibleAnswers.Select(x => (BaseEntity)x).ToList();

        public override void SetFlatProperties(BaseEntity newValue)
        {
            QuestionText = ((Question)newValue).QuestionText;
            Type = ((Question)newValue).Type;
            State = ((Question)newValue).State;
        }

        public override void RemoveChild(AssestmentContext dbContext,BaseEntity childToRemove)
        {
            var itemToRemove = PossibleAnswers.FirstOrDefault(x => x.InternalId == childToRemove.InternalId);
            PossibleAnswers.Remove(itemToRemove);
            dbContext.PossibleAnswers.Remove(itemToRemove);
        }

        public override void AddChild(BaseEntity childToAdd)
        {
            PossibleAnswers.Add((PossibleAnswer)childToAdd);
        }
    }
    public partial class Test : BaseEntity
    {
        public override int InternalId => Id;

        public override List<BaseEntity> Childs => Questions.Select(x => (BaseEntity)x).ToList();

        public override void SetFlatProperties(BaseEntity newValue)
        {
            CreationDate = ((Test)newValue).CreationDate;
            Title = ((Test)newValue).Title;
        }
        public override void RemoveChild(AssestmentContext dbContext, BaseEntity childToRemove)
        {
            var itemToRemove = Questions.FirstOrDefault(x => x.InternalId == childToRemove.InternalId);
            Questions.Remove(itemToRemove);
            dbContext.Questions.Remove(itemToRemove);
        }
        public override void AddChild(BaseEntity childToAdd)
        {
            Questions.Add((Question)childToAdd);
        }
    }
    public partial class PossibleAnswer : BaseEntity
    {
        public override int InternalId => Id;

        public override List<BaseEntity> Childs => new List<BaseEntity>();

        public override void SetFlatProperties(BaseEntity newValue)
        {
            Text = ((PossibleAnswer)newValue).Text;
            IsCorrect = ((PossibleAnswer)newValue).IsCorrect;
        }
        public override void RemoveChild(AssestmentContext dbContext, BaseEntity childToRemove)
        {
        }

        public override void AddChild(BaseEntity childToRemove)
        {
        }
    }


    public partial class ExTest : BaseEntity
    {
        public override int InternalId => Id;

        public override List<BaseEntity> Childs => ExTest_Question.Select(x => (BaseEntity)x).ToList();

        public override void SetFlatProperties(BaseEntity newValue)
        {
            ExecutionDate = ((ExTest) newValue).ExecutionDate;
            //Id = ((ExTest)newValue).Id;
            Name = ((ExTest)newValue).Name;
            State = ((ExTest)newValue).State;
        }
        public override void RemoveChild(AssestmentContext dbContext, BaseEntity childToRemove)
        {
            var itemToRemove = ExTest_Question.FirstOrDefault(x => x.InternalId == childToRemove.InternalId);
            ExTest_Question.Remove(itemToRemove);
            dbContext.ExTest_Question.Remove(itemToRemove);
        }
        public override void AddChild(BaseEntity childToAdd)
        {
            ExTest_Question.Add((ExTest_Question)childToAdd);
        }
    }
    public partial class ExTest_Question : BaseEntity
    {
        public override int InternalId => Id;
        public override List<BaseEntity> Childs => Pa_ExQuestion.Select(x => (BaseEntity)x).ToList();

        public override void SetFlatProperties(BaseEntity newValue)
        {
            Position = ((ExTest_Question)newValue).Position;
            
            //QuestionText = ((Question)newValue).QuestionText;
            //Type = ((Question)newValue).Type;
            //State = ((Question)newValue).State;
        }

        public override void RemoveChild(AssestmentContext dbContext, BaseEntity childToRemove)
        {
            var itemToRemove = Pa_ExQuestion.FirstOrDefault(x => x.InternalId == childToRemove.InternalId);
            Pa_ExQuestion.Remove(itemToRemove);
            dbContext.Pa_ExQuestion.Remove(itemToRemove);
        }

        public override void AddChild(BaseEntity childToAdd)
        {
            Pa_ExQuestion.Add((Pa_ExQuestion)childToAdd);
        }
    }

    public partial class Pa_ExQuestion : BaseEntity
    {
        public override int InternalId => Id;
        public override List<BaseEntity> Childs => new List<BaseEntity>();

        public override void SetFlatProperties(BaseEntity newValue)
        {
            
            //QuestionText = ((Question)newValue).QuestionText;
            //Type = ((Question)newValue).Type;
            //State = ((Question)newValue).State;
        }

        public override void RemoveChild(AssestmentContext dbContext, BaseEntity childToRemove)
        {

        }

        public override void AddChild(BaseEntity childToAdd)
        {
        }
    }

}
