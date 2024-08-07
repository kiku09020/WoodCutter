using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Score
{
	/// <summary> スコア表示クラス </summary>
	public class ScoreView : MonoBehaviour
	{
		/* Fields */
		[Header("Components")]
		[SerializeField] ScoreController scoreController;
		[SerializeField] ParticleSystem highScoreEffect;

		[Header("UI")]
		[SerializeField] TextMeshProUGUI scoreText;
		[SerializeField] TextMeshProUGUI highScoreText;

		[Header("Animations")]
		[SerializeField] float animationDuration = 0.5f;
		[SerializeField] float animationTargetScale = 1.2f;
		[SerializeField] Ease animationEase;

		Tween tween;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			scoreText.text = "0";

			scoreController.OnChangeScore += (score) =>
			{
				scoreText.text = score.ToString();
				AnimationScoreText(scoreText);
			};

			scoreController.OnChangeHighScore += (highScore) =>
			{
				highScoreText.text = $"BEST:{highScore}";
			};

			scoreController.OnChangeHighScoreFirst += () =>
			{
				AnimationScoreText(highScoreText);
				highScoreEffect.Play();
			};
		}

		//-------------------------------------------------------------------
		/* Methods */
		void AnimationScoreText(TextMeshProUGUI scoreText)
		{
			tween?.Complete();

			tween = scoreText.transform.DOScale(animationTargetScale, animationDuration)
				.SetEase(animationEase)
				.SetLoops(2, LoopType.Yoyo);
		}
	}
}
