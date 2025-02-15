using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.DAL.Configurations;

public class PostcardConfiguration : IEntityTypeConfiguration<Postcard>
{
    public void Configure(EntityTypeBuilder<Postcard> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne<Travel>().WithMany();
        builder
            .Property(x => x.PhotoUrl).IsRequired();
    }
}