using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreWebAPI.Models;
using StoreWebAPI.DTOs;
using AutoMapper;


namespace StoreWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        private readonly StoreDBContext _context;

        public CustomersController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Customers/Page/3/30
        //        [HttpGet("Page/{pag}/{tam}")]
        public IEnumerable<CustomerDTO> GetCustomer([FromRoute] int pag, [FromRoute] int tam)
        {
            var model = _context.Customer.Skip(tam * pag - 1).Take(tam).OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
            var dto = Mapper.Map<IEnumerable<CustomerDTO>>(model);
            return dto;
        }


        //[HttpGet("Page?pag={pag}&tam={tam}")]
        //FromQuery?

        // GET: api/Customers/Page/3/30
        [HttpGet("")]
        public IEnumerable<CustomerDTO> GetCustomer()
        {
            var model = _context.Customer.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
            var dto = Mapper.Map<IEnumerable<CustomerDTO>>(model);
            return dto;
        }

        /*
        [HttpGet("{first}/{last}/{username}")]
        public async Task<IActionResult> GetCustomer([FromRoute] string first, [FromRoute] string last, [FromRoute] string username)
        {
        }
        */

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CustomerDTO>(customer));
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(Mapper.Map<Customer>(customer)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var map = Mapper.Map<Customer>(customer);

            _context.Customer.Add(map);
            await _context.SaveChangesAsync();
            customer.Id = map.Id;

            return CreatedAtAction("GetCustomer", new { id = map.Id }, customer);
        }

        //POST: api/Customers/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] dynamic credentials)
        {
            var username = (string)credentials["username"];
            var password = (string)credentials["password"];

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.UserName == username && m.Password == password);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CustomerDTO>(customer));
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customer.SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(Mapper.Map<CustomerDTO>(customer));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}