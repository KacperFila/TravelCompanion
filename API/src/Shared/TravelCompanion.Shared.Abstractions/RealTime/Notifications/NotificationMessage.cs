using System;
using TravelCompanion.Shared.Abstractions.Notifications;

namespace TravelCompanion.Shared.Abstractions.RealTime.Notifications;

public class NotificationMessage : INotificationMessage
{
    public string Title { get; set; }
    public string Message { get; set; }
    public DateTime SentAt { get; set; }
    public string SentFrom { get; set; }

    public static NotificationMessage Create(string title, string message, string from)
    {
        return new NotificationMessage()
        {
            Message = message,
            SentAt = DateTime.UtcNow,
            Title = title,
            SentFrom = from
        };
    }

    public static NotificationMessage Create(string title, string message)
    {
        return new NotificationMessage()
        {
            Message = message,
            SentAt = DateTime.UtcNow,
            Title = title,
            SentFrom = null
        };
    }
}