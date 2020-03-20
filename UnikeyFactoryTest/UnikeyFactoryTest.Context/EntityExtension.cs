using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;

namespace UnikeyFactoryTest.Context
{
    public abstract class EntityExtension
    {
        public abstract int MyId { get; }
        public abstract List<EntityExtension> Childs { get; }
        public abstract void SetFlatProperty(EntityExtension newEntity);
        public abstract void RemoveChild(EntityExtension toRemove, TestPlatformDBEntities ctx);
        public abstract void AddChild(EntityExtension toAdd, TestPlatformDBEntities ctx);

    }

    public partial class Test : EntityExtension
    {
        public override int MyId => Id;
        public override List<EntityExtension> Childs => Questions.Select(x => (EntityExtension) x).ToList();
        public override void SetFlatProperty(EntityExtension newEntity)
        {
            Date = ((Test) newEntity).Date;
            Title = ((Test) newEntity).Title;
            UserId = ((Test) newEntity).UserId;
        }

        public override void RemoveChild(EntityExtension toRemove, TestPlatformDBEntities ctx)
        {
            ctx.Tests.FirstOrDefault(x => x.Id == Id).Questions.Remove((Question)toRemove);
        }

        public override void AddChild(EntityExtension toAdd, TestPlatformDBEntities ctx)
        {
            ctx.Tests.FirstOrDefault(x=>x.Id == Id).Questions.Add((Question)toAdd);
        }
    }

    public partial class Question : EntityExtension
    {
        public override int MyId => Id;

        public override List<EntityExtension> Childs => Answers.Select(x=> (EntityExtension)x).ToList();

        public override void AddChild(EntityExtension toAdd, TestPlatformDBEntities ctx)
        {
            ctx.Questions.FirstOrDefault(x=>x.Id == MyId).Answers.Add((Answer)toAdd);
        }

        public override void RemoveChild(EntityExtension toRemove, TestPlatformDBEntities ctx)
        {
            ctx.Questions.FirstOrDefault(x => x.Id == MyId).Answers.Remove((Answer) toRemove);

        }

        public override void SetFlatProperty(EntityExtension newEntity)
        {
            Text = ((Question) newEntity).Text;
            Position = ((Question) newEntity).Position;
            TestId = ((Question) newEntity).TestId;
        }
    }

    public partial class Answer : EntityExtension
    {
        public override int MyId => Id;
        public override List<EntityExtension> Childs => new List<EntityExtension>();
        public override void SetFlatProperty(EntityExtension newEntity)
        {
            QuestionId = ((Answer) newEntity).QuestionId;
            Text = ((Answer) newEntity).Text;
            IsCorrect = ((Answer) newEntity).IsCorrect;
            Score = ((Answer) newEntity).Score;
        }

        public override void RemoveChild(EntityExtension toRemove, TestPlatformDBEntities ctx)
        {

        }

        public override void AddChild(EntityExtension toAdd, TestPlatformDBEntities ctx)
        {

        }
    }

    public partial class AdministratedTest : EntityExtension
    {
        public override int MyId => Id;
        public override List<EntityExtension> Childs => AdministratedQuestions.Select(x=> (EntityExtension)x).ToList();
        public override void SetFlatProperty(EntityExtension newEntity)
        {
            Title = ((AdministratedTest) newEntity).Title;
            MaxScore = ((AdministratedTest) newEntity).MaxScore;
            TestSubject = ((AdministratedTest) newEntity).TestSubject;
            Date = ((AdministratedTest) newEntity).Date;
            State = ((AdministratedTest) newEntity).State;
            Score = ((AdministratedTest) newEntity).Score;
        }

        public override void RemoveChild(EntityExtension toRemove, TestPlatformDBEntities ctx)
        {
            ctx.AdministratedTests.FirstOrDefault(x => x.Id == MyId).AdministratedQuestions
                .Remove((AdministratedQuestion) toRemove);
        }

        public override void AddChild(EntityExtension toAdd, TestPlatformDBEntities ctx)
        {
            ctx.AdministratedTests.FirstOrDefault(x=> x.Id == MyId).AdministratedQuestions.Add((AdministratedQuestion)toAdd);
        }
    }

    public partial class AdministratedQuestion : EntityExtension
    {
        public override int MyId => Id;

        public override List<EntityExtension> Childs =>
            AdministratedAnswers.Select(x => (EntityExtension) x).ToList();
        public override void SetFlatProperty(EntityExtension newEntity)
        {
            Text = ((AdministratedQuestion) newEntity).Text;
            Position = ((AdministratedQuestion) newEntity).Position;
        }

        public override void RemoveChild(EntityExtension toRemove, TestPlatformDBEntities ctx)
        {
            ctx.AdministratedQuestions.FirstOrDefault(x=>x.Id == MyId).AdministratedAnswers.Remove((AdministratedAnswer)toRemove);
        }

        public override void AddChild(EntityExtension toAdd, TestPlatformDBEntities ctx)
        {
            ctx.AdministratedQuestions.FirstOrDefault(x=>x.Id == MyId).AdministratedAnswers.Add((AdministratedAnswer)toAdd);
        }
    }

    public partial class AdministratedAnswer : EntityExtension
    {
        public override int MyId => Id;
        public override List<EntityExtension> Childs => new List<EntityExtension>();
        public override void SetFlatProperty(EntityExtension newEntity)
        {
            Text = ((AdministratedAnswer) newEntity).Text;
            isCorrect = ((AdministratedAnswer) newEntity).isCorrect;
            isSelected = ((AdministratedAnswer) newEntity).isSelected;
            Score = ((AdministratedAnswer) newEntity).Score;
        }

        public override void RemoveChild(EntityExtension toRemove, TestPlatformDBEntities ctx)
        {

        }

        public override void AddChild(EntityExtension toAdd, TestPlatformDBEntities ctx)
        {

        }
    }
}
