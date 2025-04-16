using System.Runtime.CompilerServices;

namespace CoreFramework.Driver;

/// <summary>
/// Provides support for asynchronous lazy initialization. 
/// The value is initialized only once and supports asynchronous operations.
/// </summary>
/// <typeparam name="T">The type of the value being lazily initialized.</typeparam>
public class AsyncLazy<T> : Lazy<Task<T>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class with a synchronous value factory.
    /// </summary>
    /// <param name="valueFactory">A function that produces the value when it is needed.</param>
    public AsyncLazy(Func<T> valueFactory)
        : base(() => Task.Factory.StartNew(valueFactory))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class with an asynchronous task factory.
    /// </summary>
    /// <param name="taskFactory">A function that produces a task which computes the value when it is needed.</param>
    public AsyncLazy(Func<Task<T>> taskFactory)
        : base(() => Task.Factory.StartNew(taskFactory).Unwrap())
    {
    }


    /// <summary>
    /// Gets an awaiter used to await the completion of the asynchronous operation.
    /// </summary>
    /// <returns>A <see cref="TaskAwaiter{T}"/> instance used to await the task.</returns>
    public TaskAwaiter<T> GetAwaiter()
    {
        return Value.GetAwaiter();
    }
}

