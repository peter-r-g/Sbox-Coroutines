using Sandbox;
using System;
using System.Collections.Generic;

namespace Coroutines;

/// <summary>
/// A container for coroutines. All calls in this class are required to be done within the main thread.
/// </summary>
public sealed class CoroutineBucket
{
	/// <summary>
	/// A list of all coroutines that have been added to the bucket.
	/// </summary>
	private List<IEnumerator<ICoroutineStaller>> Coroutines { get; } = new();

	/// <summary>
	/// Starts a new coroutine that is fetched from the <paramref name="coroutineMethod"/>.
	/// </summary>
	/// <param name="coroutineMethod">The method to get the coroutine instance from.</param>
	/// <returns>The coroutine instance that was retrieved.</returns>
	public IEnumerator<ICoroutineStaller> Start( Func<IEnumerator<ICoroutineStaller>> coroutineMethod )
	{
		ThreadSafe.AssertIsMainThread();

		var coroutine = Coroutine.Start( coroutineMethod );
		Coroutines.Add( coroutine );
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
	public IEnumerator<ICoroutineStaller> Start<T1>( Func<T1, IEnumerator<ICoroutineStaller>> coroutineMethod,
		T1 firstValue )
	{
		ThreadSafe.AssertIsMainThread();

		var coroutine = Coroutine.Start( coroutineMethod, firstValue );
		Coroutines.Add( coroutine );
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
	public IEnumerator<ICoroutineStaller> Start<T1, T2>( Func<T1, T2, IEnumerator<ICoroutineStaller>> coroutineMethod,
		T1 firstValue, T2 secondValue )
	{
		ThreadSafe.AssertIsMainThread();

		var coroutine = Coroutine.Start( coroutineMethod, firstValue, secondValue );
		Coroutines.Add( coroutine );
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
	public IEnumerator<ICoroutineStaller> Start<T1, T2, T3>( Func<T1, T2, T3, IEnumerator<ICoroutineStaller>> coroutineMethod,
		T1 firstValue, T2 secondValue, T3 thirdVlaue )
	{
		ThreadSafe.AssertIsMainThread();

		var coroutine = Coroutine.Start( coroutineMethod, firstValue, secondValue, thirdVlaue );
		Coroutines.Add( coroutine );
		return coroutine;
	}

	/// <summary>
	/// Starts an existing instance of a coroutine.
	/// </summary>
	/// <param name="coroutine">The coroutine to start.</param>
	public void Start( IEnumerator<ICoroutineStaller> coroutine )
	{
		ThreadSafe.AssertIsMainThread();

		Coroutine.Start( coroutine );
		Coroutines.Add( coroutine );
	}

	/// <summary>
	/// Stops an existing coroutine.
	/// </summary>
	/// <param name="coroutine">The coroutine to stop.</param>
	public void Stop( IEnumerator<ICoroutineStaller> coroutine )
	{
		ThreadSafe.AssertIsMainThread();

		Coroutine.Stop( coroutine );
		Coroutines.Remove( coroutine );
	}

	/// <summary>
	/// Stops all coroutines in the bucket.
	/// </summary>
	public void StopAll()
	{
		ThreadSafe.AssertIsMainThread();

		foreach ( var coroutine in Coroutines )
			Coroutine.Stop( coroutine );

		Coroutines.Clear();
	}
}
