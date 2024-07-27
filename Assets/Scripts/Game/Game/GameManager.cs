using System.Collections;
using System.Collections.Generic;
using Game.Player;
using Game.Tree;
using UnityEngine;

using Template.Utils;

namespace Game
{
	/// <summary> ゲーム管理クラス </summary>
	public class GameManager : MonoBehaviour
	{
		/* Fields */
		[SerializeField] PlayerObject player;

		List<IGameOverChecker> gameOverCheckers = new List<IGameOverChecker>();

		//-------------------------------------------------------------------
		/* Properties */
		public static bool IsGameOver { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		public event System.Action OnGameOvered;

		void Awake()
		{
			IsGameOver = false;

			// ゲームオーバーチェッカー取得
			gameOverCheckers.AddRange(ObjectUtils.FindObjectsByInterface<IGameOverChecker>());
		}

		void FixedUpdate()
		{
			if (IsGameOver) return;

			CheckGameOver();
		}

		//-------------------------------------------------------------------
		/* Methods */
		void CheckGameOver()
		{
			foreach (var checker in gameOverCheckers)
			{
				if (checker.CheckGameOvered())
				{
					IsGameOver = true;
					OnGameOvered?.Invoke();
					break;
				}
			}
		}
	}
}
