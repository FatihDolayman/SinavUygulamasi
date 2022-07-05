using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SinavUygulamasi.Data;
using SinavUygulamasi.Models;
using SinavUygulamasi.ViewModel;

namespace SinavUygulamasi.Controllers
{
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ExamController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var exams = _context.Exams.ToList();
            var userExamResults = _context.UserExamResults.Where(a => a.UserId == user.Id);
            var model = new List<UserExamListViewModel>();
            foreach (var exam in exams)
            {
                var userExamResult = userExamResults.FirstOrDefault(a => a.ExamId == exam.Id); var userExam = new UserExamListViewModel
                {
                    Id = exam.Id,
                    TextTitle = exam.TextTitle,
                };
                if (userExamResult != null)
                {
                    userExam.IsTaken = true;
                    userExam.ResultDate = userExamResult.ResultDate;
                    userExam.CorrectAnswersCount = userExamResult.CorrectAnswersCount;
                    userExam.WrongAnswersCount = userExamResult.WrongAnswersCount;
                }
                model.Add(userExam);
            }
            return View(model);
        }

        // GET: Exam/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }   
        private bool ExamExists(int id)
        {
            return _context.Exam.Any(e => e.Id == id);
        }
    }
}
