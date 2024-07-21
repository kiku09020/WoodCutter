using System.Collections;
using System.Collections.Generic;
using Game.Tree;
using UnityEngine;

namespace Game.Player
{
	/// <summary> プレイヤー </summary>
	public class PlayerObject : MonoBehaviour
	{
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

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				// 方向転換
				SetDirection();

				// カット
				OnCutTree?.Invoke(stemCutter);
			}
		}

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> プレイヤーの左右位置を変更 </summary>
		void SetDirection()
		{
			// 画面半分で方向を変える
			if (Input.mousePosition.x < Screen.width / 2)
			{
				Direction = Directions.Left;
			}
			else
			{
				Direction = Directions.Right;
			}

			// 方向転換時
			if (prevDirection != Direction)
			{
				OnChangeDirection?.Invoke(transform, Direction);
			}

			// 以前の方向を保存
			prevDirection = Direction;
		}
	}
}
