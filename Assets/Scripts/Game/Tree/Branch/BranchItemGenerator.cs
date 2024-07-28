using Template.DesignPatterns.ObjectPool;

namespace Game.Tree.Branch.Item
{
	/// <summary> 枝につくアイテムの生成基底クラス </summary>
	public abstract class BranchItemGenerator : PooledMonoBehaviourObjectManager<BranchItem>
	{
		/// <summary> 生成確率 </summary>
		public abstract float GeneratedProb { get; }

		//------------------------------------------------------------

		/// <summary> 生成 </summary>
		public abstract BranchItem Generate();
	}
}
