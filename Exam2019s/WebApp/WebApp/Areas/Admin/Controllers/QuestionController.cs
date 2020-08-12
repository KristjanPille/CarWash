using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuestionAnswer = BLL.App.DTO.QuestionAnswer;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class QuestionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;

        public QuestionController(AppDbContext context, IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }

        // GET: Question
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Questions.Include(q => q.Quiz);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Question/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Question/Create
        public IActionResult Create()
        {
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "NameOfQuiz");
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> Create([Bind("NameOfQuestion,CorrectAnswerId,QuizId,Id")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.Id = Guid.NewGuid();
                _context.Add(question);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Answers", new { QuestionId = question.Id });
            }
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "NameOfQuiz", question.QuizId);
            return RedirectToAction(nameof(Index));
        }
        
        
        public IActionResult Answers(Guid QuestionId)
        {
            ViewData["QuestionId"] = QuestionId;
            return View();
        }
        
        // POST: QuestionAnswer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> CreateAnswer([Bind("Answer1, Answer1Checked, Answer2, Answer2Checked, Answer3, Answer3Checked, Answer4, Answer4Checked, Answer5, Answer5Checked, Id, QuestionId")] PublicApi.DTO.v1.QuestionAnswerDummy questionAnswerDummy)
        {

            if (ModelState.IsValid)
            {
                questionAnswerDummy.Id = Guid.NewGuid();

                if (questionAnswerDummy.Answer1 != null)
                {
                    var questionAnswer = new Domain.App.QuestionAnswer()
                    {
                        Answer = questionAnswerDummy.Answer1,
                        QuestionId = questionAnswerDummy.QuestionId,
                        Id = Guid.NewGuid(),
                    };
                    _context.Add(questionAnswer);
                    await _context.SaveChangesAsync();
                    
                    if (questionAnswerDummy.Answer1Checked == true)
                    {
                        var question = await _context.Questions.FirstOrDefaultAsync(m => m.Id == questionAnswerDummy.QuestionId);
                        if (question != null)
                        {
                            question.CorrectAnswerId = questionAnswer.Id;
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                if (questionAnswerDummy.Answer2 != null)
                {
                    var questionAnswer = new Domain.App.QuestionAnswer()
                    {
                        Answer = questionAnswerDummy.Answer2,
                        QuestionId = questionAnswerDummy.QuestionId,
                        Id = Guid.NewGuid(),
                    };
                    _context.Add(questionAnswer);
                    await _context.SaveChangesAsync();
                    if (questionAnswerDummy.Answer2Checked == true)
                    {
                        var question = await _context.Questions.FirstOrDefaultAsync(m => m.Id == questionAnswerDummy.QuestionId);
                        if (question != null)
                        {
                            question.CorrectAnswerId = questionAnswer.Id;
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                if (questionAnswerDummy.Answer3 != null)
                {
                    var questionAnswer = new Domain.App.QuestionAnswer()
                    {
                        Answer = questionAnswerDummy.Answer3,
                        QuestionId = questionAnswerDummy.QuestionId,
                        Id = Guid.NewGuid(),
                    };
                    _context.Add(questionAnswer);
                    await _context.SaveChangesAsync();
                    if (questionAnswerDummy.Answer3Checked == true)
                    {
                        var question = await _context.Questions.FirstOrDefaultAsync(m => m.Id == questionAnswerDummy.QuestionId);
                        if (question != null)
                        {
                            question.CorrectAnswerId = questionAnswer.Id;
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                if (questionAnswerDummy.Answer4 != null)
                {
                    var questionAnswer = new Domain.App.QuestionAnswer()
                    {
                        Answer = questionAnswerDummy.Answer4,
                        QuestionId = questionAnswerDummy.QuestionId,
                        Id = Guid.NewGuid(),
                    };
                    _context.Add(questionAnswer);
                    await _context.SaveChangesAsync();
                    if (questionAnswerDummy.Answer4Checked == true)
                    {
                        var question = await _context.Questions.FirstOrDefaultAsync(m => m.Id == questionAnswerDummy.QuestionId);
                        if (question != null)
                        {
                            question.CorrectAnswerId = questionAnswer.Id;
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                if (questionAnswerDummy.Answer5 != null)
                {
                    var questionAnswer = new Domain.App.QuestionAnswer()
                    {
                        Answer = questionAnswerDummy.Answer5,
                        QuestionId = questionAnswerDummy.QuestionId,
                        Id = Guid.NewGuid(),
                    };
                    _context.Add(questionAnswer);
                    await _context.SaveChangesAsync();
                    if (questionAnswerDummy.Answer5Checked == true)
                    {
                        var question = await _context.Questions.FirstOrDefaultAsync(m => m.Id == questionAnswerDummy.QuestionId);
                        if (question != null)
                        {
                            question.CorrectAnswerId = questionAnswer.Id;
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                questionAnswerDummy.QuestionId = null;
            }
            
            return RedirectToAction(nameof(Index));
        }

        // GET: Question/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answers = await _bll.QuestionAnswers.GetQuestionAnswers(id);

            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "NameOfQuiz", question.QuizId);
            ViewData["CorrectAnswerId"] = new SelectList(answers, "Id", "Answer");
            return View(question);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("NameOfQuestion,CorrectAnswerId,QuizId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "NameOfQuiz", question.QuizId);
            return View(question);
        }

        // GET: Question/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Questions
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(Guid id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
