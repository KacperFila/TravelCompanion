using System;
using System.Threading.Tasks;

namespace TravelCompanion.Shared.Infrastructure.Postgres
{
    public interface IUnitOfWork
    {
        Task ExecuteAsync(Func<Task> action);
    }
}