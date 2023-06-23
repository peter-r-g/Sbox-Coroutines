using Sandbox;
using System.Collections.Generic;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until another coroutine is completed.
/// </summary>
public sealed class WaitForCoroutine : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete { get; private set; }
	/// <inheritdoc/>
	public WaitingStrategy WaitingStrategy { get; }

	/// <summary>
	/// The coroutine to wait for.
	/// </summary>
	private IEnumerator<ICoroutineStaller> CoroutineToWaitFor { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForCoroutine"/>.
	/// </summary>
	/// <param name="coroutine">The coroutine to wait for.</param>
	/// <param name="waitingStrategy">The way for the coroutine to wait for completion.</param>
	public WaitForCoroutine( IEnumerator<ICoroutineStaller> coroutine, WaitingStrategy waitingStrategy = WaitingStrategy.Tick )
	{
		if ( waitingStrategy == WaitingStrategy.Frame )
			Game.AssertClientOrMenu();

		CoroutineToWaitFor = coroutine;
		WaitingStrategy = waitingStrategy;
	}

	/// <inheritdoc/>
	public void Tick()
	{
		IsComplete = Coroutine.IsComplete( CoroutineToWaitFor );
	}
}
