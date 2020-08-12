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
        
        
        // GET: Quizzes/Answer/5
        public async Task<IActionResult> Answer(Guid? id)
        {
            DAL.App.DTO.QuestionAnswer answerForQuestion = new DAL.App.DTO.QuestionAnswer();

            var dummy =  new PublicApi.DTO.v1.QuestionAnswerDummyV2();
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

            dummy.Question = questions.First();
            dummy.QuizName = quiz.NameOfQuiz;
            // Find Answers to question
            IEnumerable<DAL.App.DTO.QuestionAnswer> questionAnswers = await _bll.QuestionAnswers.GetQuestionAnswers(questions.First().Id);
            
            
            ViewData["CorrectAnswerId"] = new SelectList(questionAnswers, "Id", "Answer");
            return View(dummy);
        }

        // POST: Quizzes/Answer/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Answer(Guid id, [Bind("NameOfQuestion, Id, QuestionId, CorrectAnswerId")] PublicApi.DTO.v1.QuestionAnswerDummyV2 questionAnswerDummy)
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
