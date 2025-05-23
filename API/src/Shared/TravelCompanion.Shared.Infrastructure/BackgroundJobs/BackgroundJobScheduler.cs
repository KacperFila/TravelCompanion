using Hangfire;
using Hangfire.Common;
using System;
using System.Linq.Expressions;
using TravelCompanion.Shared.Abstractions.BackgroundJobs;

namespace TravelCompanion.Shared.Infrastructure.BackgroundJobs;

public class BackgroundJobScheduler : IBackgroundJobScheduler
{
    public string ScheduleAt(Expression<Action> method, DateTime date)
    {
        var jobId = BackgroundJob.Schedule(method, date);
        return jobId;
    }

    public string Enqueue(Expression<Action> method)
    {
        var jobId = BackgroundJob.Enqueue(method);
        return jobId;
    }

    public void ScheduleDaily(Expression<Action> method, string jobId)
    {
        var manager = new RecurringJobManager();
        manager.AddOrUpdate(jobId, Job.FromExpression(method), Cron.Daily());
    }
    public void ScheduleMonthly(Expression<Action> method, string jobId)
    {
        var manager = new RecurringJobManager();
        manager.AddOrUpdate(jobId, Job.FromExpression(method), Cron.Monthly());
    }

    public void RemoveIfExists(string jobId)
    {
        var manager = new RecurringJobManager();
        manager.RemoveIfExists(jobId);
    }
}