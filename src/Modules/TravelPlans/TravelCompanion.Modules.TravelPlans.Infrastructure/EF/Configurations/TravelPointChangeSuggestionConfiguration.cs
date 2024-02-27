using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

public class TravelPointChangeSuggestionConfiguration : IEntityTypeConfiguration<TravelPointChangeSuggestion>
{
    public void Configure(EntityTypeBuilder<TravelPointChangeSuggestion> builder)
    {
        builder.Property(x => x.TravelPlanPointId)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder.Property(x => x.SuggestedById)
            .HasConversion(x => x.Value, x => new ParticipantId(x));

        builder.Property(x => x.SuggestionId)
            .HasConversion(x => x.Value, x => new TravelPointChangeSuggestionId(x));

        builder.HasKey(x => x.SuggestionId);

        builder.Property(x => x.PlaceName).IsRequired();
    }
}