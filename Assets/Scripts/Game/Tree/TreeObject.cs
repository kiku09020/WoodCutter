using System.Collections;
using System.Collections.Generic;
using Game.Player;
using Game.Score;
using Game.Tree.Stem;
using UnityEngine;

namespace Game.Tree
{
	/// <summary> 木 </summary>
	public class TreeObject : MonoBehaviour
	{
		/* Fields */
		[SerializeField] PlayerObject player;
		[SerializeField] ScoreController scoreController;

		[Header("Components")]
		[SerializeField] StemGenerator stemGenerator;

		[Header("Properties")]
		[SerializeField] int startStemCount = 16;
		[SerializeField] float startPosY = -4;

		float currentStemPosY;

		Queue<StemObject> stems = new();

		//-------------------------------------------------------------------
		/* Properties */
		/// <summary> 一番下の幹が枝を持っているかどうか </summary>
		public bool HasBranchStemOnBottom => stems.Peek().HasBranch;

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			stemGenerator.Initialize();

			// 初期生成
			GenerateStemsOnStart();

			// カットイベント登録
			player.OnCutTree += CutStem;
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
			var firstStem = stemGenerator.GenerateStem(currentStemPosY, false);
			AddStemPosY(firstStem);

			// 初期生成
			for (int i = 0; i < startStemCount; i++)
			{
				var stem = stemGenerator.GenerateStem(currentStemPosY, stems);
				AddStemPosY(stem);
			}
		}

		//------------------------------------------------------------

		/// <summary> 木を切る処理 </summary>
		void CutStem(StemCutter stemCutter)
		{
			// 幹生成
			var generatedStem = stemGenerator.GenerateStem(currentStemPosY, stems);
			AddStemPosY(generatedStem);

			// 幹切る
			var cutStem = stemCutter.CutStem(stems);
			RemoveStemPosY(cutStem);

			// スコア加算
			// 幹によってスコア変更など
			scoreController.AddScore();
		}

		//------------------------------------------------------------

		/// <summary> 一番下の幹の枝の方向を取得する </summary>
		/// <returns> 枝の方向。枝がなければNoneを返す </returns>
		public Directions GetBottomStemBranchDirection()
		{
			if (HasBranchStemOnBottom)
			{
				return stems.Peek().BranchDirection;
			}
			else
			{
				return Directions.None;
			}
		}
	}

}
