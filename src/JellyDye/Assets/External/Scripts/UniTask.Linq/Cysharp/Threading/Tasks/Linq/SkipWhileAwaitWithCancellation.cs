using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
	internal sealed class SkipWhileAwaitWithCancellation<TSource> : IUniTaskAsyncEnumerable<TSource>
	{
		private class _SkipWhileAwaitWithCancellation : AsyncEnumeratorAwaitSelectorBase<TSource, TSource, bool>
		{
			private Func<TSource, CancellationToken, UniTask<bool>> predicate;

			public _SkipWhileAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<bool>> predicate, CancellationToken cancellationToken)
			{
				((AsyncEnumeratorAwaitSelectorBase<, , >)(object)this)._002Ector((IUniTaskAsyncEnumerable<TSource>)null, default(CancellationToken));
			}

			protected override UniTask<bool> TransformAsync(TSource sourceCurrent)
			{
				return default(UniTask<bool>);
			}

			protected override bool TrySetCurrentCore(bool awaitResult, out bool terminateIteration)
			{
				terminateIteration = default(bool);
				return false;
			}
		}

		private readonly IUniTaskAsyncEnumerable<TSource> source;

		private readonly Func<TSource, CancellationToken, UniTask<bool>> predicate;

		public SkipWhileAwaitWithCancellation(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UniTask<bool>> predicate)
		{
		}

		public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}
	}
}
