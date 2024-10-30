using CodeProject.Server.Context;
using CodeProject.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeProject.Server.Providers
{
    public class HomeService : ServiceBase
    {
        private readonly MoolahContext _context;

        public HomeService(MoolahContext context) : base(context) 
        {
            _context = context;
        }


        public override async Task<List<ToDo>> GetAll()
        {
            var result = await _context.ToDos.Where(x => 
                x.Provider.Name == "home")
                .ToListAsync();
            return result;
        }

        public override async Task<List<ToDo>> Search(ToDoSearchParams searchParams)
        {
            return await _context.ToDos
                .Where(x => x.Provider.Name == "home" &&
                    (!string.IsNullOrEmpty(searchParams.Search) &&
                        (x.Name.Contains(searchParams.Search) ||
                         x.Description.Contains(searchParams.Search))))
                .ToListAsync();
        }
    }
}
