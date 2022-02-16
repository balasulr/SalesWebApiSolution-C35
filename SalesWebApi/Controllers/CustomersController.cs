using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebApi.Models;
// Had Visual Studio create the CustomersController
namespace SalesWebApi.Controllers
{
    [Route("api/[controller]")] // Take Class name of controller then strip off the Controller & put at end of the localhost address
    [ApiController] // What type of Controller using
    public class CustomersController : ControllerBase // Controller can send and receive JSON data
    {
        private readonly AppDbContext _context; // Db contect

        public CustomersController(AppDbContext context) // Constructor that takes one parameter
        {
            _context = context;
        }

        // GET: api/Customers      // Method to return all the customers in the table
        [HttpGet] // HttpGet says that it will only read the database not insert, update or delete. Get is a HTTP Method
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
            // GetCustomers is the method |     // IEnumerable is a collection of customers coming back   // ActionResult return different types
            // of responses that tell caller what can not find if things go wrong. // Task is a class in .Net & have to use as a return type
        { // Above, async shows that is asyncronous processing. Everything over the web is asynchronous.
            return await _context.Customers.ToListAsync(); // Asynchronous and need the await
        }

        // GET: api/Customers/5  <- What the web browser shows after the localhost to return 5th customer  // Similar to read by Primary Key
        [HttpGet("{id}")] // Also, a HTTP Get Method. The URL is different for the 2 HTTP Get methods since have to put in the id for the customer
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(); // Returns on the web to tell user what went wrong instead of null
            }

            return customer;
        }

        // PUT: api/Customers/5 <- Same as the Get method (Read) but the method is different (This has HttpPut whereas Get method has HttpGet)
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")] // Changes or Updates          // The Put is like changing the database.
        public async Task<IActionResult> PutCustomer(int id, Customer customer) // The customer is a instance of a customer
        {
            if (id != customer.Id) // If say going to change customer number 2 but pass up customer number 1 then it will say that not Found
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

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

        // POST: api/Customers   //// Post is like adding a customer to the collection
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost] // Adding customer
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync(); // The Save Changes is done Asyncronously

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer); // Rereads the database for the customer that was added.
        }

        // DELETE: api/Customers/5   //// The Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer); // The remove
            await _context.SaveChangesAsync(); // Save changes asynchronously

            return NoContent(); // Not returning anything. Similar to void
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
