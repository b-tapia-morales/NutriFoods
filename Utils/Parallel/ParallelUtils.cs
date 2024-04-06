using System.Threading.Tasks.Dataflow;

namespace Utils.Parallel;

public static class ParallelUtils
{
    public static async Task AsyncParallelForEach<T>(this IAsyncEnumerable<T> source, Func<T, Task> body,
        int maxDegreeOfParallelism = DataflowBlockOptions.Unbounded, TaskScheduler? scheduler = null)
    {
        var options = new ExecutionDataflowBlockOptions
        {
            MaxDegreeOfParallelism = maxDegreeOfParallelism
        };
        if (scheduler != null)
            options.TaskScheduler = scheduler;

        var block = new ActionBlock<T>(body, options);

        await foreach (var item in source)
            await block.SendAsync(item);

        block.Complete();
        await block.Completion;
    }
}