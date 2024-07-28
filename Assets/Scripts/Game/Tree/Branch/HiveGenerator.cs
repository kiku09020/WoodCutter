using Game.Score;
using UnityEngine;

namespace Game.Tree.Branch.Item
{
	/// <summary> 蜂の巣生成クラス </summary>
	public class HiveGenerator : BranchItemGenerator
	{
		[SerializeField] float generatedProb = 0.1f;
		[SerializeField] float generatedProbMax = 0.5f;
		[SerializeField] ScoreController scoreController;

		public override float GeneratedProb => GetGeneratedProb();

		//------------------------------------------------------------

		public override BranchItem Generate()
		{
			return pool.GetPooledObject(Vector3.zero);
		}

		/// <summary> スコアに応じて生成確率変える </summary>
		float GetGeneratedProb()
		{
			return Mathf.Min(generatedProb * scoreController.ScoreLevelRate, generatedProbMax);
		}
	}
}
