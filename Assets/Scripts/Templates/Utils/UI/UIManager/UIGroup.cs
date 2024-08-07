﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template.Utils.UI.UIManager
{
	public class UIGroup : MonoBehaviour
	{
		/// <summary> UIの初期化 </summary>
		public virtual void Initialize() { }

		/// <summary> UIを非表示にする </summary>
		public virtual void Hide()
		{
			transform.parent.gameObject.SetActive(false);
		}

		/// <summary> UIを表示する </summary>
		public virtual void Show()
		{
			transform.parent.gameObject.SetActive(true);
		}

		void OnValidate()
		{
			gameObject.name = GetType().Name;
		}
	}
}
