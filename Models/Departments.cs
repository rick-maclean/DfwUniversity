using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DfwUniversity.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        // The Budget column is defined using the SQL Server 'money' type in the database:
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        // Administrator below is the navigation property associated with this FK
        // Note this is a FK for the dept Administrator  who we assume is also an Instructor
        // The question mark (?) specifies the property is nullable (therefore might not have an Administrator)
        public int? InstructorID { get; set; }

        // The navigation property is named Administrator but holds an Instructor entity
        public Instructor Administrator { get; set; }
        
        // A department may have many courses, so there's a Courses navigation property
        public ICollection<Course> Courses { get; set; }
    }
}