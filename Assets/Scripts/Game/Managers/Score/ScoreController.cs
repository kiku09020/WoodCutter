using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Template.Utils;

namespace Game.Score
{
	/// <summary> スコアレベル比率参照インターフェース </summary>
	public interface IScoreRef : IFoundObject
	{
		/// <summary> スコア </summary>
		int Score { get; }

		/// <summary> スコアレベル比率 </summary>
		float ScoreLevelRate { get; }
	}

	/// <summary> スコア操作クラス </summary>
	public class ScoreController : MonoBehaviour, IScoreRef
	{
		/* Fields */
		[Header("Components")]
		[SerializeField] GameManager gameManager;

		[Header("Parameters")]
		[SerializeField] int maxLevelScore = 1500;

		[Header("Debug")]
		[SerializeField] bool resetScore = false;

		public float ScoreLevelRate => (float)Score / maxLevelScore;

		bool isFirstUpdateHighScore = false;
		bool isFirstPlay = false;

		//-------------------------------------------------------------------
		/* Properties */
		public int Score { get; private set; }
		public int HighScore { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		public event System.Action<int> OnChangeScore;

		public event System.Action<int> OnChangeHighScore;

		public event System.Action OnChangeHighScoreFirst;

		void Awake()
		{
			if (resetScore)
			{
				PlayerPrefs.DeleteKey("HighScore");
			}

			// ハイスコア初期化
			HighScore = PlayerPrefs.GetInt("HighScore", 0);
			if (HighScore == 0)
			{
				isFirstPlay = true;
			}

			OnChangeHighScore?.Invoke(HighScore);

			// スコア加算時にハイスコア更新判定
			OnChangeScore += (score) =>
			{
				SetHighScore();
			};

			// ゲームオーバー時のハイスコア保存
			gameManager.OnGameOvered += () =>
			{
				SetHighScoreToData();
			};
		}

		//-------------------------------------------------------------------
		/* Methods */
		public void AddScore(int score = 1)
		{
			Score += score;
			OnChangeScore?.Invoke(Score);
		}

		//------------------------------------------------------------

		void SetHighScore()
		{
			// スコアが0以下の場合はハイスコア更新しない
			if (Score <= 0) return;

			if (Score > HighScore)
			{
				// ハイスコア初回更新時
				if (!isFirstUpdateHighScore && !isFirstPlay)
				{
					isFirstUpdateHighScore = true;
					OnChangeHighScoreFirst?.Invoke();
				}

				// ハイスコア更新
				HighScore = Score;
				OnChangeHighScore?.Invoke(HighScore);
			}
		}

		/// <summary> ハイスコア保存 </summary>
		public void SetHighScoreToData()
		{
			PlayerPrefs.SetInt("HighScore", HighScore);
		}
	}
}
