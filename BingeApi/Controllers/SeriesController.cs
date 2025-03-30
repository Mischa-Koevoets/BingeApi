using BingeApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BingeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly BingeDbContext _context;

        public SeriesController(BingeDbContext context)
        {
            _context = context;
        }

        // GET: api/Series
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Series>>> GetSeries()
        {
            return await _context.Series.Include(s => s.SeriesGenres).ThenInclude(sg => sg.Genre).ToListAsync();
        }

        // GET: api/Series/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Series>> GetSeries(int id)
        {
            var series = await _context.Series.Include(s => s.SeriesGenres).ThenInclude(sg => sg.Genre).FirstOrDefaultAsync(s => s.Id == id);

            if (series == null)
            {
                return NotFound();
            }

            return series;
        }

        // POST: api/Series
        [HttpPost]
        public async Task<ActionResult<Series>> PostSeries(Series series)
        {
            _context.Series.Add(series);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSeries), new { id = series.Id }, series);
        }

        // PUT: api/Series/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeries(int id, Series series)
        {
            if (id != series.Id)
            {
                return BadRequest();
            }

            _context.Entry(series).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Series.Any(e => e.Id == id))
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

        // DELETE: api/Series/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeries(int id)
        {
            var series = await _context.Series.FindAsync(id);
            if (series == null)
            {
                return NotFound();
            }

            _context.Series.Remove(series);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
