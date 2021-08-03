using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Carly_Challenge.Models;

namespace Carly_Challenge.Controllers
{
    [Produces("application/json")]
    [Route("api/bookings")]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/bookings
        [HttpGet]
        public IEnumerable<Booking> GettblBookingTest()
        {
            return _context.tblBookingTest;
        }

        // GET: api/bookings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var booking = await _context.tblBookingTest.SingleOrDefaultAsync(m => m.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // PUT: api/bookings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking([FromRoute] int id, [FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != booking.BookingId)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST: api/bookings
        [HttpPost]
        public async Task<IActionResult> PostBooking([FromBody] Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.tblBookingTest.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.BookingId }, booking);
        }

        // DELETE: api/bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var booking = await _context.tblBookingTest.SingleOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.tblBookingTest.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }

        [HttpPost("report")]
        public async Task<IActionResult> Report([FromBody] Interval interval)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get records from database based on interval
            var list = await _context.tblBookingTest
                .Join(_context.tblCustomerTest, booking => booking.CustomerId, customer => customer.CustomerId, (booking, customer) => new { State = customer.State, Amount = booking.Amount, Created = booking.Created })
                .Where(m => (m.Created >= interval.startdate && m.Created <= interval.enddate)).ToListAsync();

            if (list == null)
            {
                return NotFound();
            }

            // Format and add normal records to result
            var result = list
                .Select(m => new { State = m.State, Amount = m.Amount.ToString("C"), Date = m.Created.ToString("dd/MM/yyyy"), Month = m.Created.ToString("MMM") }).ToList();

            // Format and add state monthly total records to result
            result.AddRange(list
                .GroupBy(m => new { m.State, m.Created.Month })
                .Select(m => new { State = m.Key.State + ' ' + m.FirstOrDefault().Created.ToString("MMM"), Amount = m.Sum(n => n.Amount).ToString("C"), Date = "", Month = "" })
                .ToList());

            // Format and add state total records to result
            result.AddRange(list
                .GroupBy(m => m.State)
                .Select(m => new { State = m.Key + " Total", Amount = m.Sum(n => n.Amount).ToString("C"), Date = "", Month = "" })
                .ToList());

            // Format and add grand total records to result
            result.Add(list
                .Select(m => new { State = "Total", Amount = list.Sum(n => n.Amount).ToString("C"), Date = "", Month = "" })
                .FirstOrDefault());

            return Ok(result);
        }

        private bool BookingExists(int id)
        {
            return _context.tblBookingTest.Any(e => e.BookingId == id);
        }
    }
}