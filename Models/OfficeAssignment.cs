using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DfwUniversity.Models
{
    public class OfficeAssignment
    {
        // Note: if we did not specify Key here it would probably be a problem since the Key name is 
        // not the same as the class name
        // In this case the database will not generate a key value because the column 
        // is for an identifying relationship
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