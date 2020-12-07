using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bitsEFClasses.Models;

namespace bitsRESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressTypesController : ControllerBase
    {
        private readonly bitsContext _context;

        public AddressTypesController(bitsContext context)
        {
            _context = context;
        }

        // GET: api/AddressTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressType>>> GetAddressType()
        {
            return await _context.AddressType.ToListAsync();
        }

        // GET: api/AddressTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressType>> GetAddressType(int id)
        {
            var addressType = await _context.AddressType.FindAsync(id);

            if (addressType == null)
            {
                return NotFound();
            }

            return addressType;
        }

        // PUT: api/AddressTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressType(int id, AddressType addressType)
        {
            if (id != addressType.AddressTypeId)
            {
                return BadRequest();
            }

            _context.Entry(addressType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressTypeExists(id))
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

        // POST: api/AddressTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AddressType>> PostAddressType(AddressType addressType)
        {
            _context.AddressType.Add(addressType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddressType", new { id = addressType.AddressTypeId }, addressType);
        }

        // DELETE: api/AddressTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AddressType>> DeleteAddressType(int id)
        {
            var addressType = await _context.AddressType.FindAsync(id);
            if (addressType == null)
            {
                return NotFound();
            }

            _context.AddressType.Remove(addressType);
            await _context.SaveChangesAsync();

            return addressType;
        }

        private bool AddressTypeExists(int id)
        {
            return _context.AddressType.Any(e => e.AddressTypeId == id);
        }
    }
}
