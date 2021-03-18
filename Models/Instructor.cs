using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DfwUniversity.Models
{
    public class Instructor
    {
        public int ID {get; set;}

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName {get; set;}

        [Required]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstMidName {get; set;}

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string EmailAddress {get; set;}

        [DataType(DataType.Date),Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
        }

        public ICollection<CourseAssignmentInstructor> CourseAssignments {get; set;}
        public OfficeAssignment OfficeAssignment {get; set;}
    }
}