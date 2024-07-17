using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Template.DesignPatterns.ObjectPool
{
	/// <summary> �I�u�W�F�N�g�v�[�����N���X </summary>
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

		/// <summary> �R�[���o�b�N�ݒ� </summary>
		protected virtual void SetPoolCallbacks() { }

		/// <summary> �v�[�������� </summary>
		protected void SetPool(bool collectionCheck = true, int defaultCapacity = 10, int maxCapacity = 100)
		{
			SetPoolCallbacks();
			Pool = new ObjectPool<T>(OnCreateObject, OnGetObject, OnReleaseObject, OnDestroyObject, collectionCheck, defaultCapacity, maxCapacity);
		}

		//-------------------------------------------------------------------
		/* Methods */
		/// <summary> �I�u�W�F�N�g���擾 </summary>
		public virtual T GetPooledObject()
		{
			return Pool.Get();
		}

		/// <summary> �I�u�W�F�N�g����� </summary>
		public virtual void ReleasePooledObject(T obj)
		{
			Pool.Release(obj);
		}
	}
}
