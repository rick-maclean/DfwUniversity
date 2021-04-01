using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DfwUniversity.Data;
using DfwUniversity.Models;

namespace DfwUniversity.Pages.Departments
{
    public class EditModel : InstructorNamePageModel
    {
        private readonly DfwUniversity.Data.SchoolContext _context;

        public EditModel(DfwUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Department Department { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the department matching the id parameter
            // and also include the Adminitrator (a navigation property)
            Department = await _context.Departments
                .Include(d => d.Administrator).FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (Department == null)
            {
                return NotFound();
            }

            // ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "ID");
            // Select current InstructorID.
            PopulateInstructoresDropDownList(_context, Department.InstructorID);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsyncOld()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(Department.DepartmentID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentToUpdate = await _context.Departments.FindAsync(id);

            if (departmentToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Department>(
                 departmentToUpdate,
                 "department",   // Prefix for form value.
                   d => d.Name, d => d.InstructorID, d => d.StartDate, d => d.Budget))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select InstructorID if TryUpdateModelAsync fails.
            // Select current InstructorID.
            PopulateInstructoresDropDownList(_context, Department.InstructorID);
            return Page();
        }       


        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentID == id);
        }
    }
}
