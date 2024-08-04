using UnityEngine;

namespace Game
{
	public class InputManager : MonoBehaviour
	{
		/* Fields */
		[Header("TapArea")]
		[SerializeField, Tooltip("ヘッダーのタップ領域外高さの割合")]
		float headerThreshold = 0.25f;
		[SerializeField, Tooltip("フッターのタップ領域外高さの割合")]
		float footerThreshold = 0.1f;

		[Header("Debug")]
		[SerializeField] bool allowRightClick = false;

		public bool IsFirstClicked { get; private set; }
		Directions prevDirection = Directions.Right;

		//-------------------------------------------------------------------
		/* Properties */
		/// <summary> 最後にクリックしてからの経過時間 </summary>
		public float LastClickedTimer { get; private set; }

		public Directions Direction { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		/// <summary> クリック時の方向を返す </summary>
		public event System.Action<Directions, bool> OnClick;

		public event System.Action OnClickFirst;

		public event System.Action<float> OnUpdateLastClickedTimer;

		void Update()
		{
			// ゲームオーバー時は入力を受け付けない
			if (GameManager.IsGameOver || GameManager.IsGamePaused) return;

			if (Input.GetMouseButtonDown(0) ||
				(allowRightClick && Input.GetMouseButtonDown(1)))       // 右クリック許可
			{
				// クリックエリア外判定
				if (!CheckClickArea()) return;

				// 左右方向取得
				Direction = GetDirection();
				var isChangedDirection = Direction != prevDirection;

				// クリックイベント
				OnClick?.Invoke(Direction, isChangedDirection);

				// 方向が同じ場合のみタイマーをリセット
				if (!isChangedDirection)
				{
					LastClickedTimer = 0;
				}

				prevDirection = Direction;

				// 初回クリック
				if (!IsFirstClicked)
				{
					IsFirstClicked = true;
					OnClickFirst?.Invoke();
				}
			}

			// 初回クリックしていない場合は、タイマーを更新しない
			if (!IsFirstClicked) return;
			LastClickedTimer += Time.deltaTime;
			OnUpdateLastClickedTimer?.Invoke(LastClickedTimer);
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
