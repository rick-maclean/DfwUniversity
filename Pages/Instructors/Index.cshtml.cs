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
        public async Task OnGetAsync(int? id, int? courseID)
        {
            InstructorData = new InstructorIndexData();
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

            if (id != null)
            {
                InstructorID = id.Value;
                Instructor instructor = InstructorData.Instructors
                    .Where(i => i.ID == id.Value).Single();
                InstructorData.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseID != null)
            {
                CourseID = courseID.Value;
                var selectedCourse = InstructorData.Courses
                    .Where(x => x.CourseID == courseID).Single();
                InstructorData.Enrollments = selectedCourse.Enrollments;
            }
        }
    }
}
