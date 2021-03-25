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

namespace DfwUniversity.Pages.Instructors
{
    // Another relationship the edit page has to handle is the one-to-zero-or-one relationship that the Instructor 
    // entity has with the OfficeAssignment entity. The instructor edit code must handle the following scenarios:
            // If the user clears the office assignment, delete the OfficeAssignment entity.
            // If the user enters an office assignment and it was empty, create a new OfficeAssignment entity.
            // If the user changes the office assignment, update the OfficeAssignment entity.
    // base this now off the InstructorCoursesPageModel vs the default PageModel
    public class EditModel : InstructorCoursesPageModel
    {
        private readonly DfwUniversity.Data.SchoolContext _context;

        public EditModel(DfwUniversity.Data.SchoolContext context)
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

            // Instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.ID == id);
            Instructor = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments).ThenInclude(i => i.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Instructor == null)
            {
                return NotFound();
            }
            // Populate the List so that it can be displayed in the UI.
            // Call PopulateAssignedCourseData in OnGetAsync to provide information for the checkboxes 
            // using the AssignedCourseData view model class.
            PopulateAssignedCourseData(_context, Instructor);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        // public async Task<IActionResult> OnPostAsync()
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return Page();
        //     }

        //     _context.Attach(Instructor).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!InstructorExists(Instructor.ID))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return RedirectToPage("./Index");
        // }
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the current Instructor entity from the database using eager loading for the OfficeAssignment, 
            // CourseAssignment, and CourseAssignment.Course navigation properties.
            var instructorToUpdate = await _context.Instructors
                                        .Include(i => i.OfficeAssignment)
                                        .Include(i => i.CourseAssignments)
                                            .ThenInclude(i => i.Course)
                                        .FirstOrDefaultAsync(s => s.ID == id);

            if (instructorToUpdate == null)
            {
                return NotFound();
            }

            // Update the retrieved Instructor entity with values from the model binder. TryUpdateModel prevents overposting.
            if (    await TryUpdateModelAsync<Instructor>
                    (
                        instructorToUpdate, "Instructor",
                        i => i.FirstMidName, i => i.LastName, i => i.HireDate, i => i.OfficeAssignment, id => id.EmailAddress
                    )
               )
            {
                // If the office location is blank, set Instructor.OfficeAssignment to null. When Instructor.OfficeAssignment 
                // is null, the related row in the OfficeAssignment table is deleted.
                if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment?.Location))
                {
                    instructorToUpdate.OfficeAssignment = null;
                }
                // Call UpdateInstructorCourses in OnPostAsync to apply information from the checkboxes to the Instructor 
                // entity being edited.
                UpdateInstructorCourses(_context, selectedCourses, instructorToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Call PopulateAssignedCourseData and UpdateInstructorCourses in OnPostAsync if TryUpdateModel fails. These method 
            // calls restore the assigned course data entered on the page when it is redisplayed with an error message.
            UpdateInstructorCourses(_context, selectedCourses, instructorToUpdate);
            PopulateAssignedCourseData(_context, instructorToUpdate);
            return Page();
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.ID == id);
        }
    }
}
