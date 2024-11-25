using static Sandbox.GameObjectSystem;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine for a specified amount of frames.
/// </summary>
public sealed class WaitForFrames : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete => FramesTillComplete <= 0;
	/// <inheritdoc/>
	public Stage PollingStage { get; } = Coroutine.DefaultPollingStage;

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
		FramesTillComplete = frames;
	}

	/// <inheritdoc/>
	public void Update()
	{
		FramesTillComplete--;
	}
}
