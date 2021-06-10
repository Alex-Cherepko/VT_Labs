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
    public class DeleteModel : PageModel
    {
        private readonly _90331_ElenaPlotnikova.DAL.Data.ApplicationDbContext _context;

        public DeleteModel(_90331_ElenaPlotnikova.DAL.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Food Food { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Food = await _context.Foods
                .Include(f => f.Group).FirstOrDefaultAsync(m => m.FoodId == id);

            if (Food == null)
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

            Food = await _context.Foods.FindAsync(id);

            if (Food != null)
            {
                _context.Foods.Remove(Food);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
