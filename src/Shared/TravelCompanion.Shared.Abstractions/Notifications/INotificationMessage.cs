using System;

namespace TravelCompanion.Shared.Abstractions.Notifications;

public interface INotificationMessage
{
    public string Title { get; set; }
    public string Message { get; set; }
    public string SentFrom { get; set; }
    public DateTime SentAt { get; set; }
}