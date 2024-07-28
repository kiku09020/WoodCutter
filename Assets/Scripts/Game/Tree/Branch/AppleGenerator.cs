using UnityEngine;

namespace Game.Tree.Branch.Item
{
	/// <summary> りんご生成クラス </summary>
	public class AppleGenerator : BranchItemGenerator
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
