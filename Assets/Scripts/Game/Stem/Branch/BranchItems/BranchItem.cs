using Template.DesignPatterns.ObjectPool;
using UnityEngine;

namespace Game.Tree.Branch.Item
{
	/// <summary> ブランチにつくアイテム基底クラス </summary>
	public abstract class BranchItem : PooledMonoBehaviour
	{
		[SerializeField] float fallProb = 0.5f;
		[SerializeField] float fallSpeed = 3;

		bool isFalling = false;

		float currentFallVel;

		public bool IsDisposed { get; private set; }

		//------------------------------------------------------------

		public override void SetPooledObject(System.IDisposable disposable)
		{
			base.SetPooledObject(disposable);

			isFalling = false;
			currentFallVel = 0;
			IsDisposed = false;
		}

		public override void Dispose()
		{
			base.Dispose();

			IsDisposed = true;
		}

		void FixedUpdate()
		{
			if (!isFalling) return;

			// 落下処理
			currentFallVel += Physics.gravity.y * Time.fixedDeltaTime;
			transform.position += Vector3.up * currentFallVel * fallSpeed * Time.fixedDeltaTime;

			if (transform.position.y < -10f)
			{
				isFalling = false;
				Dispose();
			}
		}

		//------------------------------------------------------------
		/// <summary> アイテム落下 </summary>
		public bool Fall()
		{
			if (isFalling || fallProb < Random.value) return false;

			// 落下処理
			isFalling = true;
			transform.SetParent(null);

			return true;
		}
	}
}
