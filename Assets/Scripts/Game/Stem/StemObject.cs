using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tree.Stem
{
	/// <summary> 幹オブジェクト </summary>
	public class StemObject : MonoBehaviour
	{
		/* Fields */
		BranchObject branch;

		//-------------------------------------------------------------------
		/* Properties */
		public bool HasBranch { get; private set; }
		public Directions BranchDirection { get; private set; }

		//-------------------------------------------------------------------
		/* Events */

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> 枝をセット </summary>
		public void SetBranch(BranchObject branch, Directions direction)
		{
			HasBranch = true;
			this.branch = branch;
			BranchDirection = direction;
		}

		/// <summary> 幹を切る </summary>
		public void CutStem()
		{
			Destroy(gameObject);

			if (HasBranch)
			{
				Destroy(branch.gameObject);
			}
		}

	}
}
