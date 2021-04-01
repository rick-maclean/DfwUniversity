using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DfwUniversity.Data;
using DfwUniversity.Models;

namespace DfwUniversity.Pages.Departments
{
    public class CreateModel : InstructorNamePageModel
    {
        private readonly DfwUniversity.Data.SchoolContext _context;

        public CreateModel(DfwUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "EmailAddress");
            //ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "ID");
            PopulateInstructoresDropDownList(_context); // Now the Administrator dropdownList will display Names vs IDs
            return Page();
        }

        [BindProperty]
        public Department Department { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            // if (!ModelState.IsValid)
            // {
            //     return Page();
            // }

            // _context.Departments.Add(Department);
            // await _context.SaveChangesAsync();

            // return RedirectToPage("./Index");


            // Changes made so that when creating a new course it will utilize InstructorSL when creating it.
            var newDepartment = new Department();

            if (await TryUpdateModelAsync<Department>(
                                newDepartment,
                                "department", //Prefix for the form value.
                                // Note this is passing in the form data to set the values for this new Course
                                s => s.Name, 
                                s => s.Budget, 
                                s => s.StartDate, 
                                s => s.InstructorID)
                )
            {
                _context.Departments.Add(newDepartment);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index"); //If all succeeds in creating it then return to the Index view
            }

            // Select InstructorID if TryUpdateModelAsync fails.
            PopulateInstructoresDropDownList(_context, newDepartment.InstructorID);
            return Page();


        }
    }
}
