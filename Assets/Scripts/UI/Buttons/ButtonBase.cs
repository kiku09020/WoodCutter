using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Buttons
{
	/// <summary> ボタン基底クラス </summary>
	public class ButtonBase : MonoBehaviour
	{
		/* Fields */
		Button button;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		public event System.Action OnClick;

		protected virtual void Awake()
		{
			button = GetComponent<Button>();
			button.onClick.AddListener(() => OnClick?.Invoke());
		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
