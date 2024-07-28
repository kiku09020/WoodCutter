using System;
using System.Collections;
using System.Collections.Generic;
using Template.DesignPatterns.ObjectPool;
using UnityEngine;

namespace Game.Tree.Stem
{
	/// <summary> 幹オブジェクト </summary>
	public class StemObject : PooledMonoBehaviour
	{
		/* Fields */
		BranchObject branch;

		//-------------------------------------------------------------------
		/* Properties */
		public bool HasBranch { get; private set; }
		public Directions BranchDirection { get; private set; }

		public BranchObject Branch => branch;

		//-------------------------------------------------------------------
		/* Events */

		//-------------------------------------------------------------------
		/* Methods */
		public override void SetPooledObject(IDisposable disposable)
		{
			base.SetPooledObject(disposable);

			Initialize();
		}

		void Initialize()
		{
			HasBranch = false;
			branch = null;
			BranchDirection = Directions.None;
		}

		//------------------------------------------------------------

		/// <summary> 枝をセット </summary>
		public void SetBranch(BranchObject branch, Directions direction, Vector3 position)
		{
			branch.transform.SetParent(transform);
			branch.transform.localPosition = position;
			HasBranch = true;
			this.branch = branch;
			BranchDirection = direction;
		}

		/// <summary> 幹を切る </summary>
		public void CutStem()
		{
			Dispose();

			if (HasBranch)
			{
				branch.Dispose();
			}
		}

	}
}
