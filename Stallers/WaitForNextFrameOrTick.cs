using Sandbox;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until the next frame or tick.
/// This will prefer frame and fall back on tick when it is not available.
/// </summary>
public sealed class WaitForNextFrameOrTick : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete { get; private set; }
	/// <inheritdoc/>
	public ExecutionStrategy ExecutionStrategy { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForNextFrameOrTick"/>.
	/// </summary>
	public WaitForNextFrameOrTick()
	{
		if ( Game.IsServer )
			ExecutionStrategy = ExecutionStrategy.Tick;
		else
			ExecutionStrategy = ExecutionStrategy.Frame;
	}

	/// <inheritdoc/>
	public void Update()
	{
		IsComplete = true;
	}
}
