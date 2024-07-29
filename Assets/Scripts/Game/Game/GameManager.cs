using System.Collections;
using System.Collections.Generic;
using Game.Player;
using Game.Tree;
using UnityEngine;

using Template.Utils;
using Game.State;

namespace Game
{
	/// <summary> ゲーム管理クラス </summary>
	public class GameManager : MonoBehaviour
	{
		/* Fields */
		[Header("Components")]
		[SerializeField] GameStateController stateController;

		[Header("References")]
		[SerializeField] PlayerObject player;

		List<IGameOverChecker> gameOverCheckers = new List<IGameOverChecker>();

		//-------------------------------------------------------------------
		/* Properties */
		public static bool IsGameOver { get; private set; }
		public static bool IsGamePaused { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		public event System.Action OnGameOvered;

		void Awake()
		{
			stateController.Initialize();

			IsGameOver = false;
			IsGamePaused = false;

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
					stateController.TransitionState<GameOverState>();
					OnGameOvered?.Invoke();
					break;
				}
			}
		}

		//------------------------------------------------------------

		public void PauseGame(bool isPause)
		{
			Time.timeScale = isPause ? 0 : 1;
			IsGamePaused = isPause;

			if (isPause)
			{
				stateController.TransitionState<GamePauseState>();
			}
			else
			{
				stateController.TransitionState<GameMainState>();
			}
		}
	}
}
