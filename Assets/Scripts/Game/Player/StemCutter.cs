using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
		public StemObject CutStem(Queue<StemObject> stems, Directions direction)
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
			cutStem.CutStem(direction);

			foreach (var stem in stems)
			{
				stem.PlayCuttingMoveAnimation();
			}

			return cutStem;
		}

	}
}
