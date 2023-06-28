using Sandbox;

namespace Coroutines.Stallers;

/// <summary>
/// Pauses a coroutine until the next input simulation of a client.
/// </summary>
public sealed class WaitForNextSimulate : ICoroutineStaller
{
	/// <inheritdoc/>
	public bool IsComplete { get; private set; }
	/// <inheritdoc/>
	public ExecutionStrategy ExecutionStrategy => ExecutionStrategy.Simulate;

	/// <summary>
	/// The client whose simulation to wait for. Null if does not matter.
	/// </summary>
	private IClient? Client { get; }
	/// <summary>
	/// Whether or not to only count first time simulations for completion.
	/// </summary>
	private bool FirstTimeOnly { get; }

	/// <summary>
	/// Initializes a new instance of <see cref="WaitForNextSimulate"/>.
	/// </summary>
	/// <param name="client">The client whose simulation to wait for. Null if does not matter.</param>
	/// <param name="firstTimeOnly">Whether or not to only count first time simulations.</param>
	public WaitForNextSimulate( IClient? client, bool firstTimeOnly = true )
	{
		Client = client;
		FirstTimeOnly = firstTimeOnly;
	}

	/// <inheritdoc/>
	public void Update()
	{
		if ( Client is not null && Coroutine.SimulatedClient != Client )
			return;

		if ( FirstTimeOnly && !Prediction.FirstTime )
			return;

		IsComplete = true;
	}
}
