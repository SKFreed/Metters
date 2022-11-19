#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSQLite;

namespace WebApiSQLite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MettersController : ControllerBase
    {
        private readonly MettersContext _context;
        public MettersController(MettersContext context)
        {
            _context = context;
        }

        // GET: api/Metters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Metter>>> GetMetters()
        {
            return await _context.Metters.ToListAsync();
        }
        // GET: api/Metters/sort
        [HttpGet("sort")]
        public ActionResult GetMetterIdAddress()
        {
            var sel = _context.Metters.Select(b => new Metter
            {
                Id = b.Id,
                Address = b.Address,
            }).OrderBy(p => p.Address).Select(p => new
            {
                Address = p.Address,
                Id = p.Id
            }).ToList();
            return new OkObjectResult(sel);
;        }
        // GET: api/Metters/anyAddress/address
        [HttpGet("anyAddress{address}")]
        public async Task<ActionResult<Metter>> GetMetter(string address)
        {
            var metter = await _context.Metters.Where(p => p.Address == address).ToListAsync();

            if (metter == null)
            {
                return NotFound();
            }

            return new OkObjectResult(metter);
        }
        // PUT: api/Metters/disableStatus/{id}
        [HttpPut("disableStatus/{id}")]
        public async Task<ActionResult<Metter>> DisableStatus(long id)
        {
            Metter? metter = await _context.Metters.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(metter == null)
            {
                return NoContent();
            }
            if (id != metter.Id)
            {
                return BadRequest();
            }
            metter.Status = 0;
            _context.Entry(metter).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return metter;
        }

        // GET: api/Metters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Metter>> GetMetter(long id)
        {
            var metter = await _context.Metters.FindAsync(id);

            if (metter == null)
            {
                return NotFound();
            }

            return metter;
        }

        // PUT: api/Metters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetter(long id, Metter metter)
        {
            if (id != metter.Id)
            {
                return BadRequest();
            }

            _context.Entry(metter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetterExists(id))
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

        // POST: api/Metters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Metter>> PostMetter(Metter metter)
        {
            _context.Metters.Add(metter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMetter", new { id = metter.Id }, metter);
        }

        // DELETE: api/Metters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetter(long id)
        {
            var metter = await _context.Metters.FindAsync(id);
            if (metter == null)
            {
                return NotFound();
            }

            _context.Metters.Remove(metter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MetterExists(long id)
        {
            return _context.Metters.Any(e => e.Id == id);
        }
    }
}
