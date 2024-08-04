using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Tree.Branch.Item
{
	/// <summary> りんご </summary>
	public class Apple : BranchItem, IScoreItem
	{
		[SerializeField] int score = 10;

		[Header("Components")]
		[SerializeField] Collider2D col;
		[SerializeField] ParticleSystem getParticle;

		public int Score => score;

		public override void SetPooledObject(IDisposable disposable)
		{
			base.SetPooledObject(disposable);

			getParticle.transform.SetParent(transform);
			getParticle.transform.localPosition = Vector3.zero;
			getParticle.transform.localScale = Vector3.one;

			col.enabled = true;
		}

		public void GetItem()
		{
			col.enabled = false;

			getParticle.transform.SetParent(null);
			getParticle.Play();
			seController.PlayAudio("GetItem");

			transform.DOScale(Vector3.zero, 0.25f)
				.OnComplete(() => Dispose());
		}
	}
}
