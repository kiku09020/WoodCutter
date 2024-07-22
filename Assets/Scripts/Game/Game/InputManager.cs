using UnityEngine;

namespace Game
{
	public class InputManager : MonoBehaviour
	{
		/* Fields */
		[SerializeField, Tooltip("ヘッダーのタップ領域外高さの割合")]
		float headerThreshold = 0.25f;

		[SerializeField, Tooltip("フッターのタップ領域外高さの割合")]
		float footerThreshold = 0.1f;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		/// <summary> クリック時の方向を返す </summary>
		public event System.Action<Directions> OnClick;

		void Update()
		{
			// ゲームオーバー時は入力を受け付けない
			if (GameManager.IsGameOver) return;

			if (Input.GetMouseButtonDown(0))
			{
				// クリックエリア外判定
				if (!CheckClickArea()) return;

				// 左右方向取得
				var direction = GetDirection();

				OnClick?.Invoke(direction);
			}
		}

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> ヘッダー、フッターのタップ領域外をクリックしたか </summary>
		bool CheckClickArea()
		{
			if (Input.mousePosition.y < Screen.height * footerThreshold ||
				Input.mousePosition.y > Screen.height * (1 - headerThreshold))
			{
				return false;
			}
			return true;
		}

		/// <summary> 方向変更判定 </summary>
		Directions GetDirection()
		{
			// 画面半分で方向を変える
			if (Input.mousePosition.x < Screen.width / 2)
			{
				return Directions.Left;
			}
			else
			{
				return Directions.Right;
			}
		}

	}
}
