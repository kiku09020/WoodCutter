using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template.Utils
{
	/// <summary> 数値フォーマット文字列汎用クラス </summary>
	public static class StringFormatUtils
	{
		/// <summary> 0埋め </summary>
		public static string ZeroPadding(this int value, int digit)
		{
			return value.ToString($"D{digit}");
		}

		/// <summary> カンマ区切り </summary>
		public static string CommaSeparated<T>(this T value) where T : struct, System.IEquatable<T>, System.IComparable
		{
			return string.Format("#,0", value);
		}

		/// <summary> パーセント表示 </summary>
		public static string Percentages<T>(this T value, int digit) where T : struct, System.IEquatable<T>, System.IComparable
		{
			return string.Format($"P{digit}", value);
		}
	}
}
