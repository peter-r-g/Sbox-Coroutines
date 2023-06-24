using System.Collections.Generic;

namespace Coroutines;

/// <summary>
/// Represents an instance of a running coroutine.
/// </summary>
internal sealed class CoroutineInstance
{
	/// <summary>
	/// The coroutine that is being executed.
	/// </summary>
	internal IEnumerator<ICoroutineStaller> Coroutine { get; }
	/// <summary>
	/// Whether or not the coroutine has finished.
	/// </summary>
	internal bool IsFinished { get; private set; }

	/// <summary>
	/// Returns the current execution strategy of the coroutine.
	/// </summary>
	internal ExecutionStrategy CurrentExecutionStrategy => CurrentStall.ExecutionStrategy;
	/// <summary>
	/// Returns the current staller of the coroutine.
	/// </summary>
	private ICoroutineStaller CurrentStall => Coroutine.Current;

	/// <summary>
	/// Initializes a new instance of <see cref="CoroutineInstance"/>.
	/// </summary>
	/// <param name="coroutine">The coroutine to execute.</param>
	internal CoroutineInstance( IEnumerator<ICoroutineStaller> coroutine )
	{
		Coroutine = coroutine;
		IsFinished = !coroutine.MoveNext();
	}

	/// <summary>
	/// Updates the state of the coroutine.
	/// </summary>
	internal void Update()
	{
		if ( IsFinished )
			return;

		CurrentStall.Update();
		if ( !CurrentStall.IsComplete )
			return;

		if ( !Coroutine.MoveNext()|| CurrentStall is null )
		{
			IsFinished = true;
			return;
		}
	}
}
