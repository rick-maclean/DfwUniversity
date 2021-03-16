using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DfwUniversity.Data;
using DfwUniversity.Models;

namespace DfwUniversity.Pages_Students
{
    public class DeleteModel : PageModel
    {
        private readonly DfwUniversity.Data.SchoolContext _context;

        public DeleteModel(DfwUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }
        public string ErrorMessage { get; set; } //added for DELETE error

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false) // error handling added
        // public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students
                .AsNoTracking() //added as part of error handling ??? why
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())  // error handling added
            {
                ErrorMessage = "Delete failed. Try again";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Commented out and added error handling below
            // Student = await _context.Students.FindAsync(id);

            // if (Student != null)
            // {
            //     _context.Students.Remove(Student);
            //     await _context.SaveChangesAsync();
            // }

            // return RedirectToPage("./Index");

            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("./Delete",
                                    new { id, saveChangesError = true });
            }
        }
    }
}
