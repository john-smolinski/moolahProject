using System.ComponentModel.DataAnnotations;

namespace CodeProject.Server.Models
{
#pragma warning disable CS8618
    public class ToDo
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public Provider Provider { get; set; }
    }
#pragma warning restore CS8618
}
