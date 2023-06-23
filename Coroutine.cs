using Sandbox;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Coroutines;

/// <summary>
/// Contains functionality to start, stop, and check completion of coroutines.
/// <br/>
/// <br/>
/// A coroutine is a method that can be paused and resumed later in time.
/// The pauses are controlled by a <see cref="ICoroutineStaller"/>.
/// All coroutines are executed within the main thread and will never leave it.
/// All calls within this class are deferred to the main thread if called from outside of it.
/// </summary>
public static class Coroutine
{
	/// <summary>
	/// A thread-safe queue to add new coroutines.
	/// </summary>
	private static ConcurrentQueue<IEnumerator<ICoroutineStaller>> CoroutinesToAdd { get; } = new();
	/// <summary>
	/// A thread-safe queue to remove existing coroutines.
	/// </summary>
	private static ConcurrentQueue<IEnumerator<ICoroutineStaller>> CoroutinesToRemove { get; } = new();
	/// <summary>
	/// A list of all currently executing coroutines.
	/// </summary>
	private static List<CoroutineInstance> Coroutines { get; } = new();

	/// <summary>
	/// Starts a new coroutine that is fetched from the <paramref name="coroutineMethod"/>.
	/// </summary>
	/// <param name="coroutineMethod">The method to get the coroutine instance from.</param>
	/// <returns>The coroutine instance that was retrieved.</returns>
    public static IEnumerator<ICoroutineStaller> Start( Func<IEnumerator<ICoroutineStaller>> coroutineMethod )
    {
		var coroutine = coroutineMethod.Invoke();
		CoroutinesToAdd.Enqueue( coroutine );
		return coroutine;
	}

	/// <summary>
	/// Starts a new coroutine that is fetched from the <paramref name="coroutineMethod"/>.
	/// This will pass <paramref name="firstValue"/> to <paramref name="coroutineMethod"/>.
	/// </summary>
	/// <typeparam name="T1">The type of the parameter to pass to the <paramref name="coroutineMethod"/>.</typeparam>
	/// <param name="coroutineMethod">The method to get the coroutine instance from.</param>
	/// <param name="firstValue">The value to pass to the <paramref name="coroutineMethod"/>.</param>
	/// <returns>The coroutine instance that was retrieved.</returns>
	public static IEnumerator<ICoroutineStaller> Start<T1>( Func<T1, IEnumerator<ICoroutineStaller>> coroutineMethod,
		T1 firstValue )
	{
		var coroutine = coroutineMethod.Invoke( firstValue );
		CoroutinesToAdd.Enqueue( coroutine );
		return coroutine;
	}

	/// <summary>
	/// Starts a new coroutine that is fetched from the <paramref name="coroutineMethod"/>.
	/// This will pass <paramref name="firstValue"/> and <paramref name="secondValue"/> to <paramref name="coroutineMethod"/>.
	/// </summary>
	/// <typeparam name="T1">The type of the first parameter to pass to the <paramref name="coroutineMethod"/>.</typeparam>
	/// <typeparam name="T2">The type of the seccond parameter to pass to the <paramref name="coroutineMethod"/>.</typeparam>
	/// <param name="coroutineMethod">The method to get the coroutine instance from.</param>
	/// <param name="firstValue">The first value to pass to the <paramref name="coroutineMethod"/>.</param>
	/// <param name="secondValue">The second value to pass to the <paramref name="coroutineMethod"/>.</param>
	/// <returns>The coroutine instance that was retrieved.</returns>
	public static IEnumerator<ICoroutineStaller> Start<T1, T2>( Func<T1, T2, IEnumerator<ICoroutineStaller>> coroutineMethod,
		T1 firstValue, T2 secondValue )
	{
		var coroutine = coroutineMethod.Invoke( firstValue, secondValue );
		CoroutinesToAdd.Enqueue( coroutine );
		return coroutine;
	}

	/// <summary>
	/// Starts a new coroutine that is fetched from the <paramref name="coroutineMethod"/>.
	/// This will pass <paramref name="firstValue"/>, <paramref name="secondValue"/> and <paramref name="thirdVlaue"/> to <paramref name="coroutineMethod"/>.
	/// </summary>
	/// <typeparam name="T1">The type of the first parameter to pass to the <paramref name="coroutineMethod"/>.</typeparam>
	/// <typeparam name="T2">The type of the seccond parameter to pass to the <paramref name="coroutineMethod"/>.</typeparam>
	/// <typeparam name="T3">The type of the third parameter to pass to the <paramref name="coroutineMethod"/>.</typeparam>
	/// <param name="coroutineMethod">The method to get the coroutine instance from.</param>
	/// <param name="firstValue">The first value to pass to the <paramref name="coroutineMethod"/>.</param>
	/// <param name="secondValue">The second value to pass to the <paramref name="coroutineMethod"/>.</param>
	/// <param name="thirdVlaue">The third value to pass to the <paramref name="coroutineMethod"/>.</param>
	/// <returns>The coroutine instance that was retrieved.</returns>
	public static IEnumerator<ICoroutineStaller> Start<T1, T2, T3>( Func<T1, T2, T3, IEnumerator<ICoroutineStaller>> coroutineMethod,
		T1 firstValue, T2 secondValue, T3 thirdVlaue )
	{
		var coroutine = coroutineMethod.Invoke( firstValue, secondValue, thirdVlaue );
		CoroutinesToAdd.Enqueue( coroutine );
		return coroutine;
	}

	/// <summary>
	/// Starts an existing instance of a coroutine.
	/// </summary>
	/// <param name="coroutine">The coroutine to start.</param>
	public static void Start( IEnumerator<ICoroutineStaller> coroutine )
	{
		CoroutinesToAdd.Enqueue( coroutine );
	}

	/// <summary>
	/// Stops an existing coroutine.
	/// </summary>
	/// <param name="coroutine">The coroutine to stop.</param>
	public static void Stop( IEnumerator<ICoroutineStaller> coroutine )
	{
		CoroutinesToRemove.Enqueue( coroutine );
	}

	/// <summary>
	/// Stops all coroutines.
	/// </summary>
	public static void StopAll()
	{
		foreach ( var existingCoroutine in Coroutines )
			CoroutinesToRemove.Enqueue( existingCoroutine.Coroutine );

		foreach ( var queuedCoroutine in CoroutinesToAdd )
			CoroutinesToRemove.Enqueue( queuedCoroutine );
	}

	/// <summary>
	/// Returns whether or not a coroutine has completed.
	/// </summary>
	/// <remarks>
	/// This will also return true if the coroutine has never been in the system.
	/// </remarks>
	/// <param name="coroutine">The coroutine to check.</param>
	/// <returns>Whether or not the coroutine has completed.</returns>
	public static bool IsComplete( IEnumerator<ICoroutineStaller> coroutine )
	{
		if ( CoroutinesToAdd.Contains( coroutine ) )
			return false;

		foreach ( var coroutineInstance in Coroutines )
		{
			if ( ReferenceEquals( coroutineInstance.Coroutine, coroutine ) )
				return false;
		}

		return true;
	}

	/// <summary>
	/// Creates a coroutine wrapper and adds it to the system if it hasn't pre-maturely finished.
	/// </summary>
	/// <param name="coroutine">The coroutine to add.</param>
	private static void AddCoroutine( IEnumerator<ICoroutineStaller> coroutine )
	{
		var coroutineInstance = new CoroutineInstance( coroutine );
		if ( !coroutineInstance.IsFinished )
			Coroutines.Add( coroutineInstance );
	}

	/// <summary>
	/// Removes a coroutine from the system.
	/// </summary>
	/// <param name="coroutine">The coroutine to remove.</param>
	private static void RemoveCoroutine( IEnumerator<ICoroutineStaller> coroutine )
	{
		CoroutineInstance? foundInstance = null;

		foreach ( var coroutineInstance in Coroutines )
		{
			if ( !ReferenceEquals( coroutineInstance.Coroutine, coroutine ) )
				continue;

			foundInstance = coroutineInstance;
			break;
		}

		if ( foundInstance is not null )
			Coroutines.Remove( foundInstance );
	}

	/// <summary>
	/// Empties all coroutine queues and updates coroutines that have the <see cref="WaitingStrategy.Tick"/> waiting strategy.
	/// </summary>
	[GameEvent.Tick]
	private static void Tick()
	{
		while ( CoroutinesToAdd.TryDequeue( out var coroutine ) )
			AddCoroutine( coroutine );

		while ( CoroutinesToRemove.TryDequeue( out var coroutine ) )
			RemoveCoroutine( coroutine );

		foreach ( var coroutineInstance in Coroutines )
		{
			if ( coroutineInstance.CurrentWaitingStrategy != WaitingStrategy.Tick )
				continue;

			coroutineInstance.Tick();
			if ( coroutineInstance.IsFinished )
				CoroutinesToRemove.Enqueue( coroutineInstance.Coroutine );
		}
	}

	/// <summary>
	/// Updates coroutines that have the <see cref="WaitingStrategy.Frame"/> waiting strategy.
	/// </summary>
	[GameEvent.Client.Frame]
	private static void Frame()
	{
		foreach ( var coroutineInstance in Coroutines )
		{
			if ( coroutineInstance.CurrentWaitingStrategy != WaitingStrategy.Frame )
				continue;

			coroutineInstance.Tick();
			if ( coroutineInstance.IsFinished )
				CoroutinesToRemove.Enqueue( coroutineInstance.Coroutine );
		}
	}
}
