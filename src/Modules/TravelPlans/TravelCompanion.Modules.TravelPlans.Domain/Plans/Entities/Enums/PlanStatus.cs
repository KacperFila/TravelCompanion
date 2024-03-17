namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums
{
    public static class PlanStatus
    {
        public const string Accepted = nameof(Accepted);
        public const string Rejected = nameof(Rejected);
        public const string DuringAcceptance = nameof(DuringAcceptance);
        public const string DuringPlanning = nameof(DuringPlanning);

        public static bool IsValid(string status)
            => status is Accepted or Rejected or DuringAcceptance or DuringPlanning;
    }
}