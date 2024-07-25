using UnityEngine;

namespace Template.DesignPatterns.ObjectPool
{
	/// <summary> ObjectPoolを操作するクラス </summary>
	public abstract class PooledObjectManager<T, U> : MonoBehaviour where T : class where U : ObjectPoolBase<T>
	{
		[Header("Object")]
		[SerializeField] protected T prefab;
		[SerializeField] protected Transform parent;

		[Header("Pool Settings")]
		[SerializeField, Tooltip("インスタンスが既にプールにある場合に例外を出す")]
		protected bool collectionCheck = true;
		[SerializeField, Tooltip("PoolのListの初期容量")]
		protected int defaultCapacity = 10;
		[SerializeField, Tooltip("プールの最大容量。これを超えている場合にReleaseされると、OnDestroyが呼ばれる")]
		protected int maxCapacity = 100;

		protected U pool;

		public abstract void Initialize();
	}

	//------------------------------------------------------------

	/// <summary> Component型がプール対象のオブジェクトプールを操作するクラス </summary>
	public class PooledComponentObjectManager<T> : PooledObjectManager<T, ObjectPool_Component<T>> where T : Component
	{
		public override void Initialize()
		{
			pool = new ObjectPool_Component<T>(prefab, parent, collectionCheck, defaultCapacity, maxCapacity);
		}
	}

	/// <summary> PooledMonoBehaviour型がプール対象のオブジェクトプールを操作するクラス </summary>
	public class PooledMonoBehaviourObjectManager<T> : PooledObjectManager<T, ObjectPool_MonoBehaviour<T>> where T : PooledMonoBehaviour
	{
		public override void Initialize()
		{
			pool = new ObjectPool_MonoBehaviour<T>(prefab, parent, collectionCheck, defaultCapacity, maxCapacity);
		}
	}
}
