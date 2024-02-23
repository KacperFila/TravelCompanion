using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.DAL.Configurations;

public class TravelRatingConfiguration : IEntityTypeConfiguration<TravelRating>
{
    public void Configure(EntityTypeBuilder<TravelRating> builder)
    {
        builder.HasKey(x => x.Id);
    }
}