namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until the next frame.
/// </summary>
/// <remarks>
/// This can only be used on the client side.
/// </remarks>
public sealed class WaitForNextFrame : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete { get; private set; }
	/// <inheritdoc/>
	public WaitingStrategy WaitingStrategy => WaitingStrategy.Frame;

	/// <inheritdoc/>
	public void Tick()
	{
		IsComplete = true;
	}
}
