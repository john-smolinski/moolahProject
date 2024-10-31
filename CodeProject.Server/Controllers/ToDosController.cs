using AutoMapper;
using CodeProject.Server.Context;
using CodeProject.Server.Models;
using CodeProject.Server.Models.Dtos;
using CodeProject.Server.Models.Entities;
using CodeProject.Server.Providers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoDto>> GetToDoById(int id, [FromQuery] string provider)
        {
            if (string.IsNullOrEmpty(provider))
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Provider name is required",
                    StatusCode = 400,
                    Detail = "The provider query parameter was not provided"
                });
            }

            var service = _providerFactory.GetToDoService(provider);
            var toDo = await service.GetById(id);

            if (toDo == null)
            {
                return NotFound(new ErrorResponse
                {
                    Message = "ToDo item not found",
                    StatusCode = 404,
                    Detail = $"No ToDo found with Id = {id} and provider {provider}"
                });
            }

            var toDoDto = _mapper.Map<ToDoDto>(toDo);
            return Ok(toDoDto);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id, [FromQuery]string provider)
        {
            if (string.IsNullOrEmpty(provider))
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Provider name is required",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Detail = "The provider query parameter was not provided"
                });
            }

            if(!await _context.ToDos.AnyAsync(x => x.Id == id && x.Provider.Name == provider))
            {
                return NotFound(new ErrorResponse
                {
                    Message = "Task not found",
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"No task found with Id {id} for provider '{provider}'"
                });
            }

            var service = _providerFactory.GetToDoService(provider);

            try
            {
                await service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Message = "A unhandeled exception occured",
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message
                });
            }
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
        public async Task<ActionResult<ToDoDto>> PostToDo([FromBody] ToDoDto toDo)
        {
            if (toDo == null)
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid request",
                    StatusCode = 400,
                    Detail = "Request body null"
                });
            }
            if (string.IsNullOrEmpty(toDo.Name) || string.IsNullOrEmpty(toDo.Description))
            {
                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid request",
                    StatusCode = 400,
                    Detail = "Name and Description required"
                });
            }

            var service = _providerFactory.GetToDoService(toDo.ProviderName);

            ToDo createdToDo;
            try
            {
                // Map to ToDo entity and save it
                createdToDo = _mapper.Map<ToDo>(toDo);
                await service.Add(createdToDo);
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

            // Use the Id of the created ToDo entity in CreatedAtAction
            return CreatedAtAction(nameof(GetToDoById), new { id = createdToDo.Id }, createdToDo);
        }

    }
}
