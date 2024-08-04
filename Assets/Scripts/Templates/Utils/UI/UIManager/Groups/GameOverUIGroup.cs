using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Template.Utils.UI.UIManager;
using UnityEngine;

namespace Game.UI
{
	public class GameOverUIGroup : UIGroup
	{
		/* Fields */
		[SerializeField] float scalingDuration = 0.5f;
		[SerializeField] float scalingDelay = 1f;
		[SerializeField] Ease scalingEase;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		public override void Show()
		{
			base.Show();

			transform.localScale = Vector3.zero;
			transform.DOScale(Vector3.one, scalingDuration)
				.SetDelay(scalingDelay)
				.SetEase(scalingEase);
		}

		//-------------------------------------------------------------------
		/* Methods */


		//-------------------------------------------------------------------
		/* Methods */

	}
}
