using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DfwUniversity.Data;
using DfwUniversity.Models;
using DfwUniversity.Models.SchoolViewModels;

namespace DfwUniversity.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly DfwUniversity.Data.SchoolContext _context;

        public IndexModel(DfwUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public int InstructorID {get; set;}
        public int CourseID {get;set;}
        public InstructorIndexData InstructorData {get; set;}

        //public IList<Instructor> Instructor { get;set; }

        // public async Task OnGetAsync()
        // {
        //     Instructor = await _context.Instructors.ToListAsync();
        // }

        //The instructors page shows data from three different tables. A view model is needed that 
        //includes three properties representing the three tables. Therefore it is using InstructorIndexData
        //
        //The OnGetAsync method accepts optional route data for the ID of the selected instructor AND courseID
        public async Task OnGetAsync(int? instructorID, int? courseID)
        {
            InstructorData = new InstructorIndexData();

            // The code specifies eager loading for the following navigation properties
            // Notice the repetition of Include and ThenInclude methods for CourseAssignments and Course. This 
            // repetition is necessary to specify eager loading for two navigation properties of the Course entity.
            InstructorData.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)                 
                .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Department)
                .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Enrollments)
                            .ThenInclude(i => i.Student)
                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync();

            // The selected instructor is retrieved from the list of instructors in the view model. The 
            // view model's Courses property is loaded with the Course entities from that instructor's 
            // CourseAssignments navigation property.
            if (instructorID != null)
            {
                InstructorID = instructorID.Value;

                // The Where method returns a collection. But in this case, the filter will select a 
                // single entity, so the Single method is called to convert the collection into a 
                // single Instructor entity.
                Instructor instructor = InstructorData.Instructors
                    .Where(i => i.ID == instructorID.Value).Single();

                // The Instructor entity provides access to the CourseAssignments property. 
                // CourseAssignments provides access to the related Course entities.
                InstructorData.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseID != null)
            {
                CourseID = courseID.Value;

                // Select the particular course from the InstructorData view model
                var selectedCourse = InstructorData.Courses
                    .Where(x => x.CourseID == courseID).Single();

                // Select the Student Enrollments for this course which will then be displayed in the UI
                InstructorData.Enrollments = selectedCourse.Enrollments;
            }
        }
    }
}
