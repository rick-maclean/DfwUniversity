
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DfwUniversity.Models.SchoolViewModels
{
#region snippet
    public class CourseViewModel
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        [Display(Name = "DepartmentVM")]
        public string DepartmentName { get; set; }
    }
#endregion
}