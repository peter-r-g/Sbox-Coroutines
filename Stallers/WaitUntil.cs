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
	public WaitingStrategy WaitingStrategy { get; }

	/// <summary>
	/// The method wait for to return true.
	/// </summary>
	private Func<bool> Waiter { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitUntil"/>.
	/// </summary>
	/// <param name="waiter">The method to wait for.</param>
	/// <param name="waitingStrategy">The way for the coroutine to wait for completion.</param>
	public WaitUntil( Func<bool> waiter, WaitingStrategy waitingStrategy = WaitingStrategy.Tick )
	{
		if ( waitingStrategy == WaitingStrategy.Frame )
			Game.AssertClientOrMenu();

		Waiter = waiter;
		WaitingStrategy = waitingStrategy;
	}

	/// <inheritdoc/>
	public void Tick()
	{
	}
}
