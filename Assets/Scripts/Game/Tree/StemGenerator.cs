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
		[SerializeField] float startPosY = -4;
		[SerializeField] int startStemCount = 5;

		float currentStemPosY;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		//-------------------------------------------------------------------
		/* Methods */
		public void GenerateStemsOnStart(Queue<StemObject> stems)
		{
			currentStemPosY = startPosY;

			// 一番下の幹はリストに追加しないで生成
			GenerateStem();

			// 初期生成
			for (int i = 0; i < startStemCount; i++)
			{
				GenerateStem(stems);
			}
		}

		/// <summary> 幹生成 </summary>
		/// <remarks> Queueに追加しない </remarks>
		public StemObject GenerateStem()
		{
			// 生成
			var stem = Instantiate(stemPrefab, new Vector3(0, currentStemPosY, 0), Quaternion.identity, transform);

			// 木の高さを更新
			currentStemPosY += stem.transform.localScale.y;

			return stem;
		}

		/// <summary> Queueに追加して幹生成 </summary>
		public StemObject GenerateStem(Queue<StemObject> stems)
		{
			var stem = GenerateStem();
			stems.Enqueue(stem);

			return stem;
		}

	}
}
