using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DfwUniversity.Models
{
    public enum Grade
    {
        A, B, C, D, F 
    }

    // There's a many-to-many relationship between the Student and Course entities. The Enrollment 
    // entity functions as a many-to-many join table with payload in the database. "With payload" means 
    // that the Enrollment table contains additional data besides FKs for the joined tables (in this 
    // case, the PK and Grade).
    //
    // If the Enrollment table didn't include grade information, it would only need to contain the 
    // two FKs (CourseID and StudentID). A many-to-many join table without payload is sometimes 
    // called a pure join table (PJT).
    public class Enrollment
    {
        public int EnrollmentID {get; set;}

        // An enrollment record is for one course, so there's a CourseID FK property and a Course navigation property
        public int CourseID {get; set;}

        // An enrollment record is for one student, so there's a StudentID FK property and a Student navigation property
        public int StudentID {get; set;}

        // Display text if there is no grade
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade {get; set;}

        public Course Course {get; set;}
        public Student Student {get; set;}
    }
}