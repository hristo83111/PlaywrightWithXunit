using System.Runtime.CompilerServices;

/// <summary>
/// Provides support for asynchronous lazy initialization. 
/// The value is initialized only once and is accessed asynchronously.
/// </summary>
/// <typeparam name="T">The type of the value to be lazily initialized.</typeparam>
public class AsyncLazy<T>
{
    private readonly Lazy<Task<T>> _instance;

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class with the specified asynchronous factory method.
    /// </summary>
    /// <param name="taskFactory">A factory method that returns a task to initialize the value.</param>
    public AsyncLazy(Func<Task<T>> taskFactory)
    {
        _instance = new Lazy<Task<T>>(() => Task.Run(taskFactory));
    }

    /// <summary>
    /// Gets the lazily initialized value as a task.
    /// </summary>
    public Task<T> Value => _instance.Value;

    /// <summary>
    /// Gets an awaiter used to await the completion of the asynchronous lazy initialization.
    /// </summary>
    /// <returns>A task awaiter for the asynchronous operation.</returns>
    public TaskAwaiter<T> GetAwaiter() => Value.GetAwaiter();

    /// <summary>
    /// Gets a value indicating whether the asynchronous value has already been created.
    /// </summary>
    public bool IsValueCreated => _instance.IsValueCreated;
}