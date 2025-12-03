using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace TravelCompanion.Shared.Infrastructure.RealTime;

public class ConnectionManager
{
    private readonly Dictionary<string, HashSet<string>> _userConnections = new();
    private readonly ILogger<ConnectionManager> _logger;

    public ConnectionManager(ILogger<ConnectionManager> logger)
    {
        _logger = logger;
    }

    public void AddConnection(string userId, string connectionId)
    {
        lock (_userConnections)
        {
            if (!_userConnections.ContainsKey(userId))
                _userConnections[userId] = new HashSet<string>();

            _userConnections[userId].Add(connectionId);

            _logger.LogInformation("User '{UserId}' now has {ConnectionCount} connection(s).", userId, _userConnections[userId].Count);

            var totalConnections = _userConnections.Sum(kvp => kvp.Value.Count);
            _logger.LogInformation("Total connections across all users: {TotalConnections}.", totalConnections);
        }
    }

    public void RemoveConnection(string userId, string connectionId)
    {
        lock (_userConnections)
        {
            if (_userConnections.ContainsKey(userId))
            {
                _userConnections[userId].Remove(connectionId);
                if (_userConnections[userId].Count == 0)
                {
                    _userConnections.Remove(userId);
                }
            }
        }
    }

    public IEnumerable<string> GetConnections(string userId)
    {
        return _userConnections.TryGetValue(userId, out var connections) 
            ? connections 
            : Enumerable.Empty<string>();
    }
}