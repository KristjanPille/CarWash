using Contracts.DAL.App.Repositories;
using ee.itcollege.carwash.kristjan.Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        IQuizRepository Quizzes { get; }
        IQuestionRepository Questions { get; }
        
        IQuestionAnswerRepository QuestionAnswers{ get; }
        
        IScoreRepository Scores{ get; }
    }
}