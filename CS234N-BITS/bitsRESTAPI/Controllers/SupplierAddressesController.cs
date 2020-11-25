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
    public class SupplierAddressesController : ControllerBase
    {
        private readonly bitsContext _context;

        public SupplierAddressesController(bitsContext context)
        {
            _context = context;
        }

        // GET: api/SupplierAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierAddress>>> GetSupplierAddress()
        {
            return await _context.SupplierAddress.ToListAsync();
        }

        // GET: api/SupplierAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierAddress>> GetSupplierAddress(int id)
        {
            var supplierAddress = await _context.SupplierAddress.FindAsync(id);

            if (supplierAddress == null)
            {
                return NotFound();
            }

            return supplierAddress;
        }

        // PUT: api/SupplierAddresses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplierAddress(int id, SupplierAddress supplierAddress)
        {
            if (id != supplierAddress.SupplierId)
            {
                return BadRequest();
            }

            _context.Entry(supplierAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierAddressExists(id))
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

        // POST: api/SupplierAddresses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SupplierAddress>> PostSupplierAddress(SupplierAddress supplierAddress)
        {
            _context.SupplierAddress.Add(supplierAddress);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SupplierAddressExists(supplierAddress.SupplierId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSupplierAddress", new { id = supplierAddress.SupplierId }, supplierAddress);
        }

        // DELETE: api/SupplierAddresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SupplierAddress>> DeleteSupplierAddress(int id)
        {
            var supplierAddress = await _context.SupplierAddress.FindAsync(id);
            if (supplierAddress == null)
            {
                return NotFound();
            }

            _context.SupplierAddress.Remove(supplierAddress);
            await _context.SaveChangesAsync();

            return supplierAddress;
        }

        private bool SupplierAddressExists(int id)
        {
            return _context.SupplierAddress.Any(e => e.SupplierId == id);
        }
    }
}
