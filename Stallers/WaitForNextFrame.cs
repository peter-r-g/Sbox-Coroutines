using static Sandbox.GameObjectSystem;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until the next frame.
/// </summary>
public sealed class WaitForNextFrame : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete { get; private set; }
	/// <inheritdoc/>
	public Stage PollingStage { get; } = Coroutine.DefaultPollingStage;

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForNextFrame"/>.
	/// </summary>
	public WaitForNextFrame()
	{
	}

	/// <inheritdoc/>
	public void Update()
	{
		IsComplete = true;
	}
}
