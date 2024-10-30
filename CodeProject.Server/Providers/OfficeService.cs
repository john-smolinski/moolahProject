using CodeProject.Server.Context;
using CodeProject.Server.Models;
using CodeProject.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeProject.Server.Providers
{
    public class OfficeService : ServiceBase
    {
        private readonly MoolahContext _context;

        public OfficeService(MoolahContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override async Task<List<ToDo>> GetAll()
        {
            return await _context.ToDos.Where(x => 
                x.Provider.Name == "office")
                .ToListAsync();
        }

        public override async Task<List<ToDo>> Search(ToDoSearchParams searchParams)
        {
            return await _context.ToDos
                .Where(x => x.Provider.Name == "office" &&
                    (!string.IsNullOrEmpty(searchParams.Search) &&
                        (x.Name.Contains(searchParams.Search) ||
                         x.Description.Contains(searchParams.Search))))
                .ToListAsync();
        }
    }
}
