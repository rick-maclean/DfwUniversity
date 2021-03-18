using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DfwUniversity.Models
{
    public class OfficeAssignment
    {
        [Key]
        public int InstructorID {get; set;}

        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location {get; set;}

        // I am not sure of this but I think maybe the Navigation property is the same name as a int KeyID
        // and somehow EF knows what to do with this ??? But what does it do with it???
        public Instructor Instructor {get; set;}
    }
}