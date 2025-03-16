using System.Collections.Generic;
using System.Linq;

public class ConnectionManager
{
    private readonly Dictionary<string, HashSet<string>> _userConnections = new();

    public void AddConnection(string userId, string connectionId)
    {
        lock (_userConnections)
        {
            if (!_userConnections.ContainsKey(userId))
                _userConnections[userId] = new HashSet<string>();

            _userConnections[userId].Add(connectionId);
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
                    _userConnections.Remove(userId);
            }
        }
    }

    public IEnumerable<string> GetConnections(string userId)
    {
        var temp = _userConnections;
        return _userConnections.TryGetValue(userId, out var connections) ? connections : Enumerable.Empty<string>();
    }
}
