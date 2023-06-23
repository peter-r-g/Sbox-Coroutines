namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until the next tick.
/// </summary>
public sealed class WaitForNextTick : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete { get; private set; }
	/// <inheritdoc/>
	public WaitingStrategy WaitingStrategy => WaitingStrategy.Tick;

	/// <inheritdoc/>
	public void Tick()
	{
		IsComplete = true;
	}
}
