using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template.DesignPatterns.State
{
	public interface IState
	{
		public event System.Action<System.Type> OnStateTransition;

		public void OnEnter();
		public void OnExit();
		public void OnUpdate();
		public void OnFixedUpdate();
	}

	public class StateBase<T> : IState where T : StateBase<T>
	{
		/* Fields */
		protected bool isDebug = false;
		string typeName;
		string TypeName
		{
			get
			{
				if (string.IsNullOrEmpty(typeName))
				{
					typeName = GetType().Name;
				}
				return typeName;
			}
		}

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		public event System.Action<System.Type> OnStateTransition;

		//-------------------------------------------------------------------
		/* Methods */
		protected void TransitionState<State>() where State : T
		{
			OnStateTransition?.Invoke(typeof(State));
		}

		public virtual void OnEnter()
		{
			if (isDebug)
			{
				Debug.Log("OnEnter: " + TypeName);
			}
		}
		public virtual void OnExit()
		{
			if (isDebug)
			{
				Debug.Log("OnExit: " + TypeName);
			}
		}
		public virtual void OnUpdate() { }
		public virtual void OnFixedUpdate() { }
	}
}
