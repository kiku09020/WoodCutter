using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Tree.Stem;
using UnityEngine;

namespace Game.Tree.Branch
{
	public class StemAnimator : MonoBehaviour
	{
		/* Fields */
		[Header("Components")]
		[SerializeField] StemObject stemObject;

		[Header("Parameters")]
		[SerializeField] Vector2 jumpEndPos;
		[SerializeField] float jumpPower = 1;
		[SerializeField] float jumpDuration = 1;
		[SerializeField] Ease jumpEase;
		[SerializeField] float randJumpMinPower = 0.8f;
		[SerializeField] float randJumpMaxPower = 1.2f;


		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			stemObject.OnCut += OnDisposed;
		}

		//-------------------------------------------------------------------
		/* Methods */
		void OnDisposed(Directions direction, System.Action completed)
		{
			var directionSign = direction == Directions.Left ? 1 : -1;
			var fixedJumpEndPos = new Vector3(jumpEndPos.x * directionSign, jumpEndPos.y, 0);

			var randomJumpPower = Random.Range(randJumpMinPower, randJumpMaxPower) * jumpPower;

			var jumpTween = transform.DOLocalJump(fixedJumpEndPos, randomJumpPower, 1, jumpDuration)
				.SetEase(jumpEase)
				.OnComplete(() =>
				{
					completed();
				});

			var rotationTween = transform.DORotate(new Vector3(0, 0, 360 * directionSign), jumpDuration, RotateMode.FastBeyond360);

			DOTween.Sequence()
				.Append(jumpTween)
				.Join(rotationTween);
		}

	}
}
