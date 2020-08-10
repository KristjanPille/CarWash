#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    [Authorize]
    public class QuizzesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;

        public QuizzesController(AppDbContext context, IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }

        // GET: Quizs
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var result = await _bll.Quizzes.GetAllAsync();
            return View(result);
        }


        public async Task<IActionResult> UpdateStatistics(Guid? id)
        {
            if (id != null)
            {
                var Quiz = await _bll.Quizzes.FirstOrDefaultAsync(id.Value);
                if (Quiz != null)
                {
                    await _bll.Quizzes.UpdateAsync(Quiz);
                }
            }

            return RedirectToAction(nameof(Index));
        }


        [AllowAnonymous]
        // GET: Quizs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Quiz = await _context.Quizzes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Quiz == null)
            {
                return NotFound();
            }

            return View(Quiz);
        }
        
        // GET: Quizs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind(
                "NameOfQuiz,Description,DiscountAmount")]
            Quiz Quiz)
        {
            if (ModelState.IsValid)
            {
                Quiz.Id = Guid.NewGuid();
                _context.Add(Quiz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Quiz);
        }

        // GET: Quizs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Quiz = await _context.Quizzes.FindAsync(id);
            if (Quiz == null)
            {
                return NotFound();
            }
            return View(Quiz);
        }

        // POST: Quizs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind(
                "Name,Description,RecordedAt,Duration,Speed,Distance,Climb,Descent,PaceMin,PaceMax,QuizTypeId,AppUserId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")]
            Quiz Quiz)
        {
            if (id != Quiz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Quiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(Quiz.Id))
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
            return View(Quiz);
        }

        // GET: Quizs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Quiz = await _context.Quizzes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Quiz == null)
            {
                return NotFound();
            }

            return View(Quiz);
        }

        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var Quiz = await _context.Quizzes.FindAsync(id);
            _context.Quizzes.Remove(Quiz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(Guid id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }
    }
}
