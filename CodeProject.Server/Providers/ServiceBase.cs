using CodeProject.Server.Context;
using CodeProject.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeProject.Server.Providers
{
    public abstract class ServiceBase(MoolahContext context) : IProviderService
    {
        public async Task Delete(int id)
        {
            var todo = await context.ToDos.FindAsync(id);
            if (todo != null)
            {
                context.Remove(todo);
                await context.SaveChangesAsync();
            }
        }

        public abstract Task<List<ToDo>> GetAll();

        public abstract Task<List<ToDo>> Search(ToDoSearchParams searchParams);

        public async Task Update(ToDo toDo)
        {
            await context.SaveChangesAsync();
        }
    }
}
