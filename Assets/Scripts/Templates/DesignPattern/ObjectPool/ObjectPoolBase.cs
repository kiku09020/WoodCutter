using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Template.DesignPatterns.ObjectPool
{
	/// <summary> オブジェクトプール基底クラス </summary>
	public class ObjectPoolBase<T> where T : class
	{
		/* Properties */
		public ObjectPool<T> Pool { get; protected set; }

		//-------------------------------------------------------------------
		/* Events */
		protected System.Func<T> OnCreateObject;
		protected System.Action<T> OnGetObject;
		protected System.Action<T> OnReleaseObject;
		protected System.Action<T> OnDestroyObject;

		/// <summary> コールバック設定 </summary>
		protected virtual void SetPoolCallbacks() { }

		/// <summary> プール初期化 </summary>
		protected void SetPool(bool collectionCheck = true, int defaultCapacity = 10, int maxCapacity = 100)
		{
			SetPoolCallbacks();
			Pool = new ObjectPool<T>(OnCreateObject, OnGetObject, OnReleaseObject, OnDestroyObject, collectionCheck, defaultCapacity, maxCapacity);
		}

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> オブジェクトを取得 </summary>
		public virtual T GetPooledObject()
		{
			return Pool.Get();
		}

		/// <summary> オブジェクトを解放 </summary>
		public virtual void ReleasePooledObject(T obj)
		{
			Pool.Release(obj);
		}
	}
}
