using UnityEngine;

namespace Game.Tree.Branch.Item
{
	/// <summary> りんご </summary>
	public class Apple : BranchItem, IScoreItem
	{
		[SerializeField] int score = 10;

		public int Score => score;

		public void GetItem()
		{
			Dispose();
		}
	}
}
