using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Tree.Stem;

namespace Game.Tree
{
	/// <summary> 幹の生成 </summary>
	public class StemGenerator : MonoBehaviour
	{
		/* Fields */
		[SerializeField] StemObject stemPrefab;
		[SerializeField] BranchObject branchPrefab;

		[Header("Prameters")]
		[SerializeField, Tooltip("枝の生成確率")]
		float branchProb = 0.3f;
		[SerializeField, Tooltip("開始時の枝同士の最小間隔")]
		int startMinBranchDistance = 5;

		int currentBranchDistance;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		//-------------------------------------------------------------------
		/* Methods */
		public void Initialize()
		{
			currentBranchDistance = startMinBranchDistance;
		}

		/// <summary> 幹生成 </summary>
		/// <remarks> Queueに追加しない </remarks>
		public StemObject GenerateStem(float currentStemPosY, bool isSetBranch = true)
		{
			// 生成
			var stem = Instantiate(stemPrefab, new Vector3(0, currentStemPosY, 0), Quaternion.identity, transform);

			// 枝をセット
			if (isSetBranch)
			{
				GenerateBranch(stem);

				// 枝同士の間隔値を設定
				currentBranchDistance--;
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

		/// <summary> 枝をセット </summary>
		void GenerateBranch(StemObject stem)
		{
			// 間隔が一定以上ないと生成しない
			if (currentBranchDistance > 0)
			{
				return;
			}

			// 枝を生成するか
			if (Random.value < branchProb)
			{
				var branch = Instantiate(branchPrefab, Vector3.zero, Quaternion.identity, stem.transform);

				// 左右どちらに生成するか
				var isLeftBranch = Random.value < 0.5f;
				var branchPosX = isLeftBranch ? -1.0f : 1.0f;
				var branchPos = new Vector3(branchPosX, 0, 0);
				branch.transform.localPosition = branchPos;

				stem.SetBranch(branch);

				// 枝同士の間隔値を設定
				currentBranchDistance = startMinBranchDistance;
			}
		}

	}
}
