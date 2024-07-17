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
		[SerializeField] StemGenerator stemGenerator;
		StemCutter stemCutter = new();

		Queue<StemObject> stems = new();

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		void Awake()
		{
			stemGenerator.GenerateStemsOnStart(stems);
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				stemCutter.CutStem(stems);
			}
		}
	}

	//-------------------------------------------------------------------
	/* Methods */

}
