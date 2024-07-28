using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
	/// <summary> プレイヤー </summary>
	public class PlayerObject : MonoBehaviour
	{
		[Header("Components")]
		[SerializeField] PlayerHitChecker hitChecker;
		[SerializeField] InputManager inputManager;
		[SerializeField] InputAbandonedChecker abandonedChecker;
		[SerializeField] GameManager gameManager;

		/* Fields */
		StemCutter stemCutter = new();

		//-------------------------------------------------------------------
		/* Properties */
		public Directions Direction { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		/// <summary> カット時のイベント </summary>
		public event System.Action<StemCutter> OnCutTree;

		/// <summary> 方向転換時のイベント </summary>
		public event System.Action<Transform, Directions> OnChangeDirection;

		/// <summary> タップ時のイベント </summary>
		public event System.Action OnTapped;

		void Awake()
		{
			Direction = Directions.Right;

			// ゲームオーバー時に当たり判定を無効化
			gameManager.OnGameOvered += () =>
			{
				hitChecker.SetColliderEnabled(false);
			};

			// カット処理
			inputManager.OnClick += (direction, isChangedDirection) =>
			{
				Direction = direction;

				// 以前と方向が違う場合は、方向転換
				if (isChangedDirection)
				{
					OnChangeDirection?.Invoke(transform, Direction);
				}

				// 以前と方向が同じ場合は、カット
				else
				{
					OnCutTree?.Invoke(stemCutter);
					abandonedChecker.ResetAbandonedTimer();
				}

				// タップイベント
				OnTapped?.Invoke();
			};
		}

		//-------------------------------------------------------------------
		/* Methods */
	}
}
