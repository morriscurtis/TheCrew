using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MoCrew;

public static class Util
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReadOnlySpan<T> AsReadOnlySpan<T>(this List<T> list)
		=> CollectionsMarshal.AsSpan(list);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Span<T> AsSpan<T>(this List<T> list)
		=> CollectionsMarshal.AsSpan(list);

	public static void SwapRemoveAt<T>(this List<T> list, int index)
	{
		int lastIndex = list.Count - 1;
		list[index] = list[lastIndex];
		list.RemoveAt(lastIndex);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Swap<T>(this List<T> list, int a, int b)
		=> (list[a], list[b]) = (list[b], list[a]);

	public static void Shuffle<T>(this Span<T> span)
	{
		Random rng = Random.Shared;
		for (int n = span.Length; n >= 2; --n)
		{
			int i = rng.Next(n);
			int j = n - 1;
			(span[j], span[i]) = (span[i], span[j]);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Shuffle<T>(this List<T> list)
		=> CollectionsMarshal.AsSpan(list).Shuffle();
}
