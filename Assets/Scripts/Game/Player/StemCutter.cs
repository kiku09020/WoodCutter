using System.Collections;
using System.Collections.Generic;
using Game.Tree.Stem;
using UnityEngine;

namespace Game.Player
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

			// 枝アイテム落下処理
			foreach (var stem in stems)
			{
				if (stem.HasBranch)
				{
					stem.Branch.FallBranchItem();
				}
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
