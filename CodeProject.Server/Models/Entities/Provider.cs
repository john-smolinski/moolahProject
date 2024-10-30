using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CodeProject.Server.Models.Entities
{
#pragma warning disable CS8618
    [Index(nameof(Name), IsUnique = true)]
    public class Provider
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string Name { get; set; }

        public ICollection<ToDo> ToDos { get; set; }
    }
#pragma warning restore CS8618
}
