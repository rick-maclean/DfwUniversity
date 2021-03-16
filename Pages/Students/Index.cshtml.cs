using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DfwUniversity.Data;
using DfwUniversity.Models;
using Microsoft.Extensions.Options;

namespace DfwUniversity.Pages_Students
{
    public class IndexModel : PageModel
    {
        private readonly DfwUniversity.Data.SchoolContext _context;
        // private readonly MvcOptions _mvcOptions;

        public IndexModel(DfwUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Student> Students { get;set; }

        public async Task OnGetAsync()
        {
            Students = await _context.Students.ToListAsync();
        }
    }
}
