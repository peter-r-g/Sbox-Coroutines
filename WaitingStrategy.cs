namespace Coroutines;

/// <summary>
/// Defines the way for coroutines to be updated.
/// </summary>
public enum WaitingStrategy
{
	/// <summary>
	/// Updates on game ticks.
	/// </summary>
	Tick,
	/// <summary>
	/// Updates on game frames.
	/// </summary>
	/// <remarks>
	/// This is only supported on client.
	/// </remarks>
	Frame
}
