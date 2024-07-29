using Template.DesignPatterns.ObjectPool;
using UnityEngine;

namespace Game.Tree.Branch.Item
{
	/// <summary> ブランチにつくアイテム基底クラス </summary>
	public abstract class BranchItem : PooledMonoBehaviour
	{
		[SerializeField] float fallProb = 0.5f;
		[SerializeField] float fallSpeed = 3;

		float currentFallVel;

		public bool IsFalling { get; private set; }

		//------------------------------------------------------------

		public override void SetPooledObject(System.IDisposable disposable)
		{
			base.SetPooledObject(disposable);
			IsFalling = false;
			currentFallVel = 0;
		}

		public override void Dispose()
		{
			base.Dispose();
		}

		void FixedUpdate()
		{
			if (!IsFalling) return;

			// 落下処理
			currentFallVel += Physics.gravity.y * Time.fixedDeltaTime;
			transform.position += Vector3.up * currentFallVel * fallSpeed * Time.fixedDeltaTime;

			if (transform.position.y < -10f)
			{
				IsFalling = false;
				Dispose();
			}
		}

		//------------------------------------------------------------
		/// <summary> アイテム落下 </summary>
		public bool Fall()
		{
			if (IsFalling || fallProb < Random.value) return false;

			// 落下処理
			IsFalling = true;
			transform.SetParent(null);

			return true;
		}
	}
}
