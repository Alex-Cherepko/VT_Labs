using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _90331_ElenaPlotnikova.DAL.Data;
using _90331_ElenaPlotnikova.DAL.Entities;

namespace _90331_ElenaPlotnikova.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly _90331_ElenaPlotnikova.DAL.Data.ApplicationDbContext _context;

        public IndexModel(_90331_ElenaPlotnikova.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Food> Food { get;set; }

        public async Task OnGetAsync()
        {
            Food = await _context.Foods
                .Include(f => f.Group).ToListAsync();
        }
    }
}
