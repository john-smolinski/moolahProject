using Microsoft.Identity.Client;

namespace CodeProject.Server.Models
{
    public class ToDoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PoviderName { get; set; }
    }
}
