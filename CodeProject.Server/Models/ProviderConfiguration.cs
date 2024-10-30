using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeProject.Server.Models
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.ToDos)
                .WithOne(t => t.Provider)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
