using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tree.Stem
{
	/// <summary> 幹オブジェクト </summary>
	public class StemObject : MonoBehaviour
	{
		/* Fields */
		bool hasBranch = false;
		BranchObject branch;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> 枝をセット </summary>
		public void SetBranch(BranchObject branch)
		{
			hasBranch = true;
			this.branch = branch;
		}

		/// <summary> 幹を切る </summary>
		public void CutStem()
		{
			Destroy(gameObject);

			if (hasBranch)
			{
				Destroy(branch.gameObject);
			}
		}

	}
}
