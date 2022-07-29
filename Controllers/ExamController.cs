using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SinavUygulamasi.Data;
using SinavUygulamasi.Models;
using SinavUygulamasi.ViewModel;

namespace SinavUygulamasi.Controllers
{
    [Authorize()]
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ExamController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult>  Index()
        {
            var user =await _userManager.GetUserAsync(User);
            var exams = _context.Exam.ToList();
            var userExamResults = _context.UserExamResults.Where(a => a.UserId == user.Id).ToList();
            var model = new List<UserExamListViewModel>();
            foreach (var exam in exams)
            {
                var userExamResult = userExamResults.FirstOrDefault(a => a.ExamId == exam.Id); 
                var userExam = new UserExamListViewModel
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

        public IActionResult TakeExam(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = _context.Exam.Include(a => a.Questions).FirstOrDefault(a => a.Id == id);
            if (exam == null)
            {
                return NotFound();
            }
            return View(exam);
        }
        [HttpPost]
        public async Task<IActionResult>  CompleteTheExam(string userAnswers, int examId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { Success = false, Message = "Kullanıcı bulunamadı!" });
            }
            var exams = _context.Exam.ToList();
            var userExamResults = _context.UserExamResults.Where(a => a.UserId == user.Id).ToList();

            if (userExamResults.Any(a => a.ExamId == examId))
            {
                return Json(new { Success = false, Message = "Bu sınavı daha önce çözmüş görünüyorsunuz!" });
            }
            try
            {               
                var answers = JsonConvert.DeserializeObject<List<UserAnswerViewModel>>(userAnswers);
                var questions = _context.Question.ToList();
                questions = questions.Where(a => answers.Any(b => b.QuestionId == a.Id)).ToList();
                var correctAnswers = new List<CorrectAnsverViewModel>();
                var userExamResult = new UserExamResult
                {
                    ExamId = examId,
                    ResultDate = DateTime.Now,
                    UserId = user.Id
                };
                foreach (var answer in answers)
                {
                    var correctAnswer = new CorrectAnsverViewModel
                    {
                        QuestionId = answer.QuestionId,
                        Answer = questions.First(a => a.Id == answer.QuestionId).Answer
                    };
                    correctAnswers.Add(correctAnswer);
                    if (!string.IsNullOrEmpty(answer.Answer))
                    {
                        var userAnswer = new UserAnswer
                        {
                            Answer = Enum.Parse<Answer>(answer.Answer),
                            QuestionId = answer.QuestionId,
                            UserId = user.Id,
                            AnswerDate = DateTime.Now
                        };

                        if (correctAnswer.Answer == userAnswer.Answer)
                        {
                            userExamResult.CorrectAnswersCount++;
                        }
                        else
                        {
                            userExamResult.WrongAnswersCount++;
                        }

                        //insert user answer to db
                        _context.UserAnswers.Add(userAnswer);
                    }
                }
                _context.UserExamResults.Add(userExamResult);
                _context.SaveChanges();

                return Json(new { Success = true, CorrectAnswers = correctAnswers });
            }
            catch (Exception ex)
            {
                var test = ex;
                return Json(new { Success = false, Message = "Bir hata oluştu!" });
            }
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
