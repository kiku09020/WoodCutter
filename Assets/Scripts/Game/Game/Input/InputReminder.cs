using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game
{
	/// <summary> 入力催促UI操作クラス </summary>
	public class InputReminder : MonoBehaviour
	{
		/* Fields */
		[Header("Components")]
		[SerializeField] InputManager inputManager;

		[Header("Settings")]
		[SerializeField] float fadeDuration = .25f;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			var canvasGroup = GetComponent<CanvasGroup>();

			inputManager.OnClickFirst += () =>
			{
				canvasGroup.DOFade(0, fadeDuration)
					.OnComplete(() => gameObject.SetActive(false));
			};
		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
