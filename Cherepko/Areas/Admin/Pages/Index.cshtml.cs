using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CherepkoLib.Data;
using CherepkoLib.Entities;

namespace Cherepko.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CherepkoLib.Data.ApplicationDbContext _context;

        public IndexModel(CherepkoLib.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Rod> Rod { get;set; }

        public async Task OnGetAsync()
        {
            Rod = await _context.Rods
                .Include(r => r.Group).ToListAsync();
        }
    }
}
