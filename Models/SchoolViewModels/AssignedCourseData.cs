namespace DfwUniversity.Models.SchoolViewModels
{
    //The AssignedCourseData class contains data to create the check boxes for courses assigned to an instructor.
    public class AssignedCourseData
    {
        public int CourseID {get; set;}
        public string Title {get; set;}
        public bool Assigned {get; set;} // Note: I think this will be used to determine if the user "checked"/selected it
    }
}