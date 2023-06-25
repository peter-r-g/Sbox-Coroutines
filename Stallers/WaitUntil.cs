using Sandbox;
using System;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until a method returns true.
/// </summary>
public sealed class WaitUntil : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete => Waiter();
	/// <inheritdoc/>
	public ExecutionStrategy ExecutionStrategy { get; }

	/// <summary>
	/// The method wait for to return true.
	/// </summary>
	private Func<bool> Waiter { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitUntil"/>.
	/// </summary>
	/// <param name="waiter">The method to wait for.</param>
	/// <param name="executionStrategy">The way for the coroutine to wait for completion.</param>
	public WaitUntil( Func<bool> waiter, ExecutionStrategy executionStrategy = ExecutionStrategy.Preserve )
	{
		if ( executionStrategy == ExecutionStrategy.Frame )
			Game.AssertClientOrMenu();

		Waiter = waiter;
		ExecutionStrategy = executionStrategy;
	}

	/// <inheritdoc/>
	public void Update()
	{
	}
}
