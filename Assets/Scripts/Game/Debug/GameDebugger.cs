using System.Collections;
using System.Collections.Generic;
using Template.Utils.SceneManagement;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;

namespace Debugger
{
	public class GameDebugger : MonoBehaviour
	{
		/* Fields */
		[Header("UI")]
		[SerializeField] TextMeshProUGUI frameRateText;

		[Header("Settings")]
		[SerializeField] float updateFrameRateInterval = 1f;

		float frameRateTimer;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			if (!Debug.isDebugBuild)
			{
				gameObject.SetActive(false);
			}

			SetFrameRateText();
		}

		async void Update()
		{
			// Ctrl + Rでシーンリロード
			if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
			{
				await SceneManagerUtils.ReloadSceneAsync();
			}

			UpdateFrameRateText();
		}

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> FPSテキスト設定 </summary>
		void SetFrameRateText()
		{
			frameRateText.text = $"FPS: {Time.timeScale / Time.deltaTime}";
		}

		/// <summary> FPSテキスト更新 </summary>
		void UpdateFrameRateText()
		{
			frameRateTimer += Time.deltaTime;

			if (frameRateTimer >= updateFrameRateInterval)
			{
				frameRateTimer = 0;
				SetFrameRateText();
			}
		}

	}
}
