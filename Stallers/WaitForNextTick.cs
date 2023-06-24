namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until the next tick.
/// </summary>
public sealed class WaitForNextTick : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete { get; private set; }
	/// <inheritdoc/>
	public ExecutionStrategy ExecutionStrategy => ExecutionStrategy.Tick;

	/// <inheritdoc/>
	public void Update()
	{
		IsComplete = true;
	}
}
