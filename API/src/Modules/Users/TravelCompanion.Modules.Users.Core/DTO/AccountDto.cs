using System;
using System.Collections.Generic;

namespace TravelCompanion.Modules.Users.Core.DTO
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Dictionary<string, IEnumerable<string>> Claims { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}