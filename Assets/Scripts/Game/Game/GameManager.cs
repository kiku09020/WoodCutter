using System.Collections;
using System.Collections.Generic;
using Game.Player;
using Game.Tree;
using UnityEngine;

namespace Game
{
	/// <summary> ゲーム管理クラス </summary>
	public class GameManager : MonoBehaviour
	{
		/* Fields */
		[SerializeField] PlayerObject player;
		[SerializeField] TreeObject tree;

		//-------------------------------------------------------------------
		/* Properties */
		public static bool IsGameOver { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		public event System.Action OnGameOvered;

		void Awake()
		{
			IsGameOver = false;

			player.OnTapped += CheckGameOver;
		}

		//-------------------------------------------------------------------
		/* Methods */
		void CheckGameOver()
		{
			// プレイヤーの向きと木の一番下の枝の向きが同じ場合、ゲームオーバー
			if (tree.GetBottomStemBranchDirection() == player.Direction)
			{
				IsGameOver = true;
				OnGameOvered?.Invoke();
			}
		}
	}
}
