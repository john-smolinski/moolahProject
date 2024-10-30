using Microsoft.Identity.Client;

namespace CodeProject.Server.Models.Dtos
{
#pragma warning disable CS8618
    public class ToDoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProviderName { get; set; }
    }
#pragma warning restore
}
