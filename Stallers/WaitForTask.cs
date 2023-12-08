using System.Threading.Tasks;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until an asynchronous task completes.
/// </summary>
public sealed class WaitForTask : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete => Task.IsCompleted;
	/// <inheritdoc/>
	public ExecutionStrategy ExecutionStrategy => ExecutionStrategy.Frame;

	/// <summary>
	/// The task to wait for.
	/// </summary>
	private Task Task { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForTask"/>.
	/// </summary>
	/// <param name="task">The task to wait for.</param>
	public WaitForTask( Task task )
	{
		Task = task;
	}

	/// <inheritdoc/>
	public void Update()
	{
	}
}
