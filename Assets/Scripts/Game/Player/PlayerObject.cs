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

		public StemCutter StemCutter => stemCutter;

		//-------------------------------------------------------------------
		/* Events */
		/// <summary> カット時のイベント </summary>
		public event System.Action OnCutTree;

		/// <summary> 方向転換時のイベント </summary>
		public event System.Action<Transform, Directions> OnChangeDirection;

		/// <summary> タップ時のイベント </summary>
		public event System.Action OnTapped;

		public event System.Action OnGameOvered;

		void Awake()
		{
			Direction = Directions.Right;

			// ゲームオーバー時に当たり判定を無効化
			gameManager.OnGameOvered += () =>
			{
				hitChecker.SetColliderEnabled(false);

				OnGameOvered?.Invoke();
			};

			// カット処理
			inputManager.OnClick += (direction, isChangedDirection) =>
			{
				Direction = direction;

				// 以前と方向が違う場合は、方向転換
				if (isChangedDirection)
				{
					SetSpriteDirection(Direction);
					OnChangeDirection?.Invoke(transform, Direction);
				}

				// 以前と方向が同じ場合は、カット
				else
				{
					OnCutTree?.Invoke();
					abandonedChecker.ResetAbandonedTimer();
				}

				// タップイベント
				OnTapped?.Invoke();
			};
		}

		//-------------------------------------------------------------------
		/* Methods */
		void SetSpriteDirection(Directions direction)
		{
			// スプライトの向きを変更
			var scale = transform.localScale;
			scale.x = direction == Directions.Left ? -1 : 1;
			transform.localScale = scale;
		}
	}
}
