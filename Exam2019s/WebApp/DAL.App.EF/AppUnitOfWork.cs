﻿using System;
using System.Collections.Generic;
 using Contracts.DAL.App;
 using Contracts.DAL.App.Repositories;
 using ee.itcollege.carwash.kristjan.Contracts.DAL.Base;
 using ee.itcollege.carwash.kristjan.DAL.Base.EF;
 using DAL.App.EF.Repositories;

 namespace DAL.App.EF
{
    public class AppUnitOfOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        public IQuizRepository Quizzes =>
            GetRepository<IQuizRepository>(() => new QuizRepository(UOWDbContext));

        public IQuestionRepository Questions =>
            GetRepository<IQuestionRepository>(() => new QuestionRepository(UOWDbContext));
        
        public IQuestionAnswerRepository QuestionAnswers =>
            GetRepository<IQuestionAnswerRepository>(() => new QuestionAnswersRepository(UOWDbContext));
    }
}