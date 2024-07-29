using System.Collections;
using System.Collections.Generic;
using Game.Score;
using Game.Tree.Branch.Item;
using Template.DesignPatterns.ObjectPool;
using Template.Utils;
using UnityEngine;

namespace Game.Tree.Stem
{
	/// <summary> 枝生成クラス </summary>
	public class BranchGenerator : PooledMonoBehaviourObjectManager<BranchObject>
	{
		/* Fields */
		[SerializeField] BranchItemHandler branchItemHandler;

		[Header("Prameters")]
		[SerializeField, Tooltip("枝の生成確率")]
		float branchProb = 0.3f;
		[SerializeField, Tooltip("開始時の枝同士の最小間隔")]
		int startMinBranchDistance = 5;
		[SerializeField, Tooltip("ランダムで変動する最小間隔")]
		int randomMinBranchDistance = 3;

		int currentBranchDistance;
		int minBranchDistance;

		Directions prevBranchDir;

		IScoreRef scoreLevelRef;


		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		//-------------------------------------------------------------------
		/* Methods */
		public override void Initialize()
		{
			base.Initialize();
			branchItemHandler.Initialize();

			minBranchDistance = startMinBranchDistance;
			currentBranchDistance = minBranchDistance;

			scoreLevelRef = ObjectUtils.FindObjectByInterface<IScoreRef>();
		}

		/// <summary> 枝をセット </summary>
		public void GenerateBranch(StemObject stem)
		{
			// 一定間隔かつ一定確率で生成
			if (currentBranchDistance <= 0 &&
				Random.value < branchProb ||
				currentBranchDistance <= -randomMinBranchDistance)
			{
				// 枝アイテムを生成
				var branchItem = branchItemHandler.Generate();

				// 枝を生成
				var branch = pool.GetPooledObject(Vector3.zero);
				branch.SetBranchItem(branchItem);

				// 左右どちらに生成するか
				var isLeftBranch = Random.value < 0.5f;
				var branchDir = isLeftBranch ? Directions.Left : Directions.Right;

				// スコアに応じて、前回の枝と同じ方向に生成する確率を下げる
				if (prevBranchDir != Directions.None &&
					prevBranchDir == branchDir &&
					Random.value < scoreLevelRef.ScoreLevelRate)
				{
					if (branchDir == Directions.Left)
					{
						branchDir = Directions.Right;
					}
					else
					{
						branchDir = Directions.Left;
					}
				}

				prevBranchDir = branchDir;

				var branchPosX = branchDir == Directions.Left ? -1.0f : 1.0f;
				var branchPos = new Vector3(branchPosX, 0, 0);

				stem.SetBranch(branch, branchDir, branchPos);

				// 枝同士の間隔値を設定
				currentBranchDistance = minBranchDistance;
			}

			// 最小間隔をスコアに応じて変更
			minBranchDistance = Mathf.Max(2, startMinBranchDistance - (int)(scoreLevelRef.ScoreLevelRate * startMinBranchDistance));

			// 枝同士の間隔値を減らす
			currentBranchDistance--;
		}
	}
}
