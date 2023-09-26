namespace BackgroundTaskManager.Extensions
{
    using BackgroundTaskManager.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class IServiceProviderExtensions
    {
        public static void InitialiseBackgroundTasks(this IServiceProvider provider)
        {
            provider.GetRequiredService<IBackgroundTaskManager>().Initialize();
        }
    }
}
