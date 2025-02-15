using TravelCompanion.Modules.Travels.Shared.DTO;
using TravelCompanion.Shared.Abstractions.Emails;

namespace TravelCompanion.Modules.Emails.Core.DTO
{
    internal class ThrowbackEmailDto : Email
    {
        public ThrowbackEmailDto(string month, List<PostcardDto> postcards)
        {
            Subject = $"See one of your last's year {month} travel!";
            Body = $"Do you remember that travel? See your postcards:\n";

            foreach (var postcard in postcards)
            {
                Body += $"Title: {postcard.Title}\n";
                Body += $"Description: {postcard.Description}\n";
                Body += $"AddedBy: {postcard.AddedById}\n";
                Body += $"PhotoUrl: {postcard.PhotoUrl}\n";
            }
        }
    }
}