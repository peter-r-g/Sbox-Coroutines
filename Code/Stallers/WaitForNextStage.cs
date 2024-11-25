using static Sandbox.GameObjectSystem;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until the next execution of a <see cref="Stage"/>.
/// </summary>
public sealed class WaitForNextStage : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete { get; private set; }
	/// <inheritdoc/>
	public Stage PollingStage { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForNextStage"/>.
	/// </summary>
	public WaitForNextStage( Stage stage )
	{
		PollingStage = stage;
	}

	/// <inheritdoc/>
	public void Update()
	{
		IsComplete = true;
	}
}
