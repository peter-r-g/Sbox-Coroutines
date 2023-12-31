﻿using System.Threading.Tasks;
using static Sandbox.GameObjectSystem;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until an asynchronous task completes.
/// </summary>
public sealed class WaitForTask : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete => Task.IsCompleted;
	/// <inheritdoc/>
	public Stage PollingStage { get; } = Coroutine.DefaultPollingStage;

	/// <summary>
	/// The task to wait for.
	/// </summary>
	private Task Task { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForTask"/>.
	/// </summary>
	/// <param name="task">The task to wait for.</param>
	public WaitForTask( Task task )
	{
		Task = task;
	}

	/// <inheritdoc/>
	public void Update()
	{
	}
}
