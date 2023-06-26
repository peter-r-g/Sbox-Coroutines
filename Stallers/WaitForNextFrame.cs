using Sandbox;

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
	public ExecutionStrategy ExecutionStrategy => ExecutionStrategy.Frame;

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForNextFrame"/>.
	/// </summary>
	public WaitForNextFrame()
	{
		Game.AssertClientOrMenu();
	}

	/// <inheritdoc/>
	public void Update()
	{
		IsComplete = true;
	}
}
