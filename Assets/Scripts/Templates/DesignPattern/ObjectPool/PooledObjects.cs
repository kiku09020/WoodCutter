using UnityEngine;

namespace Template.DesignPatterns.ObjectPool
{
	/// <summary> プール対象のMonoBehaviour派生クラス </summary>
	public class PooledMonoBehaviour : MonoBehaviour
	{
		private System.IDisposable disposable;

		// Disposable取得
		public void SetPooledObject(System.IDisposable disposable)
		{
			this.disposable = disposable;
		}

		/// <summary> オブジェクトを解放する </summary>
		public void Dispose()
		{
			if (disposable == null) return;
			disposable.Dispose();
		}
	}
}