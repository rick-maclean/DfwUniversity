using DfwUniversity.Data;
using DfwUniversity.Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DfwUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            // context.Database.EnsureCreated(); // remove this now that migrations are being used

            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
            {
                // new Student{FirstMidName="Carson", LastName="Alexander", EmailAddress="caleander@swbts.edu", EnrollmentDate=DateTime.Parse("2021-09-28")},
                // new Student{FirstMidName="Meredith",LastName="Alonso", EmailAddress="cslonso@swbts.edu", EnrollmentDate=DateTime.Parse("2017-09-01")},
                // new Student{FirstMidName="Arturo",LastName="Anand",EmailAddress="aanand@swbts.edu", EnrollmentDate=DateTime.Parse("2018-09-01")},
                // new Student{FirstMidName="Gytis",LastName="Barzdukas",EmailAddress="cbarzdukas@swbts.edu", EnrollmentDate=DateTime.Parse("2017-09-01")},
                // new Student{FirstMidName="Yan",LastName="Li",EmailAddress="yli@swbts.edu", EnrollmentDate=DateTime.Parse("2017-09-01")},
                // new Student{FirstMidName="Peggy",LastName="Justice",EmailAddress="pjustice@swbts.edu", EnrollmentDate=DateTime.Parse("2016-09-01")},
                // new Student{FirstMidName="Laura",LastName="Norman",EmailAddress="lnorman@swbts.edu", EnrollmentDate=DateTime.Parse("2018-09-01")},
                // new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2019-09-01")}

                new Student { FirstMidName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2016-09-01"), EmailAddress="caleander@swbts.edu" },
                new Student { FirstMidName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2018-09-01"), EmailAddress="cslonso@swbts.edu" },
                new Student { FirstMidName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2019-09-01"), EmailAddress="aanand@swbts.edu" },
                new Student { FirstMidName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2018-09-01"), EmailAddress="cbarzdukas@swbts.edu" },
                new Student { FirstMidName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2018-09-01"),EmailAddress="yli@swbts.edu" },
                new Student { FirstMidName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2017-09-01"),EmailAddress="pjustice@swbts.edu" },
                new Student { FirstMidName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2019-09-01"),EmailAddress="lnorman@swbts.edu" },
                new Student { FirstMidName = "Richard",    LastName = "MacLean",
                    EnrollmentDate = DateTime.Parse("2021-09-01"),EmailAddress="rmaclean@swbts.edu" },
                    new Student { FirstMidName = "Sam",    LastName = "Hurley",
                    EnrollmentDate = DateTime.Parse("2021-09-01"),EmailAddress="shurley@swbts.edu" },
                 new Student { FirstMidName = "Luke",    LastName = "MacLean",
                    EnrollmentDate = DateTime.Parse("2021-09-01"),EmailAddress="lmaclean06@gmail.com" },
                new Student { FirstMidName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2011-09-01"),EmailAddress="onion@gmail.com" }
            };
            context.Students.AddRange(students);
            context.SaveChanges();

            var instructors = new Instructor[]
            {
                new Instructor { FirstMidName = "Kim",     LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11"), EmailAddress="lm2@gmail.com" },
                new Instructor { FirstMidName = "Fadi",    LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06"), EmailAddress="lm3@gmail.com" },
                new Instructor { FirstMidName = "Roger",   LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01"), EmailAddress="lm4@gmail.com" },
                new Instructor { FirstMidName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15"), EmailAddress="lm5@gmail.com" },
                new Instructor { FirstMidName = "Roger",   LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12"), EmailAddress="lm6@gmail.com" }
            };
            context.Instructors.AddRange(instructors);
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Abercrombie").ID },
                new Department { Name = "Mathematics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Fakhouri").ID },
                new Department { Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Harui").ID },
                new Department { Name = "Economics",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID  = instructors.Single( i => i.LastName == "Kapoor").ID }
            };
            context.Departments.AddRange(departments);
            context.SaveChanges();


            var courses = new Course[]
            {
                new Course {CourseID = 1050, Title = "Chemistry",      Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID
                },
                new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID
                },
                new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID
                },
                new Course {CourseID = 1045, Title = "Calculus",       Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
                },
                new Course {CourseID = 3141, Title = "Trigonometry",   Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
                },
                new Course {CourseID = 2021, Title = "Composition",    Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
                },
                new Course {CourseID = 2042, Title = "Literature",     Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
                },
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();

            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Fakhouri").ID,
                    Location = "Smith 17" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Harui").ID,
                    Location = "Gowan 27" },
                new OfficeAssignment {
                    InstructorID = instructors.Single( i => i.LastName == "Kapoor").ID,
                    Location = "Thompson 304" },
            };
            context.OfficeAssignments.AddRange(officeAssignments);
            context.SaveChanges();

var courseInstructors = new CourseAssignmentInstructor[]
            {
                new CourseAssignmentInstructor {
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID
                    },
                new CourseAssignmentInstructor {
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Harui").ID
                    },
                new CourseAssignmentInstructor {
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
                    },
                new CourseAssignmentInstructor {
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
                    },
                new CourseAssignmentInstructor {
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID
                    },
                new CourseAssignmentInstructor {
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Harui").ID
                    },
                new CourseAssignmentInstructor {
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                    },
                new CourseAssignmentInstructor {
                    CourseID = courses.Single(c => c.Title == "Literature" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                    },
            };
            context.CourseAssignments.AddRange(courseInstructors);
            context.SaveChanges();

            // var enrollments = new Enrollment[]
            // {
            //     new Enrollment{StudentID=1, CourseID=1050,Grade=Grade.A},
            //     new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            //     new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            //     new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            //     new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            //     new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            //     new Enrollment{StudentID=3,CourseID=1050},
            //     new Enrollment{StudentID=4,CourseID=1050},
            //     new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            //     new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            //     new Enrollment{StudentID=6,CourseID=1045},
            //     new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            // };
            // context.Enrollments.AddRange(enrollments );
            // context.SaveChanges();
            var enrollments = new Enrollment[]
            {
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    Grade = Grade.A
                },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    Grade = Grade.C
                },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    Grade = Grade.B
                },
                new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    Grade = Grade.B
                },
                new Enrollment {
                        StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    Grade = Grade.B
                },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    Grade = Grade.B
                },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                    Grade = Grade.B
                },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                    Grade = Grade.B
                 },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Li").ID,
                    CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                    Grade = Grade.B
                },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Justice").ID,
                    CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                    Grade = Grade.B
                }
            };            
            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                            s.Student.ID == e.StudentID &&
                            s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();

        }
    }
}