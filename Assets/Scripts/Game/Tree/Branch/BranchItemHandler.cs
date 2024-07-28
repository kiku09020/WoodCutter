using System.Collections.Generic;
using UnityEngine;

namespace Game.Tree.Branch.Item
{
	/// <summary> 枝アイテムハンドラ </summary>
	public class BranchItemHandler : MonoBehaviour
	{

		[SerializeField] List<BranchItemGenerator> branchItemGenerators = new List<BranchItemGenerator>();

		//------------------------------------------------------------

		public void Initialize()
		{
			foreach (var generator in branchItemGenerators)
			{
				generator.Initialize();
			}
		}

		/// <summary> 生成 </summary>
		public BranchItem Generate()
		{
			var generatedItems = new List<int>();

			// 確率をもとに生成
			for (int i = 0; i < branchItemGenerators.Count; i++)
			{
				if (Random.value < branchItemGenerators[i].GeneratedProb)
				{
					generatedItems.Add(i);
				}
			}

			if (generatedItems.Count != 0)
			{
				// 生成アイテムをランダムに選択
				var index = Random.Range(0, generatedItems.Count);
				return branchItemGenerators[generatedItems[index]].Generate();
			}
			else
			{
				return null;
			}
		}
	}
}
