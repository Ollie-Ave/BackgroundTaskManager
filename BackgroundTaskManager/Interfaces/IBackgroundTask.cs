namespace BackgroundTaskManager.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IBackgroundTask
    {
        public string Name { get; }

        public TimeSpan Interval { get; }

        public Task ExecuteAsync();
    }
}
