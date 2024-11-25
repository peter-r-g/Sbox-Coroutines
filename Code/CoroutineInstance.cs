using Sandbox;
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
	/// Returns the current polling stage of the coroutine.
	/// </summary>
	internal GameObjectSystem.Stage CurrentPollingStage
	{
		get
		{
			if ( CurrentStall.PollingStage == Coroutines.Coroutine.PreservePollingStage )
				return LastPollingStage;

			return CurrentStall.PollingStage;
		}
	}
	/// <summary>
	/// The last valid polling stage that was used.
	/// </summary>
	private GameObjectSystem.Stage LastPollingStage { get; set; }
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
		LastPollingStage = Coroutines.Coroutine.DefaultPollingStage;

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

		if ( !Coroutine.MoveNext() || CurrentStall is null )
		{
			IsFinished = true;
			return;
		}

		if ( CurrentStall.PollingStage != Coroutines.Coroutine.PreservePollingStage )
			LastPollingStage = CurrentStall.PollingStage;
	}
}
