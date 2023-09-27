# Background Task Manager.

[![Build Gate](https://github.com/Ollie-Ave/BackgroundTaskManager/actions/workflows/BuildGate.yml/badge.svg?branch=release)](https://github.com/Ollie-Ave/BackgroundTaskManager/actions/workflows/BuildGate.yml)

This is a simple dotnet library used for running background tasks in a .Net application.

## Usage

This library is designed to be used in conjunction with a dependency injection framework.

The following example uses the [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/) library.

Minimally, we will need to register the `IBackgroundTaskManager` interface with the dependency injection framework.

```csharp
IServiceCollection serviceCollection = new ServiceCollection();

serviceCollection.AddBackgroundTaskManager();
```

Next, we will create a background task. This should implement the `IBackgroundTask` interface.

```csharp
internal class ExampleBackgroundTask : IBackgroundTask
{
    public string Name => "ExampleTask";

    public TimeSpan Interval => TimeSpan.FromSeconds(1);

    public Task ExecuteAsync()
    {
        Console.WriteLine($"Hello World From {this.Name}");

        throw new Exception("FooBar");

        return Task.CompletedTask;
    }
}
```

The `Name` property is used to identify the task. This is used when logging exceptions.

The `Interval` property is used to determine how often the task should be executed.

The `ExecuteAsync` method is called when the task is executed.

This task will now need to be registered with the dependency injection framework.

```csharp
serviceCollection.AddBackgroundTask<BackgroundTask>();
```

Finally, once we have built our service provider, we can start the background task manager.

```csharp
ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

serviceProvider.InitialiseBackgroundTasks();
```

This will run the tasks in the background. 
If any of the tasks throw an exception, provided a logger has been registered to the DI system, the exception will be logged and the task will be stopped.

It is that simple! Once the `InitialiseBackgroundTasks()` method is called, continue with whatever other code you need to execute and the registered tasks will be run in the background.

Each task will be run on a separate thread, so if you have multiple tasks, they will be run concurrently.