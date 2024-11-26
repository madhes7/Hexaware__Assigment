using Student_Information_System.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Information_System.Main
{
    internal class S_I_S
    {
        public void Student_Information_System_Main() { 
        IStudentService studentService = new StudentService();
        IEnrollmentService enrollmentService = new EnrollmentService();
        IPaymentService paymentService = new PaymentService();
        ITeacherService teacherService = new TeacherService();
        ICourseService courseService = new CourseService();

        bool continueRunning = true;
            while (continueRunning)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("***********************************************");
                Console.WriteLine("        *** STUDENT MANAGEMENT MENU ***       ");
                Console.WriteLine("***********************************************");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1.  Update Student Information");
                Console.WriteLine("2.  Display Students");
                Console.WriteLine("3.  Get Enrolled Course for Student");
                Console.WriteLine("4.  Enroll Student in Course");
                Console.WriteLine("5.  Make Payment for Student");
                Console.WriteLine("6.  Get Payment History for Student");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("***********************************************");
                Console.WriteLine("7.  Get Enrollments for a Specific Course");
                Console.WriteLine("8.  Get Student by Enrollment ID");
                Console.WriteLine("9.  Get Course by Enrollment ID");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("***********************************************");
                Console.WriteLine("10. Get Payments for Student");
                Console.WriteLine("11. Get Payment Amount");
                Console.WriteLine("12. Get Payment Date");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("***********************************************");
                Console.WriteLine("13. Get Teacher Info");
                Console.WriteLine("14. Get Assigned Courses for Teacher");
                Console.WriteLine("15. Update Teacher Information");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("***********************************************");
                Console.WriteLine("16. Assign Teacher to Course");
                Console.WriteLine("17. Update Course Info");
                Console.WriteLine("18. Display Course Info");
                Console.WriteLine("19. Get Teacher Info for Course");
                Console.WriteLine("20. Calculate Course Statistics");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("21. To Exit");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("***********************************************");
                Console.WriteLine("Please select an option:");

                Console.ResetColor();
                

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        
                        try
                        {
                            Console.Write("Enter Student ID to Update: ");
                            int studentid = int.Parse(Console.ReadLine());

                            Console.Write("Enter First Name: ");
                            string firstName = Console.ReadLine();

                            Console.Write("Enter Last Name: ");
                            string lastName = Console.ReadLine();

                            Console.Write("Enter Date of Birth (yyyy-MM-dd): ");
                            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

                            Console.Write("Enter Email: ");
                            string email = Console.ReadLine();

                            Console.Write("Enter Phone Number: ");
                            string phoneNumber = Console.ReadLine();

                            studentService.UpdateStudentInfo(studentid, firstName, lastName, dateOfBirth, email, phoneNumber);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format. Please try again.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }
                        break;


                    case "2":
                      studentService.DisplayStudentInfo();
                      break;

                    case "3":
                        try
                        {
                            Console.Write("Enter Student ID: ");
                            int StudentId = int.Parse(Console.ReadLine());

                            enrollmentService.GetEnrolledCourses(StudentId);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format. Please enter a valid student ID.");
                        }
                        break;



                    case "4":
                        Console.WriteLine("Enter Student ID:");
                        int studentId = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter Course ID:");
                        int courseId = Convert.ToInt32(Console.ReadLine());


                        enrollmentService.EnrollInCourse(studentId, courseId);
                        break;




                    case "5":
                        try
                        {
                            Console.Write("Enter Student ID: ");
                            int studentID = int.Parse(Console.ReadLine());

                            Console.Write("Enter Payment Amount: ");
                            decimal amount = decimal.Parse(Console.ReadLine());

                           
                           

                            studentService.MakePayment(studentID, amount, DateTime.Now);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter valid details.");
                        }
                        break;



                    case "6":
                        try
                        {
                            Console.Write("Enter Student ID: ");
                            int StudentId = int.Parse(Console.ReadLine());

                            studentService.GetPaymentHistory(StudentId);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid student ID.");
                        }
                        break;


                    case "7":
                        Console.WriteLine("Enter Course ID:");
                        int CourseId = Convert.ToInt32(Console.ReadLine());


                        enrollmentService.GetEnrollments(CourseId);
                        break;




                    case "8":
                        try
                        {
                            Console.Write("Enter Enrollment ID: ");
                            int enrollmentId = Convert.ToInt32(Console.ReadLine());

                            enrollmentService.GetStudentByEnrollmentId(enrollmentId);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format. Please enter a valid enrollment ID.");
                        }
                        break;


                    case "9":
                        try
                        {
                            Console.Write("Enter Enrollment ID: ");
                            int enrollmentId = Convert.ToInt32(Console.ReadLine());

                            enrollmentService.GetCourseByEnrollmentId(enrollmentId);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format. Please enter a valid enrollment ID.");
                        }
                        break;


                    case "10":
                        try
                        {
                            Console.Write("Enter Student ID: ");
                            int studentid = int.Parse(Console.ReadLine());


                            paymentService.DisplayPaymentsForStudent(studentid);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format. Please enter a valid student ID.");
                        }
                        break;


                    case "11":
                        try
                        {
                            Console.Write("Enter Payment ID: ");
                            int paymentId = int.Parse(Console.ReadLine());


                            paymentService.DisplayPaymentAmount(paymentId);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format. Please enter a valid payment ID.");
                        }
                        break;


                    case "12":
                        try
                        {
                            Console.Write("Enter Payment ID: ");
                            int paymentId = int.Parse(Console.ReadLine());

                            paymentService.DisplayPaymentDate(paymentId);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format. Please enter a valid payment ID.");
                        }
                        break;


                    case "13":
                        teacherService.DisplayTeacherInfo();
                        break;


                    case "14":
                        try
                        {
                            Console.Write("Enter Teacher ID: ");
                            int TeacherId = int.Parse(Console.ReadLine());

                            teacherService.GetAssignedCoursesforTeacher(TeacherId);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format. Please enter a valid Teacher ID.");
                        }
                        break;



                    case "15":
                        try
                        {
                            Console.Write("Enter Teacher ID: ");
                            int teacherid = int.Parse(Console.ReadLine());

                            Console.Write("Enter First Name (leave blank to skip): ");
                            string firstName = Console.ReadLine();

                            Console.Write("Enter Last Name (leave blank to skip): ");
                            string lastName = Console.ReadLine();

                            Console.Write("Enter Email (leave blank to skip): ");
                            string email = Console.ReadLine();

                            teacherService.UpdateTeacherInfo(
                                teacherid,
                                string.IsNullOrWhiteSpace(firstName) ? null : firstName,
                                string.IsNullOrWhiteSpace(lastName) ? null : lastName,
                                string.IsNullOrWhiteSpace(email) ? null : email
                            );
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter valid details.");
                        }
                        break;



                    case "16":
                        try
                        {
                            Console.Write("Enter Course ID: ");
                            int courseid = int.Parse(Console.ReadLine());

                            Console.Write("Enter Teacher ID: ");
                            int teacherID = int.Parse(Console.ReadLine());

                            courseService.AssignTeacher(courseid, teacherID);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter valid Course ID and Teacher ID.");
                        }
                        break;


                    case "17":
                        Console.WriteLine("Enter Course ID to update:");
                        int courseID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter new Course Name:");
                        string courseName = Console.ReadLine();
                        Console.WriteLine("Enter new Credits:");
                        int credits = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Teacher ID:");
                        int teacherId = int.Parse(Console.ReadLine());

                        courseService.UpdateCourseInfo(courseID, courseName, credits, teacherId);
                        break;




                    case "18":

                        Console.WriteLine("Enter Course ID to display:");
                        int Courseid = int.Parse(Console.ReadLine());

                        courseService.DisplayCourseInfo(Courseid);
                        break;



                    case "19":
                        Console.Write("Enter Course ID to get assigned teacher: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        courseService.GetTeacherForCourse(id);
                        break;



                    case "20":
                        Console.Write("Enter Course ID to view statistics: ");
                        int coursEId = Convert.ToInt32(Console.ReadLine());
                        courseService.CalculateCourseStatistics(coursEId);
                        break;
                    case "21":
                        // Exit
                        continueRunning = false;
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("\n\nExiting the program. Goodbye!\n\n");
                        Console.ResetColor();
                        break;


                    default:
                         Console.WriteLine("Invalid option. Please select a valid choice (1-2).");
    break;
   
}
            }
        }
    }
}

