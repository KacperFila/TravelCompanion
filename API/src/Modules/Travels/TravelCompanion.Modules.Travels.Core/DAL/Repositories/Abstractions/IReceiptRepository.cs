using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.DAL.Repositories.Abstractions;

internal interface IReceiptRepository
{
    Task AddAsync(Receipt receipt);
}