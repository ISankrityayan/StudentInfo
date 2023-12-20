using System;

using System.Collections.Generic;

namespace Application
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        private List<Course> assignedCourses = new List<Course>();

        public Teacher(int teacherId, string firstName, string lastName, string email)
        {
            TeacherID = teacherId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void UpdateTeacherInfo(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void DisplayTeacherInfo()
        {
            Console.WriteLine($"Teacher ID: {TeacherID}, Name: {FirstName} {LastName}, Email: {Email}");
            Console.WriteLine("Assigned Courses:");
            foreach (var course in assignedCourses)  
            {
                Console.WriteLine($"  - {course.CourseName}");
            }
        }

        public void AssignCourse(Course course)
        {
            assignedCourses.Add(course);
        }

        public List<Course> GetAssignedCourses()
        {
            return assignedCourses;
        }

    }
}

