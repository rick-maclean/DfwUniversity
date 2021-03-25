using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DfwUniversity.Data;
using DfwUniversity.Models;

namespace DfwUniversity.Pages.Instructors
{
    public class DeleteModel : PageModel
    {
        private readonly DfwUniversity.Data.SchoolContext _context;

        public DeleteModel(DfwUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Instructor Instructor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.ID == id);

            if (Instructor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Instructor = await _context.Instructors.FindAsync(id);

            // if (Instructor != null)
            // {
            //     _context.Instructors.Remove(Instructor);
            //     await _context.SaveChangesAsync();
            // }

            // Uses eager loading for the CourseAssignments navigation property. CourseAssignments must be included or 
            // they aren't deleted when the instructor is deleted. To avoid needing to read them, configure cascade 
            // delete in the database.
            Instructor instructor = await _context.Instructors
                .Include(i => i.CourseAssignments)
                .SingleAsync(i => i.ID == id);

            if (instructor == null)
            {
                return RedirectToPage("./Index");
            }

            // If the instructor to be deleted is assigned as administrator of any departments, removes the instructor 
            // assignment from those departments.
            var departments = await _context.Departments
                .Where(d => d.InstructorID == id)
                .ToListAsync();
            departments.ForEach(d => d.InstructorID = null);

            _context.Instructors.Remove(instructor);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
