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
		[SerializeField] int maxLevelScore = 1500;

		public float ScoreLevelRate => (float)Score / maxLevelScore;

		//-------------------------------------------------------------------
		/* Properties */
		public int Score { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		public event System.Action<int> OnChangeScore;

		//-------------------------------------------------------------------
		/* Methods */
		public void AddScore(int score = 1)
		{
			Score += score;
			OnChangeScore?.Invoke(Score);
		}
	}
}
