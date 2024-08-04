using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game
{
	/// <summary> 操作催促UI </summary>
	public class InputFinger : MonoBehaviour
	{
		[SerializeField] RectTransform rectTransform;

		[Header("Settings")]
		[SerializeField] Vector3 moveEndPos = new Vector3(-3, 3);
		[SerializeField] float moveDuration = .5f;
		[SerializeField] Ease moveEase;
		[SerializeField] float loopDelay = .25f;

		void Awake()
		{
			var moveTween = rectTransform.DOLocalMove(moveEndPos, moveDuration)
				.SetEase(moveEase)
				.SetLoops(-1, LoopType.Yoyo)
				.SetRelative(true);

			DOTween.Sequence()
				.Append(moveTween)
				.SetDelay(loopDelay);
		}
	}
}
