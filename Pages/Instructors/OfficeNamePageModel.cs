using DfwUniversity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DfwUniversity.Pages.Instructors
{
    // The scaffolded code for the Instructor Create and Edit pages has an Office drop-down list that shows Office ID 
    // (an integer). The drop-down should show the Office name, so both of these pages need a list of office names. 
    public class OfficeNamePageModel : PageModel
    {
        public SelectList OfficeNameSL {get; set;}

        // Note: this was copied from PopulateDepartmentsDropDownList in Pages.Courses
        // We can see how it was used there and implement it for the Instructors too.
        public void PopulateOfficesDropDownList(SchoolContext _context, object selectedOffice = null)
        {
            var officesQuery = from d in _context.OfficeAssignments
                                   orderby d.Location // Sort by name.
                                   select d;

            // here is the URL explaining this function
            // https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.selectlist.-ctor?view=aspnet-mvc-5.2#System_Web_Mvc_SelectList__ctor_System_Collections_IEnumerable_System_String_System_String_System_Object_
            OfficeNameSL = new SelectList(officesQuery.AsNoTracking(), "InstructorID", "Location", selectedOffice);
        }

    }
}