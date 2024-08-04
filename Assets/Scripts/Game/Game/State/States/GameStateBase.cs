using Template.DesignPatterns.State;
using Template.Utils.UI.UIManager;
using UnityEngine;

namespace Game.State
{
	public abstract class GameStateBase : MonoBehaviour, IState
	{
		/* Fields */
		[SerializeField] protected UIGroup uiGroup;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		public event System.Action<System.Type> OnStateTransition;

		//-------------------------------------------------------------------
		/* Methods */
		public virtual void OnEnter()
		{
			uiGroup.Show();
		}
		public virtual void OnExit()
		{
			uiGroup.Hide();
		}

		public virtual void OnUpdate() { }
		public virtual void OnFixedUpdate() { }

		protected virtual void TransitionState<State>() where State : GameStateBase
		{
			OnStateTransition?.Invoke(typeof(State));
		}
	}
}
