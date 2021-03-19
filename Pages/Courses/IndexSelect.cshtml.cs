using DfwUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DfwUniversity.Pages.Courses
{
    public class IndexSelectModel : PageModel
    {
        private readonly DfwUniversity.Data.SchoolContext _context;

        public IndexSelectModel(DfwUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        #region snippet_RevisedIndexMethod
        public IList<CourseViewModel> CourseVM {get; set;}

        public async Task OnGetAsync()
        {
            CourseVM = await _context.Courses
                    .Select(p => new CourseViewModel
                    {
                        CourseID = p.CourseID,
                        Title = p.Title,
                        Credits = p.Credits,
                        DepartmentName = p.Department.Name
                    }).ToListAsync();
        }
        #endregion
    }
}