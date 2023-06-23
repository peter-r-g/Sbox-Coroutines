namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine for a specified amount of ticks.
/// </summary>
public sealed class WaitForTicks : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete => TicksTillComplete <= 0;
	/// <inheritdoc/>
	public WaitingStrategy WaitingStrategy => WaitingStrategy.Tick;

	/// <summary>
	/// The amount of ticks left to wait.
	/// </summary>
	private int TicksTillComplete { get; set; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForTicks"/>.
	/// </summary>
	/// <param name="ticks">The number of ticks to wait for.</param>
	public WaitForTicks( int ticks )
	{
		TicksTillComplete = ticks;
	}

	/// <inheritdoc/>
	public void Tick()
	{
		TicksTillComplete--;
	}
}
