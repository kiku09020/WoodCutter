using System.Collections;
using System.Collections.Generic;
using Game.Score;
using Game.Tree.Branch.Item;
using UnityEngine;

namespace Game.Player
{
	/// <summary> プレイヤーと枝アイテムとの当たり判定処理 </summary>
	public class PlayerHitChecker : MonoBehaviour, IGameOverChecker
	{
		/* Fields */
		[SerializeField] ScoreController scoreController;

		bool isGameOvered = false;
		public bool CheckGameOvered() => isGameOvered;

		Collider2D col;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			col = GetComponent<Collider2D>();
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			// スコアアイテムの場合、スコアを加算
			if (other.TryGetComponent(out IScoreItem scoreItem))
			{
				scoreItem.GetItem();
				scoreController.AddScore(scoreItem.Score);
			}

			else if (other.TryGetComponent(out IDeadlyItem deadlyItem))
			{
				deadlyItem.GetItem();
				// ゲームオーバー処理
				isGameOvered = true;
			}
		}

		//-------------------------------------------------------------------
		/* Methods */
		public void SetColliderEnabled(bool isEnabled)
		{
			col.enabled = isEnabled;
		}

	}
}
