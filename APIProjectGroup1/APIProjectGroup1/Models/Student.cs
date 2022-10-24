using System;
using System.Collections.Generic;

namespace APIProjectGroup1.Models
{
    public partial class Student
    {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UniversityAttended { get; set; }
        public string? CourseTaken { get; set; }
        public int? MarkAchieved { get; set; }
    }
}
