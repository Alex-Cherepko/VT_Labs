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
    public class DeleteModel : PageModel
    {
        private readonly CherepkoLib.Data.ApplicationDbContext _context;

        public DeleteModel(CherepkoLib.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Rod Rod { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Rod = await _context.Rods
                .Include(r => r.Group).FirstOrDefaultAsync(m => m.RodId == id);

            if (Rod == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Rod = await _context.Rods.FindAsync(id);

            if (Rod != null)
            {
                _context.Rods.Remove(Rod);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
