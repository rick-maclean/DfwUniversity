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
    public class CreateModel : PageModel
    {
        private readonly DfwUniversity.Data.SchoolContext _context;

        public CreateModel(DfwUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "EmailAddress");
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Department Department { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Departments.Add(Department);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
