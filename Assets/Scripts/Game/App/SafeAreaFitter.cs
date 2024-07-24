using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
	/// <summary> UIをセーフエリアに合わせる操作するクラス </summary>
	[ExecuteAlways, RequireComponent(typeof(RectTransform))]
	public class SafeAreaFitter : MonoBehaviour
	{
		/* Fields */
		RectTransform rectTransform;

		Vector3 prevLocalPosition;
		Rect prevSafeArea;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
		}

		void Update()
		{
			if (rectTransform == null) return;

			// モバイルデバイス以外の場合は処理を終了
			if (UnityEngine.Device.SystemInfo.deviceType != DeviceType.Handheld)
			{
				rectTransform.anchorMin = Vector2.zero;
				rectTransform.anchorMax = Vector2.one;

				return;
			}

			// 前フレームの値と異なる場合、セーフエリアを更新
			if (prevLocalPosition != rectTransform.localPosition
				|| prevSafeArea != Screen.safeArea)
			{
				SetAnchorWithSafeArea();
			}
		}

		void OnEnable()
		{
			SetAnchorWithSafeArea();
		}

		void OnDisable()
		{
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
		}

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> セーフエリアに伴って、アンカーを調整する </summary>
		void SetAnchorWithSafeArea()
		{
			var safeArea = Screen.safeArea;
			var resoultion = Screen.currentResolution;

			// セーフエリア(0-1) / 画面サイズ(1080など) = アンカーサイズ(0-1)
			var anchorMin = new Vector2(safeArea.xMin / resoultion.width, safeArea.yMin / resoultion.height);
			var anchorMax = new Vector2(safeArea.xMax / resoultion.width, safeArea.yMax / resoultion.height);

			rectTransform.sizeDelta = Vector2.zero;
			rectTransform.anchorMin = anchorMin;
			rectTransform.anchorMax = anchorMax;

			// 前フレームの値を更新
			prevSafeArea = safeArea;
			prevLocalPosition = rectTransform.localPosition;
		}
	}
}
