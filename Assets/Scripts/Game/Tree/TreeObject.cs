using System.Collections;
using System.Collections.Generic;
using Game.Tree.Stem;
using UnityEngine;

namespace Game.Tree
{
	/// <summary> 木 </summary>
	public class TreeObject : MonoBehaviour
	{
		/* Fields */
		[Header("Components")]
		[SerializeField] StemGenerator stemGenerator;
		StemCutter stemCutter = new();

		[Header("Properties")]
		[SerializeField] int startStemCount = 16;
		[SerializeField] float startPosY = -4;

		float currentStemPosY;

		Queue<StemObject> stems = new();

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			// 初期生成
			GenerateStemsOnStart();
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				// 幹生成
				var generatedStem = stemGenerator.GenerateStem(currentStemPosY, stems);
				AddStemPosY(generatedStem);

				// 幹切る
				var cutStem = stemCutter.CutStem(stems);
				RemoveStemPosY(cutStem);

			}
		}
		//-------------------------------------------------------------------
		/* Methods */

		void AddStemPosY(StemObject stem)
		{
			currentStemPosY += stem.transform.localScale.y;
		}

		void RemoveStemPosY(StemObject stem)
		{
			currentStemPosY -= stem.transform.localScale.y;
		}

		//------------------------------------------------------------

		void GenerateStemsOnStart()
		{
			currentStemPosY = startPosY;

			// 一番下の幹はリストに追加しないで生成
			var firstStem = stemGenerator.GenerateStem(currentStemPosY);
			AddStemPosY(firstStem);

			// 初期生成
			for (int i = 0; i < startStemCount; i++)
			{
				var stem = stemGenerator.GenerateStem(currentStemPosY, stems);
				AddStemPosY(stem);
			}
		}

	}

}
