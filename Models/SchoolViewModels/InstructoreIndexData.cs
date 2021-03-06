using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DfwUniversity.Models.SchoolViewModels
{
    //The instructors page shows data from three different tables. A view model is needed that 
    //includes three properties representing the three tables.
    public class InstructorIndexData
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}