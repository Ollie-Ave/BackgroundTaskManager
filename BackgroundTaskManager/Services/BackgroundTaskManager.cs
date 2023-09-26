using BackgroundTaskManager.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackgroundTaskManager.Services
{
    internal class BackgroundTaskManager : IBackgroundTaskManager
    {
        private readonly IEnumerable<IBackgroundTask> registeredTasks;
        private readonly ILogger<BackgroundTaskManager>? logger;

        public BackgroundTaskManager(IEnumerable<IBackgroundTask> registeredTasks, ILogger<BackgroundTaskManager> logger)
        {
            this.registeredTasks = registeredTasks;
            this.logger = logger;
        }

        public BackgroundTaskManager(IEnumerable<IBackgroundTask> registeredTasks)
        {
            this.registeredTasks = registeredTasks;
        }

        public void Initialize()
        {
            foreach (IBackgroundTask task in this.registeredTasks)
            {
                this.StartTask(task);
            }
        }

        public void StartTask(IBackgroundTask task)
        {
            Task.Run(async () =>
            {
                try
                {
                    while (true)
                    {
                        await task.ExecuteAsync();

                        await Task.Delay(task.Interval);
                    }
                }
                catch (Exception ex)
                {
                    this.logger?.LogError(ex, "An exception occurred while executing background task {TaskName}", task.Name);
                }
            });
        }
    }
}
