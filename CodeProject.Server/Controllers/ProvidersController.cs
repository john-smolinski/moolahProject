using CodeProject.Server.Context;
using CodeProject.Server.Models;
using CodeProject.Server.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeProject.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly MoolahContext _context;

        public ProvidersController(MoolahContext moolahContext)
        {
            _context = moolahContext;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> GetAll()
        {
            try
            {
                var result = await _context.Providers.ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Message = "An error occurred while fetching providers.",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message
                });
            }
        }
    }
}
