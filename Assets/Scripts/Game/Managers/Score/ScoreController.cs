using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Score
{
	public interface IScoreRef
	{
		int Score { get; }
		event System.Action<int> OnChangeScore;
	}

	/// <summary> スコア操作クラス </summary>
	public class ScoreController : MonoBehaviour, IScoreRef
	{
		/* Fields */

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
