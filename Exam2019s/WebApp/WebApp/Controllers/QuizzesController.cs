using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using Question = Domain.App.Question;
using QuestionAnswer = Domain.App.QuestionAnswer;
using Quiz = Domain.App.Quiz;

namespace WebApp.Controllers
{
    public class QuizzesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;
        private readonly QuestionMapperV2 _mapperV2 = new QuestionMapperV2();
        private readonly QuestionMapper _mapper = new QuestionMapper();

        public QuizzesController(AppDbContext context, IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }

        // GET: Quizzes
        public async Task<IActionResult> Index()
        {
            var quizList = await _context.Quizzes.ToListAsync();
            List<QuizDummy> dummyList = new List<QuizDummy>();
            
            foreach (var quiz in quizList)
            {
                QuizDummy dummy = new QuizDummy();
                dummy.Id = quiz.Id;
                dummy.NameOfQuiz = quiz.NameOfQuiz;
                var score = await _bll.Scores.GetAverageScore(quiz.Id);
                dummy.AverageScore = score;
                
                dummyList.Add(dummy);
            }
            
            return View(dummyList);
        }

        // GET: Quizzes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // GET: Quizzes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            PublicApi.DTO.v1.QuestionAnswerDummyV2 questionAnswerDummy1 = new PublicApi.DTO.v1.QuestionAnswerDummyV2();
            PublicApi.DTO.v1.QuestionAnswerDummyV2 questionAnswerDummy2 = new PublicApi.DTO.v1.QuestionAnswerDummyV2();
            PublicApi.DTO.v1.QuestionAnswerDummyV2 questionAnswerDummy3 = new PublicApi.DTO.v1.QuestionAnswerDummyV2();
            PublicApi.DTO.v1.QuestionAnswerDummyV2 questionAnswerDummy4 = new PublicApi.DTO.v1.QuestionAnswerDummyV2();
            PublicApi.DTO.v1.QuestionAnswerDummyV2 questionAnswerDummy5 = new PublicApi.DTO.v1.QuestionAnswerDummyV2();
            
            
            DAL.App.DTO.QuestionAnswer answerForQuestion1 = new DAL.App.DTO.QuestionAnswer();
            DAL.App.DTO.QuestionAnswer answerForQuestion2 = new DAL.App.DTO.QuestionAnswer();
            DAL.App.DTO.QuestionAnswer answerForQuestion3 = new DAL.App.DTO.QuestionAnswer();
            DAL.App.DTO.QuestionAnswer answerForQuestion4 = new DAL.App.DTO.QuestionAnswer();
            DAL.App.DTO.QuestionAnswer answerForQuestion5 = new DAL.App.DTO.QuestionAnswer();

            var index = 0;
            
            var dummyList = new List<PublicApi.DTO.v1.QuestionAnswerDummyV2>();
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            IEnumerable<DAL.App.DTO.Question> questions = await _bll.Questions.GetQuizQuestions(quiz.Id);
            QuizDummy dummy = new QuizDummy();
            dummy.Id = quiz.Id;
            dummy.NameOfQuiz = quiz.NameOfQuiz;
            dummy.Questions = questions;

            foreach (var question in questions)
            {
                IEnumerable<DAL.App.DTO.QuestionAnswer> questionAnswers = await _bll.QuestionAnswers.GetQuestionAnswers(question.Id);
                List<DAL.App.DTO.QuestionAnswer> test = new List<DAL.App.DTO.QuestionAnswer>();
                PublicApi.DTO.v1.QuestionAnswerDummyV2 questionAnswerDummy = new PublicApi.DTO.v1.QuestionAnswerDummyV2();
                questionAnswerDummy.ListOfAnswers = new List<DAL.App.DTO.QuestionAnswer>();
                foreach (var questionAnswer in questionAnswers)
                {
                    test.Add(questionAnswer);
                    questionAnswerDummy.ListOfAnswers.Add(questionAnswer);
                }
                questionAnswerDummy.QuestionId = question.Id;

                var dbQuestion = await _bll.Questions.FirstOrDefaultAsync(question.Id);
                
                questionAnswerDummy.Question = _mapper.Map(dbQuestion);
                
                questionAnswerDummy.Answer1 = new DAL.App.DTO.QuestionAnswer();
                questionAnswerDummy.Answer1.Answer = test[0].Answer;
                
                questionAnswerDummy.Answer2 = new DAL.App.DTO.QuestionAnswer();
                if (test.Count >= 2)
                {
                    questionAnswerDummy.Answer2.Answer = test[1].Answer;  
                }
                questionAnswerDummy.Answer3 = new DAL.App.DTO.QuestionAnswer();
                if (test.Count >= 3)
                { 
                    questionAnswerDummy.Answer3.Answer = test[2].Answer;  
                }
                questionAnswerDummy.Answer4 = new DAL.App.DTO.QuestionAnswer();
                if (test.Count >= 4)
                {
                    questionAnswerDummy.Answer4.Answer = test[3].Answer;  
                }
                questionAnswerDummy.Answer5 = new DAL.App.DTO.QuestionAnswer();
                if (test.Count >= 5)
                {
                    questionAnswerDummy.Answer5.Answer = test[4].Answer;  
                }
                dummyList.Add(questionAnswerDummy);

                switch (index)
                {
                    case 0:
                        questionAnswerDummy1 = questionAnswerDummy;
                        break;
                    case 1:
                        questionAnswerDummy2 = questionAnswerDummy;
                        break;
                    case 2:
                        questionAnswerDummy3 = questionAnswerDummy;
                        break;
                    case 3:
                        questionAnswerDummy4 = questionAnswerDummy;
                        break;
                    case 4:
                        questionAnswerDummy5 = questionAnswerDummy;
                        break;
                }
                index += 1;
            }
            ViewData["questionAnswerDummy1"] = new SelectList(questionAnswerDummy1.ListOfAnswers);
            
            // Bind selected value from selectelist to here
            ViewData["answerForQuestion1"] = null;
            
            if (questionAnswerDummy2.ListOfAnswers != null)
            {
                ViewData["questionAnswerDummy2"] = new SelectList(questionAnswerDummy2.ListOfAnswers);
            }
            if (questionAnswerDummy3.ListOfAnswers != null)
            {
                ViewData["questionAnswerDummy3"] = new SelectList(questionAnswerDummy3.ListOfAnswers);
            }
            if (questionAnswerDummy4.ListOfAnswers != null)
            {
                ViewData["questionAnswerDummy4"] = new SelectList(questionAnswerDummy4.ListOfAnswers);
            }
            if (questionAnswerDummy5.ListOfAnswers != null)
            {
                ViewData["questionAnswerDummy5"] = new SelectList(questionAnswerDummy5.ListOfAnswers);
            }
            return View(dummyList);
        }

        // POST: Quizzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("NameOfQuestion, Id, QuestionId, answerForQuestion1")] IEnumerable<PublicApi.DTO.v1.QuestionAnswerDummyV2> questionAnswerDummy)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction(nameof(Index));
            }
            return View(questionAnswerDummy);
        }

        
        
        
        
        // GET: Quizzes/Answer/5
        public async Task<IActionResult> Answer(Guid? id)
        {
            DAL.App.DTO.QuestionAnswer answerForQuestion = new DAL.App.DTO.QuestionAnswer();

            var dummy= new PublicApi.DTO.v1.QuestionAnswer();
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            // Find quiz specific Questions
            IEnumerable<DAL.App.DTO.Question> questions = await _bll.Questions.GetQuizQuestions(quiz.Id);
            
            // Find Answers to question
            IEnumerable<DAL.App.DTO.QuestionAnswer> questionAnswer = await _bll.QuestionAnswers.GetQuestionAnswers(questions.First().Id);

            
            return View(dummy);
        }

        // POST: Quizzes/Answer/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Answer(Guid id, [Bind("NameOfQuestion, Id, QuestionId, answerForQuestion1")] PublicApi.DTO.v1.QuestionAnswer questionAnswerDummy)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction(nameof(Index));
            }
            return View(questionAnswerDummy);
        }
        
        
        
        
        
        
        // POST: Quizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(Guid id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }
    }
}
