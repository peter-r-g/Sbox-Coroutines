namespace Coroutines;

/// <summary>
/// Defines a way for coroutines to check their blocked state and resume execution.
/// </summary>
public enum ExecutionStrategy
{
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
}
