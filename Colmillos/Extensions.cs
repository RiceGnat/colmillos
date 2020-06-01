using System;
using System.Collections.Generic;

namespace Colmillos
{
	public static class Extensions
	{
		public static T LastOrDefault<T>(this IList<T> list) => list.Count > 0 ? list[list.Count - 1] : default;

		public static void UseProperty<T>(this IProperties properties, Enum key, Action<T> func) => func(properties.GetProperty<T>(key));
	}
}
