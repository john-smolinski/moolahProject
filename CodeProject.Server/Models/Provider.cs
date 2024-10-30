using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CodeProject.Server.Models
{
#pragma warning disable CS8618
    [Index(nameof(Name), IsUnique =true)]
    public class Provider
    {
        public int Id { get; private set; }
        
        [StringLength(10)]
        public string Name { get; private set; }
        
        public ICollection<ToDo> ToDos { get; private set; }
    }
#pragma warning restore CS8618
}
