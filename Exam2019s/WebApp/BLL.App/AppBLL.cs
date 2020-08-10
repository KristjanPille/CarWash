using BLL.App.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain.App;
using ee.itcollege.carwash.kristjan.BLL.Base;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }
        public IQuizService Quizzes =>
            GetService<IQuizService>(() => new QuizService(UOW));
        
        public IQuestionService Questions =>
            GetService<IQuestionService>(() => new QuestionService(UOW));
        
        public IQuestionAnswerService QuestionAnswers =>
            GetService<IQuestionAnswerService>(() => new QuestionAnswerService(UOW));
        
        public IScoreService Scores =>
            GetService<IScoreService>(() => new ScoreService(UOW));
    }
}