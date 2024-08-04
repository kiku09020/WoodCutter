using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Score
{
	public class GameOverScoreView : MonoBehaviour
	{
		/* Fields */
		[Header("Components")]
		[SerializeField] ScoreController scoreController;

		[Header("UI")]
		[SerializeField] TextMeshProUGUI scoreText;
		[SerializeField] TextMeshProUGUI highScoreText;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		//-------------------------------------------------------------------
		/* Methods */
		public void SetScoreTexts()
		{
			scoreText.text = $"Score:{scoreController.Score}";
			highScoreText.text = $"BEST:{scoreController.HighScore}";
		}

	}
}
