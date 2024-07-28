using System.Collections;
using System.Collections.Generic;
using Game.Tree.Branch.Item;
using Template.DesignPatterns.ObjectPool;
using UnityEngine;

namespace Game.Tree.Stem
{
	/// <summary> 枝オブジェクト </summary>
	public class BranchObject : PooledMonoBehaviour
	{
		/* Fields */
		BranchItem branchItem;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		//-------------------------------------------------------------------
		/* Methods */
		public void SetBranchItem(BranchItem branchItem)
		{
			if (branchItem == null) return;

			branchItem.transform.SetParent(transform);
			branchItem.transform.localPosition = Vector3.zero;

			this.branchItem = branchItem;
		}

		public override void Dispose()
		{
			base.Dispose();

			if (branchItem == null) return;
			branchItem.Dispose();
			branchItem = null;
		}
	}
}
