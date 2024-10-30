using AutoMapper;
using CodeProject.Server.Context;
using CodeProject.Server.Models;
using CodeProject.Server.Models.Dtos;
using CodeProject.Server.Models.Entities;
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
        private readonly IMapper _mapper;

        public ToDosController(ProviderFactory providerFactory, MoolahContext context, IMapper mapper)
        {
            _providerFactory = providerFactory;
            _context = context;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ToDoDto>>> SearchToDo([FromQuery] ToDoSearchParams searchParams)
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
            var toDos = await service.Search(searchParams);

            if (toDos == null || !toDos.Any())
            {
                return NotFound();
            }
            var toDosDto = _mapper.Map<IEnumerable<ToDoDto>>(toDos);

            return Ok(toDosDto);
        }

        

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<ToDo>> PostToDo([FromBody] ToDo toDo)
        {
            if(toDo == null)
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid request",
                    StatusCode = 400,
                    Detail = "Request body null"
                });
            }
            if(string.IsNullOrEmpty(toDo.Name) || string.IsNullOrEmpty(toDo.Description))
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid request",
                    StatusCode = 400,
                    Detail = "Name and Description required"
                });
            }
            var service = _providerFactory.GetToDoService(toDo.Provider.Name);

            try
            {
                await service.Add(toDo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Message = "A unhandled exception occurred",
                    StatusCode = 500,
                    Detail = ex.Message
                });
            }
            return CreatedAtAction("GetToDo", new { id = toDo.Id }, toDo);
        }
    }
}
