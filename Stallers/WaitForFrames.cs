using Sandbox;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine for a specified amount of frames.
/// </summary>
/// <remarks>
/// This can only be used on the client side.
/// </remarks>
public sealed class WaitForFrames : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete => FramesTillComplete <= 0;
	/// <inheritdoc/>
	public ExecutionStrategy ExecutionStrategy => ExecutionStrategy.Frame;

	/// <summary>
	/// The amount of frames left to wait.
	/// </summary>
	private int FramesTillComplete { get; set; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForFrames"/>.
	/// </summary>
	/// <param name="frames">The number of frames to wait for.</param>
	public WaitForFrames( int frames )
	{
		Game.AssertClientOrMenu();
		FramesTillComplete = frames;
	}

	/// <inheritdoc/>
	public void Update()
	{
		FramesTillComplete--;
	}
}
