using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Template.DesignPatterns.ObjectPool
{
	/// <summary> Object型がプール対象のオブジェクトプール </summary>
	/// <remarks> 生成時にInstantiate,破棄時にDestroyを呼ばれる </remarks>
	public abstract class ObjectPool_UnityObject<T> : ObjectPoolBase<T> where T : Object
	{
		protected T prefab;
		protected Transform parent;

		public ObjectPool_UnityObject(T prefab, Transform parent, bool collectionCheck = true, int defaultCapacity = 10, int maxCapacity = 100)
		{
			this.prefab = prefab;
			this.parent = parent;

			SetPool(collectionCheck, defaultCapacity, maxCapacity);
		}

		protected override void SetPoolCallbacks()
		{
			base.SetPoolCallbacks();

			// 生成時にインスタンス化
			OnCreateObject += () =>
			{
				var obj = Object.Instantiate(prefab, parent);
				return obj;
			};

			// 削除時に破棄
			OnDestroyObject += (obj) =>
			{
				Object.Destroy(obj);
			};
		}
	}

	//------------------------------------------------------------

	/// <summary> GameObject型がプール対象のオブジェクトプール </summary>
	/// <remarks> 取得時や解放時に、自身をSetActive()する </remarks>
	public class ObjectPool_GameObject : ObjectPool_UnityObject<GameObject>
	{
		public ObjectPool_GameObject(GameObject prefab, Transform parent, bool collectionCheck = true, int defaultCapacity = 10, int maxCapacity = 100)
			: base(prefab, parent, collectionCheck, defaultCapacity, maxCapacity) { }

		protected override void SetPoolCallbacks()
		{
			base.SetPoolCallbacks();

			// 取得時にアクティブ化
			OnGetObject += (obj) =>
			{
				obj.SetActive(true);
			};

			// 解放時に非アクティブ化
			OnReleaseObject += (obj) =>
			{
				obj.SetActive(false);
			};
		}
	}

	/// <summary> Component型がプール対象のオブジェクトプール </summary>
	/// <remarks> 取得時や解放時に、gameObjectをSetActive()する </remarks>
	public class ObjectPool_Component<T> : ObjectPool_UnityObject<T> where T : Component
	{
		public ObjectPool_Component(T prefab, Transform parent, bool collectionCheck = true, int defaultCapacity = 10, int maxCapacity = 100)
			: base(prefab, parent, collectionCheck, defaultCapacity, maxCapacity) { }

		protected override void SetPoolCallbacks()
		{
			base.SetPoolCallbacks();

			OnDestroyObject = (obj) =>
			{
				// Componentの場合はgameObjectを破棄
				Object.Destroy(obj.gameObject);
			};

			// 取得時にアクティブ化
			OnGetObject += (obj) =>
			{
				obj.gameObject.SetActive(true);
			};

			// 解放時に非アクティブ化
			OnReleaseObject += (obj) =>
			{
				obj.gameObject.SetActive(false);
			};
		}
	}

	/// <summary> PooledMonoBehaviour型がプール対象のオブジェクトプール </summary>
	/// <remarks> PooledMonoBehaviour型を継承した独自のクラスをプールする。
	/// Disposableで、自身を解放できる </remarks>
	public class ObjectPool_MonoBehaviour<T> : ObjectPool_UnityObject<T> where T : PooledMonoBehaviour
	{
		public ObjectPool_MonoBehaviour(T prefab, Transform parent, bool collectionCheck = true, int defaultCapacity = 10, int maxCapacity = 100)
			: base(prefab, parent, collectionCheck, defaultCapacity, maxCapacity) { }

		protected override void SetPoolCallbacks()
		{
			base.SetPoolCallbacks();

			OnDestroyObject = (obj) =>
			{
				// PooledMonoBehaviourの場合はDispose
				//obj.Dispose();
				Object.Destroy(obj.gameObject);
			};

			// 取得時にアクティブ化
			OnGetObject += (obj) =>
			{
				obj.gameObject.SetActive(true);
			};

			// 解放時に非アクティブ化
			OnReleaseObject += (obj) =>
			{
				obj.gameObject.SetActive(false);
			};
		}

		// Disposableセットして取得
		public override T GetPooledObject()
		{
			var disposable = Pool.Get(out var obj);
			obj.SetPooledObject(disposable);
			return obj;
		}
	}
}
