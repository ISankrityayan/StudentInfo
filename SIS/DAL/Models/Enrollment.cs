using System;
namespace Application
{
	public class Enrollment
	{
		public int EnrollmentId { get; set; }
		public Student StudentId { get; set; }
        public int CourseID { get; set; } 
        public DateTime EnrollmentDate { get; set; }
    }
}

