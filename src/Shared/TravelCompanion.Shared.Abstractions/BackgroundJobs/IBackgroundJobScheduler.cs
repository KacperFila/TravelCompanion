using System;
using System.Linq.Expressions;

namespace TravelCompanion.Shared.Abstractions.BackgroundJobs;

public interface IBackgroundJobScheduler
{
    public string ScheduleAt(Expression<Action> method, DateTime date);
    public string Enqueue(Expression<Action> method);
    public void ScheduleDaily(Expression<Action> method);
}