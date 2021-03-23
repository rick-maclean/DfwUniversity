namespace DfwUniversity.Models
{
    // The Instructor-to-Courses many-to-many relationship requires a join table, and the 
    // entity for that join table is CourseAssignmentInstructor.
    //
    // It's common to name a join entity EntityName1EntityName2. For example, the Instructor-to-Courses 
    // join table using this pattern would be CourseInstructor. However, we recommend using a name that 
    // describes the relationship.
    //
    // Composite key 
    // The two FKs in CourseAssignmentInstructor (InstructorID and CourseID) together uniquely identify each row of the 
    // CourseAssignmentInstructor table. CourseAssignment doesn't require a dedicated PK. The InstructorID 
    // and CourseID properties function as a composite PK.
    // The composite key ensures that:
        // Multiple rows are allowed for one course.
        // Multiple rows are allowed for one instructor.
        // Multiple rows aren't allowed for the same instructor and course.
    public class CourseAssignmentInstructor
    {
        public int InstructorID {get;set;}
        public Instructor Instructor {get; set;}

        public int CourseID {get; set;}
        public Course Course {get;set;}
    }
    
}