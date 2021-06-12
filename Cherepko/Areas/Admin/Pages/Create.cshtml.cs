using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CherepkoLib.Data;
using CherepkoLib.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Cherepko.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment environment;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            environment = env;
        }

        public IActionResult OnGet()
        {
        ViewData["RodGroupId"] = new SelectList(_context.RodGroups, "RodGroupId", "GroupName");
            return Page();
        }

        [BindProperty]
        public Rod Rod { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Rods.Add(Rod);
            await _context.SaveChangesAsync();

            if (Image != null)
            {
                var fileName = $"{Rod.RodId}" + Path.GetExtension(Image.FileName);
                Rod.Image = fileName;
                var path = Path.Combine(environment.WebRootPath, "Images", fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(fStream);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
