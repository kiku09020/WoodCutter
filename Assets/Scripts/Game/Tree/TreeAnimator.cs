using DG.Tweening;
using UnityEngine;

namespace Game.Tree
{
	public class TreeAnimator : MonoBehaviour
	{
		/* Fields */
		[Header("Components")]
		[SerializeField] TreeObject treeObject;
		[SerializeField] InputAbandonedChecker abandonedChecker;

		[Header("Shake Animation")]
		[SerializeField] float shakeDuration = 1;

		[Header("Fall Animation")]
		[SerializeField] float fallDuration = 1;
		[SerializeField] Ease fallEase;

		Tween shakeTween;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			abandonedChecker.OnAbandonedCaution += (cautionTime) =>
			{
				PlayTreeShakeAnimation();
			};

			abandonedChecker.OnAbandoned += (direction) =>
			{
				PlayTreeFallAnimation(direction);
			};
		}

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> 木を揺らすアニメーション </summary>
		void PlayTreeShakeAnimation()
		{
			shakeTween = treeObject.transform.DOShakePosition(1, 0.1f, 10, 90)
				.SetLoops(-1, LoopType.Incremental);
		}

		/// <summary> 木倒すアニメーション </summary>
		void PlayTreeFallAnimation(Directions direction)
		{
			shakeTween.Kill();

			// TODO: プレイヤーの方向に合わせて倒す
			var fallRotation = direction == Directions.Left ? 90 : -90;

			treeObject.transform.DORotate(new Vector3(0, 0, fallRotation), fallDuration)
				.SetEase(fallEase);
		}
	}
}
