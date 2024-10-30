using System.ComponentModel.DataAnnotations;

namespace CodeProject.Server.Models
{
#pragma warning disable CS8618
    public class ToDo
    {
        public int Id { get; private set; }

        [StringLength(100)]
        public string Name { get; private set; }
        [StringLength(1000)]
        public string Description { get; private set; }
        public Provider Provider { get; private set; }
    }
#pragma warning restore CS8618
}
