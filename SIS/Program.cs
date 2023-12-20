using Application;
using System.Collections.Generic;


class Program {

    static void Main(string[] args)
    {

        Student student = new Student { StudentID = 1, FirstName = "Will", LastName = "Byers" };
        Course course = new Course { CourseID = 101, CourseName = "Mathematics" };
        Teacher teacher = new Teacher(teacherId: 1, firstName: "Mike", lastName: "Wheeler", email: "mike@gmail.com");
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nStudent Information System");
            Console.WriteLine("1: Enroll a Student in a Course");
            Console.WriteLine("2: Update Student Information");
            Console.WriteLine("3: Make a Payment");
            Console.WriteLine("4: Display Student Information");
            Console.WriteLine("5: Assign Teacher to a Course");
            Console.WriteLine("6: Update Course Information");
            Console.WriteLine("7: Display Course Information");
            Console.WriteLine("8: Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    student.EnrollInCourse(course);
                    break;
                case "2":
                    student.UpdateStudentInfo("Nancy", "Wheeler", new DateTime(2000, 1, 1), "nancy@gmail.com", "1234567890");
                    break;
                case "3":
                    student.MakePayment(100.00m, DateTime.Now);
                    break;
                case "4":
                    student.DisplayStudentInfo();
                    break;
                case "5":
                    course.AssignTeacher(teacher);
                    break;
                case "6":
                    course.UpdateCourseInfo("MATH101", "Advanced Mathematics", "Dr. Steve");
                    break;
                case "7":
                    course.DisplayCourseInfo();
                    break;
                case "8":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
}
