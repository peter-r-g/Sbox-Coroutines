using static Sandbox.GameObjectSystem;

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
	/// Returns the way for the staller to be updated and execution to continue.
	/// </summary>
	Stage PollingStage { get; }

	/// <summary>
	/// Updates the stallers state.
	/// </summary>
	void Update();
}
