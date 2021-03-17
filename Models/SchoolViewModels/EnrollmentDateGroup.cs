using System;
using System.ComponentModel.DataAnnotations;

namespace DfwUniversity.Models.SchoolViewModels
{
    public class EnrollmentDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate {get; set;}

        public int StudentCountPerDate {get; set;}
    }
}