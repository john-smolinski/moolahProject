using CodeProject.Server.Context;
using CodeProject.Server.Models;
using CodeProject.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeProject.Server.Providers
{
    public abstract class ServiceBase(MoolahContext context) : IProviderService
    {

        public abstract Task<List<ToDo>> GetAll();

        public abstract Task<List<ToDo>> Search(ToDoSearchParams searchParams);

        public async Task<ToDo> GetById(int id)
        {
            try
            {
                return await context.ToDos.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return null;
            } 
        }

        public async Task Add(ToDo toDo)
        {
            context.ToDos.Add(toDo);
            await context.SaveChangesAsync();    
        }

        public async Task Update(ToDo toDo)
        {
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var todo = await context.ToDos.FindAsync(id);
            if (todo != null)
            {
                context.Remove(todo);
                await context.SaveChangesAsync();
            }
        }

        private bool ToDoExists(int id)
        {
            return context.ToDos.Any(e => e.Id == id);
        }
    }
}
