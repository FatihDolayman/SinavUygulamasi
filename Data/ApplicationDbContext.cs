using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SinavUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SinavUygulamasi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Exam> Exam { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<UserExamResult> UserExamResults { get; set; }
        
    }
}
