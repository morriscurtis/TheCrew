using System.Runtime.InteropServices;

namespace MoCrew;

public static class Util
{
	public static void SwapRemoveAt<T>(this List<T> list, int index)
	{
		int lastIndex = list.Count - 1;
		list[index] = list[lastIndex];
		list.RemoveAt(lastIndex);
	}

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

	public static void Shuffle<T>(this List<T> list)
		=> CollectionsMarshal.AsSpan(list).Shuffle();
}
