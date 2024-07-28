using UnityEngine;

namespace Game.Tree.Branch.Item
{
	/// <summary> 蜂の巣生成クラス </summary>
	public class HiveGenerator : BranchItemGenerator
	{
		[SerializeField] float generatedProb = 0.5f;

		public override float GeneratedProb => generatedProb;

		//------------------------------------------------------------

		public override BranchItem Generate()
		{
			return pool.GetPooledObject(Vector3.zero);
		}
	}
}
