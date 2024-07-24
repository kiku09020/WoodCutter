using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace App
{
	public class AppManager : MonoBehaviour
	{
		/* Fields */
		[SerializeField] TextMeshProUGUI frameRateText;
		[SerializeField] float updateFrameRateInterval = 3f;

		float frameRateTimer;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			SetupApp();

			SetFrameRateText();
		}

		void Update()
		{
			frameRateTimer += Time.deltaTime;

			if (frameRateTimer >= updateFrameRateInterval)
			{
				frameRateTimer = 0;
				SetFrameRateText();
			}
		}

		//-------------------------------------------------------------------
		/* Methods */
		void SetFrameRateText()
		{
			frameRateText.text = $"FPS: {1 / Time.deltaTime}";
		}

		void SetupApp()
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 60;
		}
	}
}
