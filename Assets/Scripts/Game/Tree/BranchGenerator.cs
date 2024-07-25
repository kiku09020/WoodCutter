using System.Collections;
using System.Collections.Generic;
using Template.DesignPatterns.ObjectPool;
using UnityEngine;

namespace Game.Tree.Stem
{
	/// <summary> 枝生成クラス </summary>
	public class BranchGenerator : PooledMonoBehaviourObjectManager<BranchObject>
	{
		/* Fields */
		[Header("Prameters")]
		[SerializeField, Tooltip("枝の生成確率")]
		float branchProb = 0.3f;
		[SerializeField, Tooltip("開始時の枝同士の最小間隔")]
		int startMinBranchDistance = 5;

		int currentBranchDistance;


		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		//-------------------------------------------------------------------
		/* Methods */
		public override void Initialize()
		{
			base.Initialize();
			currentBranchDistance = startMinBranchDistance;
		}

		/// <summary> 枝をセット </summary>
		public void GenerateBranch(StemObject stem)
		{
			// 一定間隔かつ一定確率で生成
			if (currentBranchDistance <= 0 &&
				Random.value < branchProb)
			{
				var branch = pool.GetPooledObject(Vector3.zero);

				// 左右どちらに生成するか
				var isLeftBranch = Random.value < 0.5f;
				var branchDir = isLeftBranch ? Directions.Left : Directions.Right;
				var branchPosX = isLeftBranch ? -1.0f : 1.0f;
				var branchPos = new Vector3(branchPosX, 0, 0);

				stem.SetBranch(branch, branchDir, branchPos);

				// 枝同士の間隔値を設定
				currentBranchDistance = startMinBranchDistance;
			}

			// 枝同士の間隔値を減らす
			currentBranchDistance--;
		}
	}
}
