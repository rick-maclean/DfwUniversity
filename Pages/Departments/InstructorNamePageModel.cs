using DfwUniversity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DfwUniversity.Pages.Departments
{
    // The scaffolded code for the Departments Create and Edit pages has a Department drop-down list that shows Instructor ID 
    // (an integer). The drop-down should show the Instructor name, so both of these pages need a list of instructor names. 
    public class InstructorNamePageModel : PageModel
    {
        public SelectList InsctructorsSL {get; set;}

        public void PopulateInstructoresDropDownList(SchoolContext _context, object selectedInstructor = null)
        {
            var instructorQuery = from i in _context.Instructors
                                   orderby i.LastName, i.FirstMidName // Sort by name.
                                   select i;

            // here is the URL explaining this function
            // https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.selectlist.-ctor?view=aspnet-mvc-5.2#System_Web_Mvc_SelectList__ctor_System_Collections_IEnumerable_System_String_System_String_System_Object_
            InsctructorsSL = new SelectList(instructorQuery.AsNoTracking(), "ID", "FullName", selectedInstructor);
        }

    }
}