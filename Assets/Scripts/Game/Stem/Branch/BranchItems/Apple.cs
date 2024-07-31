using System;
using UnityEngine;

namespace Game.Tree.Branch.Item
{
	/// <summary> りんご </summary>
	public class Apple : BranchItem, IScoreItem
	{
		[SerializeField] int score = 10;
		[SerializeField] ParticleSystem getParticle;

		public int Score => score;

		public override void SetPooledObject(IDisposable disposable)
		{
			base.SetPooledObject(disposable);

			getParticle.transform.SetParent(transform);
			getParticle.transform.localPosition = Vector3.zero;
		}

		public void GetItem()
		{
			getParticle.transform.SetParent(null);
			getParticle.Play();

			Dispose();
		}
	}
}
