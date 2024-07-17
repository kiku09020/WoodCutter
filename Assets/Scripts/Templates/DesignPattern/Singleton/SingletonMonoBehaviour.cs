using UnityEngine;

namespace Template.DesignPatterns.Singleton
{
	/// <summary> UnityMonoBehaviour用のシングルトン </summary>
	public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{
		static T instance;
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					// 既存のインスタンスを探す
					instance = FindObjectOfType<T>();

					// 既存のインスタンスがない場合は新規作成
					if (instance == null)
					{
						CreateInstance();
					}
				}
				return instance;
			}
		}

		/// <summary> インスタンス生成処理 </summary>
		protected static void CreateInstance()
		{
			if (instance == null)
			{
				var obj = new GameObject(typeof(T).Name);
				instance = obj.AddComponent<T>();
			}
		}

		//------------------------------------------------------------

		protected virtual void Awake()
		{
			RemoveDuplicateInstance();
		}

		// 重複したインスタンスを削除
		void RemoveDuplicateInstance()
		{
			if (instance == null)
			{
				instance = this as T;
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}
