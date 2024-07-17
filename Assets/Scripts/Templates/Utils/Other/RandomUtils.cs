using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template.Utils
{
	/// <summary> ランダム汎用クラス </summary>
	public static class RandomUtils
	{
		/// <summary> boolをランダムで取得する </summary>
		public static bool GetRandomBool() { return Random.Range(0, 2) == 0; }

		/// <summary> 0か1をランダムで取得する </summary>
		public static int GetRandom01() { return Random.Range(0, 2); }

		/// <summary> -1か1をランダムで取得する </summary>
		public static int GetRandomSign() { return Random.Range(0, 2) == 0 ? -1 : 1; }
	}
}
