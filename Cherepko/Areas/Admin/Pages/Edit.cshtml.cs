using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CherepkoLib.Data;
using CherepkoLib.Entities;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Cherepko.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment environment;

        public EditModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            environment = env;
        }

        [BindProperty]
        public Rod Rod { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }

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
           ViewData["RodGroupId"] = new SelectList(_context.RodGroups, "RodGroupId", "GroupName");
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

            _context.Attach(Rod).State = EntityState.Modified;

            try
            {
                
                if (Image != null)
                {
                    var fileName = $"{Rod.RodId}" + Path.GetExtension(Image.FileName);
                    Rod.Image = fileName;
                    var path = Path.Combine(environment.WebRootPath, "Images", fileName);
                    using (var fStream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(fStream);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RodExists(Rod.RodId))
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

        private bool RodExists(int id)
        {
            return _context.Rods.Any(e => e.RodId == id);
        }
    }
}
