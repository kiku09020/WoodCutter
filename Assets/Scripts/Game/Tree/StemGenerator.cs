using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Tree.Stem;
using Template.DesignPatterns.ObjectPool;

namespace Game.Tree
{
	/// <summary> 幹の生成 </summary>
	public class StemGenerator : PooledMonoBehaviourObjectManager<StemObject>
	{
		/* Fields */
		[Header("Prefabs")]
		[SerializeField] BranchGenerator branchGenerator;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		//-------------------------------------------------------------------
		/* Methods */
		public override void Initialize()
		{
			base.Initialize();

			branchGenerator.Initialize();
		}

		/// <summary> 幹生成 </summary>
		/// <remarks> Queueに追加しない </remarks>
		public StemObject GenerateStem(float currentStemPosY, bool isSetBranch = true)
		{
			// 生成
			var stem = pool.GetPooledObject(new Vector3(0, currentStemPosY, 0));

			// 枝をセット
			if (isSetBranch)
			{
				branchGenerator.GenerateBranch(stem);
			}

			return stem;
		}

		/// <summary> Queueに追加して幹生成 </summary>
		public StemObject GenerateStem(float currentStemPosY, Queue<StemObject> stems)
		{
			var stem = GenerateStem(currentStemPosY);
			stems.Enqueue(stem);

			return stem;
		}


		//------------------------------------------------------------



	}
}
