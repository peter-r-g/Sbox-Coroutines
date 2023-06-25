namespace Coroutines;

/// <summary>
/// Defines a way for coroutines to check their blocked state and resume execution.
/// </summary>
public enum ExecutionStrategy
{
	/// <summary>
	/// Preserves the execution strategy the coroutine is currently in.
	/// </summary>
	Preserve,
	/// <summary>
	/// Updates on game tick.
	/// </summary>
	Tick,
	/// <summary>
	/// Updates on game client frame.
	/// </summary>
	/// <remarks>
	/// This is only supported on client.
	/// </remarks>
	Frame,
	/// <summary>
	/// Updates on input simulation.
	/// </summary>
	Simulate
}
