using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Template.DesignPatterns.ObjectPool;
using UnityEngine;

namespace Game.Tree.Stem
{
	/// <summary> 幹オブジェクト </summary>
	public class StemObject : PooledMonoBehaviour
	{
		/* Fields */
		[SerializeField] float moveDuration = 0.5f;
		[SerializeField] Ease moveEase;

		BranchObject branch;

		Tween moveTween;

		//-------------------------------------------------------------------
		/* Properties */
		public bool HasBranch { get; private set; }
		public Directions BranchDirection { get; private set; }

		public BranchObject Branch => branch;

		//-------------------------------------------------------------------
		/* Events */
		public event System.Action<Directions, System.Action> OnCut;

		//-------------------------------------------------------------------
		/* Methods */
		public override void SetPooledObject(IDisposable disposable)
		{
			base.SetPooledObject(disposable);

			Initialize();
		}

		public override void Dispose()
		{
			base.Dispose();

			if (HasBranch)
			{
				branch.Dispose();
			}
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
			branch.transform.rotation = Quaternion.identity;
			HasBranch = true;
			this.branch = branch;
			BranchDirection = direction;
		}

		/// <summary> 幹を切る </summary>
		public void CutStem(Directions direction)
		{
			OnCut?.Invoke(direction, Dispose);
		}

		public void PlayCuttingMoveAnimation()
		{
			moveTween?.Complete();
			moveTween = transform.DOLocalMoveY(transform.localPosition.y - transform.localScale.y, moveDuration)
				.SetEase(moveEase);
		}
	}
}
