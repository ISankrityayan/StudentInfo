using System;

using System.Collections.Generic;

namespace Application
{
	public class Course
	{
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string InstructorName { get; set; }

        private Teacher assignedTeacher;
        private List<Student> enrolledStudents = new List<Student>();

        public void AssignTeacher(Teacher teacher)
        {
            assignedTeacher = teacher;
            teacher.AssignCourse(this);
        }

        public void UpdateCourseInfo(string courseCode, string courseName, string instructor)
        {
            CourseCode = courseCode;
            CourseName = courseName;
            InstructorName = instructor;
        }

        public void DisplayCourseInfo()
        {
            Console.WriteLine($"Course ID: {CourseID}, Name: {CourseName}, Code: {CourseCode}, Instructor: {InstructorName}");
            Console.WriteLine("Enrolled Students:");
            foreach (var student in enrolledStudents)
            {
                Console.WriteLine($"  - {student.FirstName} {student.LastName}");
            }
        }

        public void EnrollStudent(Student student)
        {
            enrolledStudents.Add(student);
        }

        public Teacher GetTeacher()
        {
            return assignedTeacher;
        }
    }
}

