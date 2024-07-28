using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Player
{
	/// <summary> プレイヤーアニメーションクラス </summary>
	public class PlayerAnimator : MonoBehaviour
	{
		/* Fields */
		[Header("Cut Animation")]
		[SerializeField] Vector3 cutTargetPosition;
		[SerializeField] float cutRotateAngle = 45;
		[SerializeField] float cutDuration = 0.1f;
		[SerializeField] Ease cutEase;

		[Header("Dead Animation")]
		[SerializeField] float deadDuration = 0.5f;
		[SerializeField] Vector3 deadJumpPos;
		[SerializeField] float deadJumpHeight = 3;
		[SerializeField] float deadRotateAngle = 1080;
		[SerializeField] Ease deadEase;

		Tween cutTween;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			var player = GetComponentInParent<PlayerObject>();
			player.OnCutTree += PlayCutAnimation;
			player.OnGameOvered += PlayDeadAnimation;
		}

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> カットアニメーション </summary>
		public void PlayCutAnimation()
		{
			cutTween?.Complete();

			var fixedTargetPosition = new Vector3(-cutTargetPosition.x, cutTargetPosition.y, cutTargetPosition.z);
			var cutMoveTween = transform.DOLocalMove(fixedTargetPosition, cutDuration).SetEase(cutEase);

			var cutRotateTween = transform.DOLocalRotate(new Vector3(0, 0, cutRotateAngle), cutDuration);

			cutTween = DOTween.Sequence()
				.Append(cutMoveTween)
				.Join(cutRotateTween)
				.SetLoops(2, LoopType.Yoyo);
		}

		/// <summary> 死亡時アニメーション再生 </summary>
		public void PlayDeadAnimation()
		{
			cutTween?.Kill();

			var deadRotateTween = transform.DORotate(new Vector3(0, 0, deadRotateAngle), deadDuration, RotateMode.FastBeyond360);
			var deadJumpTween = transform.DOLocalJump(deadJumpPos, deadJumpHeight, 1, deadDuration).SetEase(deadEase);

			DOTween.Sequence()
				.Append(deadRotateTween)
				.Join(deadJumpTween);
		}

	}
}
