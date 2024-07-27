using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Template.Utils
{
	/// <summary> 検索対象のインターフェース </summary>
	public interface IFoundObject
	{
	}

	public class ObjectUtils
	{
		/// <summary> 指定されたインターフェースのMonoBehaviourクラスを取得する </summary>
		public static IReadOnlyCollection<T> FindObjectsByInterface<T>() where T : IFoundObject
		{
			return Object.FindObjectsOfType<MonoBehaviour>().OfType<T>().ToArray();
		}
	}
}
