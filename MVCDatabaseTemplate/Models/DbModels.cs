using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace BestPracticeTemplate.Models
{
    [Table("Course")]
    public class StudentCourse
    {
        [Key]
        public int course_No { get; set; }
        public string description { get; set; }
        public Decimal? cost { get; set; }
        public int? prerequisite { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }
    }

    public class CourseDbContext : DbContext
        {
            public DbSet<StudentCourse> courses { get; set; }
        }

}