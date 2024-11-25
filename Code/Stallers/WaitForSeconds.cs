using Sandbox;
using static Sandbox.GameObjectSystem;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until a specified amount of seconds have passed.
/// </summary>
public sealed class WaitForSeconds : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete => SecondsUntilComplete <= 0;
	/// <inheritdoc/>
	public Stage PollingStage { get; }

	/// <summary>
	/// The number of seconds left until completion.
	/// </summary>
	private TimeUntil SecondsUntilComplete { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForSeconds"/>.
	/// </summary>
	/// <param name="seconds">The number of seconds to wait.</param>
	/// <param name="pollingStage">The way for the coroutine to wait for completion.</param>
	public WaitForSeconds( float seconds, Stage pollingStage = Coroutine.PreservePollingStage )
	{
		SecondsUntilComplete = seconds;
		PollingStage = pollingStage;
	}

	/// <inheritdoc/>
	public void Update()
	{
	}
}
