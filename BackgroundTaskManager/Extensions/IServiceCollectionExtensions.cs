namespace Microsoft.Extensions.DependencyInjection
{
    using BackgroundTaskManager.Interfaces;
    using BackgroundTaskManager.Services;

    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBackgroundTaskManager(this IServiceCollection services)
        {
            services.AddSingleton<IBackgroundTaskManager, BackgroundTaskManager>();

            return services;
        }

        public static IServiceCollection AddBackgroundTask<T>(this IServiceCollection services)
            where T : IBackgroundTask
        {
            services.AddSingleton(typeof(IBackgroundTask), typeof(T));

            return services;
        }
    }
}
