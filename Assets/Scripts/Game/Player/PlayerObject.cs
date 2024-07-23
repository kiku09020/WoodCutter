using System.Collections;
using System.Collections.Generic;
using Game.Score;
using Game.Tree;
using UnityEngine;

namespace Game.Player
{
	/// <summary> プレイヤー </summary>
	public class PlayerObject : MonoBehaviour
	{
		[SerializeField] InputManager inputManager;

		/* Fields */
		Directions prevDirection;
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
			prevDirection = Direction;

			inputManager.OnClick += (direction) =>
			{
				// 方向転換時はカットしない
				var isChangedDirection = SetDirection(direction);
				if (!isChangedDirection)
				{
					// カット
					OnCutTree?.Invoke(stemCutter);
				}

				// タップイベント
				OnTapped?.Invoke();
			};
		}

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> プレイヤーの左右位置を変更 </summary>
		bool SetDirection(Directions direction)
		{
			var isChangedDirection = false;

			Direction = direction;

			// 方向転換時
			if (prevDirection != Direction)
			{
				OnChangeDirection?.Invoke(transform, Direction);
				isChangedDirection = true;
			}

			// 以前の方向を保存
			prevDirection = Direction;

			return isChangedDirection;
		}
	}
}
