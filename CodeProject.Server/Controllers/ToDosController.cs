using CodeProject.Server.Context;
using CodeProject.Server.Models;
using CodeProject.Server.Providers;
using Microsoft.AspNetCore.Mvc;

namespace CodeProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly ProviderFactory _providerFactory;
        private readonly MoolahContext _context;
        
        public ToDosController(ProviderFactory providerFactory, MoolahContext context)
        {
            _providerFactory = providerFactory;
            _context = context;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ToDo>>> SearchToDo([FromQuery] ToDoSearchParams searchParams)
        {
            if (searchParams == null || string.IsNullOrEmpty(searchParams.Provider))
            {
                return BadRequest();
            }
            if (searchParams.Provider != "home" && searchParams.Provider != "office")
            { 
                return BadRequest();
            }

            var service = _providerFactory.GetToDoService(searchParams.Provider);
            var result = await service.Search(searchParams);

            if (result.Count == 0)
            {
                return NotFound();
            }
            return result;
        }







        
        //// PUT: api/ToDos/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutToDo(int id, ToDo toDo)
        //{
        //    if (id != toDo.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(toDo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ToDoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/ToDos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<ToDo>> PostToDo(ToDo toDo)
        //{
        //    _context.ToDos.Add(toDo);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ToDoExists(toDo.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetToDo", new { id = toDo.Id }, toDo);
        //}

        // DELETE: api/ToDos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo(int id)
        {
            var toDo = await _context.ToDos.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }

            _context.ToDos.Remove(toDo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoExists(int id)
        {
            return _context.ToDos.Any(e => e.Id == id);
        }
        

       



    }
}
