using System.Collections.Generic;

namespace TravelCompanion.Shared.Infrastructure.Modules
{
    internal record ModuleInfo(string Name, string Path, IEnumerable<string> Policies);
}