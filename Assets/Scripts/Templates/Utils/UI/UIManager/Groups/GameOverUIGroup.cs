using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Score;
using Template.Utils.UI.UIManager;
using UnityEngine;

namespace Game.UI
{
	public class GameOverUIGroup : UIGroup
	{
		/* Fields */
		[Header("Components")]
		[SerializeField] GameOverScoreView scoreView;

		[Header("Animations")]
		[SerializeField] float scalingDuration = 0.5f;
		[SerializeField] float scalingDelay = 1f;
		[SerializeField] Ease scalingEase;

		[SerializeField] float scoreTextAnimDelay = 0.5f;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		public override void Show()
		{
			base.Show();

			scoreView.SetScoreTexts();
			PlayScalingAnimation(transform);
			PlayScalingAnimation(scoreView.transform, scoreTextAnimDelay);
		}

		//-------------------------------------------------------------------
		/* Methods */
		void PlayScalingAnimation(Transform transform, float addedDelay = 0)
		{
			transform.localScale = Vector3.zero;
			transform.DOScale(Vector3.one, scalingDuration)
				.SetDelay(scalingDelay + addedDelay)
				.SetEase(scalingEase);
		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
