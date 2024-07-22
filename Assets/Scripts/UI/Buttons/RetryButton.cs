using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Buttons
{
	public class RetryButton : ButtonBase
	{
		/* Fields */
		[SerializeField] RetryController retryController;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		protected override void Awake()
		{
			OnClick += retryController.Retry;

			base.Awake();
		}

		//-------------------------------------------------------------------
		/* Methods */


		//-------------------------------------------------------------------
		/* Methods */

	}
}
