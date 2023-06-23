using Sandbox;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until a specified amount of seconds have passed.
/// </summary>
/// <remarks>
/// This is affected by <see cref="Game.TimeScale"/>.
/// </remarks>
public sealed class WaitForSeconds : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete => SecondsUntilComplete <= 0;
	/// <inheritdoc/>
	public WaitingStrategy WaitingStrategy { get; }

	/// <summary>
	/// The number of seconds left until completion.
	/// </summary>
	private TimeUntil SecondsUntilComplete { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForSeconds"/>.
	/// </summary>
	/// <param name="seconds">The number of seconds to wait.</param>
	/// <param name="waitingStrategy">The way for the coroutine to wait for completion.</param>
	public WaitForSeconds( float seconds, WaitingStrategy waitingStrategy = WaitingStrategy.Tick )
	{
		if ( waitingStrategy == WaitingStrategy.Frame )
			Game.AssertClientOrMenu();

		SecondsUntilComplete = seconds;
		WaitingStrategy = waitingStrategy;
	}

	/// <inheritdoc/>
	public void Tick()
	{
	}
}
