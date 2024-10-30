using CodeProject.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeProject.Server.Models.Configurations
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
