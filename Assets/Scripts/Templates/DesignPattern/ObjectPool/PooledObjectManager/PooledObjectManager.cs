using UnityEngine;

namespace Template.DesignPatterns.ObjectPool
{
	/// <summary> ObjectPoolを操作するクラス </summary>
	public class PooledObjectManager<T> : MonoBehaviour where T : class
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

		protected ObjectPoolBase<T> pool;
	}

	//------------------------------------------------------------

	/// <summary> Component型がプール対象のオブジェクトプールを操作するクラス </summary>
	public class PooledComponentObjectManager<T> : PooledObjectManager<T> where T : Component
	{
		protected virtual void Awake()
		{
			pool = new ObjectPool_Component<T>(prefab, parent, collectionCheck, defaultCapacity, maxCapacity);
		}
	}

	/// <summary> PooledMonoBehaviour型がプール対象のオブジェクトプールを操作するクラス </summary>
	public class PooledMonoBehaviourObjectManager<T> : PooledObjectManager<T> where T : PooledMonoBehaviour
	{
		protected virtual void Awake()
		{
			pool = new ObjectPool_MonoBehaviour<T>(prefab, parent, collectionCheck, defaultCapacity, maxCapacity);
		}
	}
}
