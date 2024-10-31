using CodeProject.Server.Models;
using CodeProject.Server.Models.Entities;

namespace CodeProject.Server.Providers
{
    public interface IProviderService
    {
        Task<List<ToDo>> GetAll();
        Task<List<ToDo>> Search(ToDoSearchParams searchParams);
        Task<ToDo> GetById(int id);
        Task Add(ToDo toDo);
        Task Update(ToDo toDo);
        Task Delete(int id);
    }
}
