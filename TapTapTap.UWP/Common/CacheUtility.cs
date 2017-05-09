using System;
using System.Collections.Generic;

namespace SM.Common
{
	public static class CacheUtility
	{
		public static T FindInCache<T, U>(Dictionary<U, T> cache, U id)
		{
			T result = default(T);

			cache.TryGetValue(id, out result);

			return result;
		}

		public static T FindInCacheOrAdd<T, U>(Dictionary<U, T> cache, U id, Func<T> constructor)
		{
			T result;

			if (!cache.TryGetValue(id, out result))
			{
				result = constructor.Invoke();

				// TODO - possibly restrict null values from the cache (the "constructor" method may not always return an object)

				cache.Add(id, result);
			}

			return result;
		}
	}
}