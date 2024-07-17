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

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> 幹生成 </summary>
		/// <remarks> Queueに追加しない </remarks>
		public StemObject GenerateStem(float currentStemPosY)
		{
			// 生成
			var stem = Instantiate(stemPrefab, new Vector3(0, currentStemPosY, 0), Quaternion.identity, transform);

			return stem;
		}

		/// <summary> Queueに追加して幹生成 </summary>
		public StemObject GenerateStem(float currentStemPosY, Queue<StemObject> stems)
		{
			var stem = GenerateStem(currentStemPosY);
			stems.Enqueue(stem);

			return stem;
		}

	}
}
