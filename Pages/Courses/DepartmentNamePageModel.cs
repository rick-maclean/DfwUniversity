using DfwUniversity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DfwUniversity.Pages.Courses
{
    // The scaffolded code for the Course Create and Edit pages has a Department drop-down list that shows Department ID 
    // (an integer). The drop-down should show the Department name, so both of these pages need a list of department names. 
    public class DepartmentNamePageModel : PageModel
    {
        public SelectList DepartmentNameSL {get; set;}

        public void PopulateDepartmentsDropDownList(SchoolContext _context, object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Departments
                                   orderby d.Name // Sort by name.
                                   select d;

            // here is the URL explaining this function
            // https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc.selectlist.-ctor?view=aspnet-mvc-5.2#System_Web_Mvc_SelectList__ctor_System_Collections_IEnumerable_System_String_System_String_System_Object_
            DepartmentNameSL = new SelectList(departmentsQuery.AsNoTracking(), "DepartmentID", "Name", selectedDepartment);
        }

    }
}