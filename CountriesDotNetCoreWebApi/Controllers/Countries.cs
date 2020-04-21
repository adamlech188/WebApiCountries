using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CountriesLibrary.Models;

namespace CountriesDotNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Countries : ControllerBase
    {
        private readonly CountriesDBContext _context;

        public Countries(CountriesDBContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCountries>>> GetTblCountries()
        {
            return await _context.TblCountries.ToListAsync();
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCountries>> GetTblCountries(int id)
        {
            var tblCountries = await _context.TblCountries.FindAsync(id);

            if (tblCountries == null)
            {
                return NotFound();
            }

            return tblCountries;
        }

        [HttpGet("byname")]
        public async Task<ActionResult<IEnumerable<TblCountries>>> GetTblCountriesByName([FromQuery]string name)
        {
            var tblCountries = await _context.TblCountries.Where(c => c.Countryname.StartsWith(name)).ToListAsync();

            if (tblCountries == null)
            {
                return NotFound();
            }

            return tblCountries;
        }
        // PUT: api/Countries/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCountries(int id, TblCountries tblCountries)
        {
            if (id != tblCountries.Countryid)
            {
                return BadRequest();
            }

            _context.Entry(tblCountries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCountriesExists(id))
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

        // POST: api/Countries
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TblCountries>> PostTblCountries(TblCountries tblCountries)
        {
            _context.TblCountries.Add(tblCountries);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblCountries", new { id = tblCountries.Countryid }, tblCountries);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblCountries>> DeleteTblCountries(int id)
        {
            var tblCountries = await _context.TblCountries.FindAsync(id);
            if (tblCountries == null)
            {
                return NotFound();
            }

            _context.TblCountries.Remove(tblCountries);
            await _context.SaveChangesAsync();

            return tblCountries;
        }

        private bool TblCountriesExists(int id)
        {
            return _context.TblCountries.Any(e => e.Countryid == id);
        }
    }
}
