using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DfwUniversity.Data;
using DfwUniversity.Models;

namespace DfwUniversity.Pages.Courses
{
    //Note we are basing this on DepartmentNamePageModel so that we can display department name vs departmentID
    // in the dropdown list.
    public class CreateModel : DepartmentNamePageModel
    {
        private readonly DfwUniversity.Data.SchoolContext _context;

        public CreateModel(DfwUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentID");

            // DepartmentNameSL from the base class is a strongly typed model and will be used by the Razor page.
            PopulateDepartmentsDropDownList(_context); // Now the Department dropdownList will display Names vs IDs
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid)
            // {
            //     return Page();
            // }

            // _context.Courses.Add(Course);
            // await _context.SaveChangesAsync();

            // return RedirectToPage("./Index");

            // Changes made so that when creating a new course it will utilize DepartmentNameSL when creating it.
            var newCourse = new Course();

            if (await TryUpdateModelAsync<Course>(
                                newCourse,
                                "course", //Prefix for the form value.
                                // Note this is passing in the form data to set the values for this new Course
                                s => s.CourseID, 
                                s => s.DepartmentID, 
                                s => s.Title, 
                                s => s.Credits)
                )
            {
                _context.Courses.Add(newCourse);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index"); //If all succeeds in creating it then return to the Index view
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateDepartmentsDropDownList(_context, newCourse.DepartmentID);
            return Page();
        }
    }
}
