using System.Collections.Generic;
using static Sandbox.GameObjectSystem;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until another coroutine is completed.
/// </summary>
public sealed class WaitForCoroutine : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete { get; private set; }
	/// <inheritdoc/>
	public Stage PollingStage { get; }

	/// <summary>
	/// The coroutine to wait for.
	/// </summary>
	private IEnumerator<ICoroutineStaller> CoroutineToWaitFor { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForCoroutine"/>.
	/// </summary>
	/// <param name="coroutine">The coroutine to wait for.</param>
	/// <param name="pollingStage">The way for the coroutine to wait for completion.</param>
	public WaitForCoroutine( IEnumerator<ICoroutineStaller> coroutine, Stage pollingStage = Coroutine.PreservePollingStage )
	{
		CoroutineToWaitFor = coroutine;
		PollingStage = pollingStage;
	}

	/// <inheritdoc/>
	public void Update()
	{
		IsComplete = Coroutine.IsComplete( CoroutineToWaitFor );
	}
}
