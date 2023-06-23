using Sandbox;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until a specified amount of seconds have passed.
/// </summary>
/// <remarks>
/// This is not affected by <see cref="Game.TimeScale"/>.
/// </remarks>
public sealed class WaitForRealSeconds : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete => RealSecondsUntilComplete <= 0;
	/// <inheritdoc/>
	public WaitingStrategy WaitingStrategy { get; }

	/// <summary>
	/// The number of seconds left until completion.
	/// </summary>
	private RealTimeUntil RealSecondsUntilComplete { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForRealSeconds"/>.
	/// </summary>
	/// <param name="seconds">The number of seconds to wait.</param>
	/// <param name="waitingStrategy">The way for the coroutine to wait for completion.</param>
	public WaitForRealSeconds( float seconds, WaitingStrategy waitingStrategy = WaitingStrategy.Tick )
	{
		if ( waitingStrategy == WaitingStrategy.Frame )
			Game.AssertClientOrMenu();

		RealSecondsUntilComplete = seconds;
		WaitingStrategy = waitingStrategy;
	}

	/// <inheritdoc/>
	public void Tick()
	{
	}
}
