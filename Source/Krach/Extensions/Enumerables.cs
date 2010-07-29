﻿// Copyright © Julian Brunner 2010

// This file is part of Krach.
//
// Krach is free software: you can redistribute it and/or modify it under the
// terms of the GNU Lesser General Public License as published by the Free
// Software Foundation, either version 3 of the License, or (at your option) any
// later version.
//
// Krach is distributed in the hope that it will be useful, but WITHOUT ANY
// WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR
// A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with
// Krach. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Krach.Extensions
{
	public static class Enumerables
	{
		public static bool ContainsAll<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			if (first == null) throw new ArgumentNullException("first");
			if (second == null) throw new ArgumentNullException("second");

			return second.All(item => first.Contains(item));
		}
		public static bool ContainsAny<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
		{
			if (first == null) throw new ArgumentNullException("first");
			if (second == null) throw new ArgumentNullException("second");

			return second.Any(item => first.Contains(item));
		}
		public static bool IsDistinct<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null) throw new ArgumentNullException("source");

			source = source.ToArray();

			return Enumerable.SequenceEqual(source, source.Distinct());
		}
		public static int GetIndex<TSource>(this IEnumerable<TSource> source, TSource item)
		{
			if (source == null) throw new ArgumentNullException("source");

			IEqualityComparer<TSource> comparer = EqualityComparer<TSource>.Default;

			int index = 0;

			foreach (TSource currentItem in source)
			{
				if (comparer.Equals(currentItem, item)) return index;

				index++;
			}

			return -1;
		}
		public static IEnumerable<TSource> Rotate<TSource>(this IEnumerable<TSource> source, int offset)
		{
			if (source == null) throw new ArgumentNullException("source");

			TSource[] buffer = source.ToArray();

			offset = offset.Modulo(buffer.Length);

			for (int i = 0; i < buffer.Length; i++) yield return buffer[(i - offset).Modulo(buffer.Length)];
		}
		public static IEnumerable<TSource> WhereNot<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null) throw new ArgumentNullException("source");

			foreach (TSource item in source)
				if (!predicate(item))
					yield return item;
		}
		public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource item)
		{
			if (source == null) throw new ArgumentNullException("source");

			return Enumerable.Concat(source, Single(item));
		}
		public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> source, TSource item)
		{
			if (source == null) throw new ArgumentNullException("source");

			return source.Except(Single(item));
		}
		public static IEnumerable<TSource> SkipLast<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null) throw new ArgumentNullException("source");

			Queue<TSource> queue = new Queue<TSource>();

			foreach (TSource item in source)
			{
				queue.Enqueue(item);

				if (queue.Count > count) yield return queue.Dequeue();
			}
		}
		public static IEnumerable<TSource> TakeLast<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null) throw new ArgumentNullException("source");

			Queue<TSource> queue = new Queue<TSource>();

			foreach (TSource item in source)
			{
				queue.Enqueue(item);

				if (queue.Count > count) queue.Dequeue();
			}

			return queue;
		}
		public static IEnumerable<TSource> Separate<TSource>(this IEnumerable<TSource> source, TSource separator)
		{
			if (source == null) throw new ArgumentNullException("source");

			bool first = true;

			foreach (TSource item in source)
			{
				if (first) first = false;
				else yield return separator;

				yield return item;
			}
		}
		public static IEnumerable<Tuple<TSource, TSource>> GetRanges<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null) throw new ArgumentNullException("source");

			IEnumerator<TSource> enumerator = source.GetEnumerator();

			if (!enumerator.MoveNext()) yield break;

			TSource last = enumerator.Current;

			while (enumerator.MoveNext()) yield return Tuple.Create(last, last = enumerator.Current);
		}
		public static IEnumerable<string> ToStrings<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null) throw new ArgumentNullException("source");

			foreach (TSource item in source) yield return item == null ? "<null>" : item.ToString();
		}
		public static string AggregateString<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null) throw new ArgumentNullException("source");

			return source.Aggregate(string.Empty, (seed, current) => seed + current);
		}
		public static IEnumerable<TSource> Concatenate<TSource>(params IEnumerable<TSource>[] sources)
		{
			if (sources == null) throw new ArgumentNullException("sources");

			foreach (IEnumerable<TSource> source in sources)
			{
				if (source == null) throw new ArgumentException("sources");

				foreach (TSource item in source)
					yield return item;
			}
		}
		public static IEnumerable<TSource> Create<TSource>(params TSource[] items)
		{
			return items;
		}
		public static IEnumerable<TSource> Consume<TSource>(Func<TSource> getItem)
		{
			while (true) yield return getItem();
		}
		public static IEnumerable<TSource> Single<TSource>(TSource item)
		{
			yield return item;
		}
	}
}