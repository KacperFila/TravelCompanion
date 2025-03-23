using System;
using System.Collections.Generic;
using TravelCompanion.Shared.Abstractions.Kernel;

namespace TravelCompanion.Modules.Users.Core.Entities;

public sealed class User : IAuditable
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }
    public Dictionary<string, IEnumerable<string>> Claims { get; set; }
    public string? VerificationToken { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public Guid? ActivePlanId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public void SetActivePlan(Guid planId)
    {
        ActivePlanId = planId;
    }
}