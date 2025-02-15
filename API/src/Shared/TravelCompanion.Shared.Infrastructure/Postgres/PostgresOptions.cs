namespace TravelCompanion.Shared.Infrastructure.Postgres
{
    internal class PostgresOptions
    {
        public string ConnectionString { get; set; }
        public string HangfireConnectionString { get; set; }
    }
}