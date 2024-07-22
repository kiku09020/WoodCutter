using System.Collections;
using System.Collections.Generic;
using Game.Player;
using Game.Tree;
using UnityEngine;

namespace Game
{
	/// <summary> ゲーム管理クラス </summary>
	public class GameManager : MonoBehaviour
	{
		/* Fields */
		[SerializeField] PlayerObject player;
		[SerializeField] TreeObject tree;

		//-------------------------------------------------------------------
		/* Properties */
		public bool IsGameOver { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			player.OnTapped += CheckGameOver;
		}

		//-------------------------------------------------------------------
		/* Methods */
		void CheckGameOver()
		{
			if (tree.GetBottomStemBranchDirection() == player.Direction)
			{
				IsGameOver = true;
				Debug.Log("Game Over");
			}
		}
	}
}
