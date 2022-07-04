using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinavUygulamasi.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public Answer Answer { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public DateTime AnswerDate { get; set; }
    }
}
