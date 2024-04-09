using Hangfire;
using System;
using System.Linq.Expressions;
using Hangfire.Common;
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

    public void ScheduleDaily(Expression<Action> method)
    {
        var manager= new RecurringJobManager();
        manager.AddOrUpdate(Guid.NewGuid().ToString(), Job.FromExpression(method), Cron.Daily());
    }
}