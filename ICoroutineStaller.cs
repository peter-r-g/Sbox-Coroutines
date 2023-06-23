namespace Coroutines;

/// <summary>
/// Defines a type that can stall a coroutine.
/// </summary>
public interface ICoroutineStaller
{
	/// <summary>
	/// Returns whether or not the staller has completed.
	/// </summary>
	bool IsComplete { get; }
	/// <summary>
	/// Returns the way for the staller to be updated.
	/// </summary>
	WaitingStrategy WaitingStrategy { get; }

	/// <summary>
	/// Updates the stallers state.
	/// </summary>
	void Tick();
}
