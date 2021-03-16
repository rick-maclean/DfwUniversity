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
        // public IndexModel(DfwUniversity.Data.SchoolContext context, IOptions<MvcOptions> mvcOptions)
        // {
        //     _context = context;
        //     _mvcOptions = mvcOptions.Value;
        // }

        public IList<Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Students.ToListAsync();
            // Student = await _context.Students.Take(
            // _mvcOptions.MaxModelBindingCollectionSize).ToListAsync();
        }
    }
}
