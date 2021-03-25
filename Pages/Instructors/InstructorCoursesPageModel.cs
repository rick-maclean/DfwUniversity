using DfwUniversity.Data;
using DfwUniversity.Models;
using DfwUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace DfwUniversity.Pages.Instructors
{
    // The InstructorCoursesPageModel is the base class you will use for the Edit and Create page models. 
    // PopulateAssignedCourseData reads all Course entities to populate AssignedCourseDataList. 
    // For each course, the code sets the CourseID, title, and whether or not the instructor is assigned 
    // to the course. A HashSet is used for efficient lookups.

    public class InstructorCoursesPageModel : PageModel
    {
        public List<AssignedCourseData> AssignedCourseDataList;

        public void PopulateAssignedCourseData(SchoolContext context, Instructor instructor)
        {
            var allCourses = context.Courses;
            var instructorCourses = new HashSet<int>(instructor.CourseAssignments.Select(c => c.CourseID));
            AssignedCourseDataList = new List<AssignedCourseData>();

            foreach (var course in allCourses)
            {
                AssignedCourseDataList.Add
                (
                    new AssignedCourseData {
                        CourseID = course.CourseID,
                        Title = course.Title,
                        Assigned = instructorCourses.Contains(course.CourseID)
                    }
                );
            }
        }

        // Since the Razor page doesn't have a collection of Course entities, the model binder can't automatically update 
        // the CourseAssignments navigation property. Instead of using the model binder to update the CourseAssignments 
        // navigation property, you do that in the new UpdateInstructorCourses method. Therefore you need to exclude the 
        // CourseAssignments property from model binding. This doesn't require any change to the code that 
        // calls TryUpdateModel because you're using the overload with declared properties and CourseAssignments 
        // isn't in the include list.

        public void UpdateInstructorCourses(SchoolContext context, string[] selectedCourses, Instructor instructorToUpdate)
        {
            // If no check boxes were selected, the code in UpdateInstructorCourses initializes the CourseAssignments 
            // navigation property with an empty collection and returns:
            if (selectedCourses == null)
            {
                instructorToUpdate.CourseAssignments = new List<CourseAssignmentInstructor>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                                    (instructorToUpdate.CourseAssignments.Select(c => c.Course.CourseID));
            
            // The code then loops through all courses in the database and checks each course against the ones currently 
            // assigned to the instructor versus the ones that were selected in the page. To facilitate efficient lookups, 
            // the latter two collections are stored in HashSet objects.
            foreach (var course in context.Courses)
            {
                // If the check box for a course was selected but the course isn't in the Instructor.CourseAssignments 
                // navigation property, the course is added to the collection in the navigation property.
                if (selectedCoursesHS.Contains( course.CourseID.ToString() ))
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.CourseAssignments.Add
                        (
                            new CourseAssignmentInstructor
                            {
                                InstructorID = instructorToUpdate.ID,
                                CourseID = course.CourseID
                            }
                        );
                    }
                }
                else
                // If the check box for a course wasn't selected, but the course is in the Instructor.CourseAssignments 
                // navigation property, the course is removed from the navigation property.
                {
                    // This is the situation where the course needs to be removed for this instructor
                    if (instructorCourses.Contains(course.CourseID))
                    {
                        CourseAssignmentInstructor courseToRemove = 
                                instructorToUpdate.CourseAssignments.SingleOrDefault(i => i.CourseID == course.CourseID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}