using System;
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
		bool HasBranchItem => branchItem != null;

		//-------------------------------------------------------------------
		/* Events */

		// FIXME: 破棄されていない状態のオブジェクトが生成されてしまう
		// 生成時にアイテム自身の破棄フラグがfalseになってしまう
		// イベントで、破棄されたタイミングでbranchObjectの参照を削除する

		public override void SetPooledObject(IDisposable disposable)
		{
			base.SetPooledObject(disposable);

			branchItem = null;
		}

		public override void Dispose()
		{
			base.Dispose();

			// 枝アイテムがなければスキップ
			if (!HasBranchItem) return;

			// 落下中でなければ破棄
			if (!branchItem.IsFalling)
			{
				branchItem.Dispose();
			}

			// 枝アイテムの参照を削除
			branchItem = null;
		}

		//-------------------------------------------------------------------
		/* Methods */
		public void SetBranchItem(BranchItem branchItem)
		{
			if (branchItem == null) return;

			branchItem.transform.SetParent(transform);
			branchItem.transform.localPosition = Vector3.down;
			branchItem.transform.rotation = Quaternion.identity;
			branchItem.transform.localScale = Vector3.one;

			branchItem.OnDisposed += () =>
			{
				this.branchItem = null;
			};

			this.branchItem = branchItem;
		}

		public void FallBranchItem()
		{
			if (!HasBranchItem) return;
			var isFell = branchItem.Fall();

			if (isFell)
			{
				branchItem = null;
			}
		}


	}
}
