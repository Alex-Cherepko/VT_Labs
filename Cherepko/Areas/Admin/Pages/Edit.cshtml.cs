using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _90331_ElenaPlotnikova.DAL.Data;
using _90331_ElenaPlotnikova.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace _90331_ElenaPlotnikova.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly _90331_ElenaPlotnikova.DAL.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(_90331_ElenaPlotnikova.DAL.Data.ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        [BindProperty]
        public Food Food { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
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
           ViewData["FoodGroupId"] = new SelectList(_context.FoodGroups, "FoodGroupId", "GroupName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Food).State = EntityState.Modified;

            try
            {
                // await _context.SaveChangesAsync();
                if (Image != null)
                {
                    var fileName = $"{Food.FoodId}" + Path.GetExtension(Image.FileName);
                    Food.Image = fileName;
                    var path = Path.Combine(_environment.WebRootPath, "Images", fileName);
                    using (var fStream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(fStream);
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(Food.FoodId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.FoodId == id);
        }
    }
}
