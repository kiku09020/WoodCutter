using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	/// <summary> プレイヤーの操作放置管理クラス </summary>
	public class InputAbandonedChecker : MonoBehaviour, IGameOverChecker
	{
		/* Fields */
		[SerializeField] InputManager inputManager;

		[Header("Properties")]
		[SerializeField] float maxAbandonedTime = 5f;
		[SerializeField] float abandonCautionRate = 0.6f;

		bool isAbandonCaution = false;
		bool isAbandoned = false;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			inputManager.OnUpdateLastClickedTimer += CheckAbandoned;
		}

		public event System.Action<float> OnAbandonedCaution;
		public event System.Action<Directions> OnAbandoned;
		public event System.Action OnResetAbandonedTimer;

		//-------------------------------------------------------------------
		/* Methods */
		void CheckAbandoned(float lastClickedTimer)
		{
			if (isAbandoned) return;

			// 操作放置時の処理
			var cautionTime = maxAbandonedTime * abandonCautionRate;
			if (lastClickedTimer > cautionTime && !isAbandonCaution)
			{
				Debug.Log("操作放置半分");
				OnAbandonedCaution?.Invoke(cautionTime);
				isAbandonCaution = true;
			}

			// 操作放置時の処理
			if (lastClickedTimer > maxAbandonedTime)
			{
				Debug.Log("操作放置");
				OnAbandoned?.Invoke(inputManager.Direction);

				isAbandoned = true;
			}
		}

		public void ResetAbandonedTimer()
		{
			// 操作放置タイマーリセット
			OnResetAbandonedTimer?.Invoke();
			isAbandoned = false;
			isAbandonCaution = false;
		}

		//------------------------------------------------------------

		public bool CheckGameOvered() => isAbandoned;

	}
}
