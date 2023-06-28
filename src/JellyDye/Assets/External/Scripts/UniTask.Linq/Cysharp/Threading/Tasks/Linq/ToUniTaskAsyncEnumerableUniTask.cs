using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
	internal class ToUniTaskAsyncEnumerableUniTask<T> : IUniTaskAsyncEnumerable<T>
	{
		private class _ToUniTaskAsyncEnumerableUniTask : IUniTaskAsyncEnumerator<T>, IUniTaskAsyncDisposable
		{
			private readonly UniTask<T> source;

			private CancellationToken cancellationToken;

			private T current;

			private bool called;

			public T Current => default(T);

			public _ToUniTaskAsyncEnumerableUniTask(UniTask<T> source, CancellationToken cancellationToken)
			{
			}

			public UniTask<bool> MoveNextAsync()
			{
				return default(UniTask<bool>);
			}

			public UniTask DisposeAsync()
			{
				return default(UniTask);
			}
		}

		private readonly UniTask<T> source;

		public ToUniTaskAsyncEnumerableUniTask(UniTask<T> source)
		{
		}

		public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}
	}
}
