using System.Collections;
using System.Collections.Generic;
using Game.Tree.Stem;
using UnityEngine;

namespace Game.Tree
{
	/// <summary> 幹切り処理 </summary>
	public class StemCutter
	{
		/* Fields */

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		//-------------------------------------------------------------------
		/* Methods */
		public StemObject CutStem(Queue<StemObject> stems)
		{
			// 幹がない場合はnullを返す
			if (stems.Count == 0)
			{
				return null;
			}

			var cutStem = stems.Dequeue();
			cutStem.CutStem();

			foreach (var stem in stems)
			{
				stem.transform.position += new Vector3(0, -cutStem.transform.localScale.y, 0);
			}

			return cutStem;
		}

	}
}
