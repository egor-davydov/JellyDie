using System.Threading;

namespace Cysharp.Threading.Tasks
{
	public static class StateExtensions
	{
		public static ReadOnlyAsyncReactiveProperty<T> ToReadOnlyAsyncReactiveProperty<T>(this IUniTaskAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			return null;
		}

		public static ReadOnlyAsyncReactiveProperty<T> ToReadOnlyAsyncReactiveProperty<T>(this IUniTaskAsyncEnumerable<T> source, T initialValue, CancellationToken cancellationToken)
		{
			return null;
		}
	}
}
