using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CherepkoLib.Data;
using CherepkoLib.Entities;

namespace Cherepko.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RodsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Rods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rod>>> GetRods(int? group)
        {
            return await _context.Rods.Where(d => !group.HasValue
                    || d.RodGroupId.Equals(group.Value))
                    ?.ToListAsync();
        }

        // GET: api/Rods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rod>> GetRod(int id)
        {
            var rod = await _context.Rods.FindAsync(id);

            if (rod == null)
            {
                return NotFound();
            }

            return rod;
        }

        // PUT: api/Rods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRod(int id, Rod rod)
        {
            if (id != rod.RodId)
            {
                return BadRequest();
            }

            _context.Entry(rod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rod>> PostRod(Rod rod)
        {
            _context.Rods.Add(rod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRod", new { id = rod.RodId }, rod);
        }

        // DELETE: api/Rods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRod(int id)
        {
            var rod = await _context.Rods.FindAsync(id);
            if (rod == null)
            {
                return NotFound();
            }

            _context.Rods.Remove(rod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RodExists(int id)
        {
            return _context.Rods.Any(e => e.RodId == id);
        }
    }
}
