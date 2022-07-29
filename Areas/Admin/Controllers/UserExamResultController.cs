using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SinavUygulamasi.Data;
using SinavUygulamasi.Models;

namespace SinavUygulamasi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserExamResultController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserExamResultController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UserExamResult
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserExamResults.Include(u => u.Exam);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/UserExamResult/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userExamResult = await _context.UserExamResults
                .Include(u => u.Exam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userExamResult == null)
            {
                return NotFound();
            }

            return View(userExamResult);
        }

        // GET: Admin/UserExamResult/Create
        public IActionResult Create()
        {
            ViewData["ExamId"] = new SelectList(_context.Exam, "Id", "Text");
            return View();
        }

        // POST: Admin/UserExamResult/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ExamId,CorrectAnswersCount,WrongAnswersCount,ResultDate")] UserExamResult userExamResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userExamResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExamId"] = new SelectList(_context.Exam, "Id", "Text", userExamResult.ExamId);
            return View(userExamResult);
        }

        // GET: Admin/UserExamResult/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userExamResult = await _context.UserExamResults.FindAsync(id);
            if (userExamResult == null)
            {
                return NotFound();
            }
            ViewData["ExamId"] = new SelectList(_context.Exam, "Id", "Text", userExamResult.ExamId);
            return View(userExamResult);
        }

        // POST: Admin/UserExamResult/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ExamId,CorrectAnswersCount,WrongAnswersCount,ResultDate")] UserExamResult userExamResult)
        {
            if (id != userExamResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userExamResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExamResultExists(userExamResult.Id))
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
            ViewData["ExamId"] = new SelectList(_context.Exam, "Id", "Text", userExamResult.ExamId);
            return View(userExamResult);
        }

        // GET: Admin/UserExamResult/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userExamResult = await _context.UserExamResults
                .Include(u => u.Exam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userExamResult == null)
            {
                return NotFound();
            }

            return View(userExamResult);
        }

        // POST: Admin/UserExamResult/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userExamResult = await _context.UserExamResults.FindAsync(id);
            _context.UserExamResults.Remove(userExamResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExamResultExists(int id)
        {
            return _context.UserExamResults.Any(e => e.Id == id);
        }
    }
}
