using CodeProject.Server.Models;

namespace CodeProject.Server.Providers
{
    public interface IProviderService
    {
        Task<List<ToDo>> GetAll();
        Task<List<ToDo>> Search(ToDoSearchParams searchParams);
        Task Update(ToDo toDo);
        Task Delete(int id);
    }
}
