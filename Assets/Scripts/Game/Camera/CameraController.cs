using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Player;
using UnityEngine;

namespace Game
{
	/// <summary> カメラ操作クラス </summary>
	public class CameraController : MonoBehaviour
	{
		/* Fields */
		[Header("Properties")]
		[SerializeField] float zoomOutTime = 0.5f;
		[SerializeField] float targetOrthoSize = 3;
		[SerializeField] Vector3 offset = new Vector3(-3, -3, -10);

		Camera mainCamera;
		Vector3 startPosition;
		float startOrthoSize;

		Tween zoomTween;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			mainCamera = Camera.main;
			startPosition = transform.position;
			startOrthoSize = mainCamera.orthographicSize;
		}

		//-------------------------------------------------------------------
		/* Methods */
		public void ZoomToPlayer(PlayerObject player, float duration)
		{
			var zoomTween = DOTween.Sequence()
				.Append(transform.DOMove(player.transform.position + offset, duration))
				.Join(mainCamera.DOOrthoSize(targetOrthoSize, duration));
		}

		public void ResetZoom()
		{
			zoomTween?.Kill();

			DOTween.Sequence()
				.Append(transform.DOMove(startPosition, zoomOutTime))
				.Join(mainCamera.DOOrthoSize(startOrthoSize, zoomOutTime));
		}
	}
}
